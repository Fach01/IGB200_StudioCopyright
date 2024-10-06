using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
using UnityEngine.Playables;
using System.Collections;
using System.Linq;
public class LevelManager : MonoBehaviour
{
    public BuildingManager buildingManager;
    public level level;
    public GameObject player;
    public GameObject uiManager;
    public GameObject playField;
    public GameObject deck;

    public int startBudget;
    public int utilitiesGoal;
    public int frameworksGoal;

    private PlayerManager playerManager;
    private UIManager UIManager;
    private PlayFieldManager playFieldManager;
    private EventManager eventManager;

    private int levelBudget;
    private int turnBudget;
    private int utilitiesCount;
    private int frameworksCount;

    private int turn = 1;

    public GameObject win;
    public GameObject lose;

    [Header("Animation Stuff")]
    public int tempBudget;
    public int tempFrames;
    public int tempUtil;


    private void Awake()
    {
        playerManager = player.GetComponent<PlayerManager>();
        UIManager = uiManager.GetComponent<UIManager>();
        playFieldManager = playField.GetComponent<PlayFieldManager>();
        eventManager = GetComponent<EventManager>();
    }

    private void Start()
    {
        if (level == level.level2) startBudget += buildingManager.BudgetPoolLevel1;
        if (level == level.level3) startBudget += buildingManager.BudgetPoolLevel2 + buildingManager.BudgetPoolLevel1;
        AudioManager.instance.PlayMusic("LevelMusic");
        BeginLevel();
    }

    // Update is called once per frame
    private void Update()
    {
        switch (playerManager.phase)
        {
            case Phase.PreTurn:
                PreTurn();
                break;
            case Phase.Setup:
                SetupPhase();
                break;
            case Phase.Play:
                PlayPhase();
                break;
            case Phase.Event:
                EventPhase();
                break;
            case Phase.End:
                EndPhase();
                break;
            default:
                break;
        }
    }

    private void BeginLevel()
    {
        levelBudget = startBudget;
        turnBudget = levelBudget;

        utilitiesCount = 0;
        frameworksCount = 0;
        
        AudioManager.instance.PlaySFX("Shuffle");

        // for (int i = 0; i < 4; i++) { playerManager.DrawCard(); }
        StartCoroutine(playerManager.DrawXCards(4));

        UIManager.SetTurnText(turn);
        UIManager.SetBudgetText(levelBudget.ToString());

        playerManager.phase = Phase.PreTurn;
    }

    public void PreTurn()
    {
        turnBudget = levelBudget;

        if(turn != 1) playerManager.DrawXCards(1); // if its not the very first round draw a card

        playerManager.ResetPlayer();
        playerManager.phase = Phase.Setup;
    }

    public void SetupPhase()
    {
        // idk if anything even needs to be here
    }

    public void EndSetupPhase()
    {
        
        playerManager.SelectCard(null);
        playerManager.phase = Phase.Play;
    }

    public void PlayPhase()
    {
        if (!tallied && !tallying)
            StartCoroutine(PlayPhaseAnimation());
        if (!eop && tallied)
            StartCoroutine(EndOfPlay());

        if (utilitiesCount >= utilitiesGoal && frameworksCount >= frameworksGoal)
        {
            GameManager.instance.UnlockNextLevel();
            win.SetActive(true);
        }
        else if (turnBudget <= 0)
        {
            lose.SetActive(true);
        }

    }

    public void EventPhase()
    {
        // go through events queue
        playerManager.phase = Phase.End;
    }

    public void EndPhase()
    {
        levelBudget = turnBudget;
        turn += 1;
        UIManager.SetBudgetText(levelBudget.ToString());
        UIManager.SetUtilitiesText(utilitiesCount.ToString());
        UIManager.SetFrameworksText(frameworksCount.ToString());
        UIManager.SetTurnText(turn);

        playerManager.phase = Phase.PreTurn;
        tallied = false;
    }

    public void Spend(int cost)
    {
        turnBudget -= cost;
    }
    public void AddUtilities(int utilities)
    {
        utilitiesCount += utilities;
    }

    public void AddFrameworks(int frameworks)
    {
        frameworksCount += frameworks;
    }
    private void UpdateBuildingBudgetPool(int budget)
    {
        if (level == level.level1) { PlayerPrefs.SetInt(buildingManager.buildingname + "level1", budget); }
        else if (level == level.level2) { PlayerPrefs.SetInt(buildingManager.buildingname + "level2", budget); }
    }
    private bool tallied = false;
    private bool tallying = false;
    IEnumerator PlayPhaseAnimation()
    {
        tallied = false;
        tallying = true;
        // animation will play at the start of the play phase to show all cards in play
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {
            if (playFieldManager.cards[i] == null)
            {
                //then start the tallying of the gains and losses
                StartCoroutine(TallyGainsAndLosses());

                yield break;
            }
            playFieldManager.cards[i].GetComponent<CardManager>().cardanimator.SetBool("Add Resource", true);
            yield return new WaitForSeconds(.2f); 
        }
    }
    IEnumerator TallyGainsAndLosses()
    {

        // open EndTurnAnimation 
        UIManager.EndTurnAnimation.SetActive(true);
        UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Entry", true);
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {
            yield return new WaitForSeconds(1f); // waits at the start to keep consistent timing

            if (playFieldManager.cards[i] == null)
            {
                //after animation deactivate EndTurnPanel
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Exit", true);

                yield return new WaitForSeconds(0.5f);

                tallied = true;
                tallying = false;
                yield break;
            }

            int cost = playFieldManager.cards[i].GetComponent<CardManager>().m_card.cost;
            int resource = playFieldManager.cards[i].GetComponent<CardManager>().m_card.resource;
            CardType cardType = playFieldManager.cards[i].GetComponent<CardManager>().m_card.type;

            tempBudget -= cost;

            if (cardType == CardType.Utilities) tempUtil += resource;
            else if (cardType == CardType.Framework) tempFrames += resource;


            UIManager.DisplayTemporaryBudget(tempBudget);
            yield return new WaitForSeconds(0.5f);
            UIManager.DisplayTemporaryUtilities(tempUtil);
            yield return new WaitForSeconds(0.5f);
            UIManager.DisplayTemporaryFramework(tempFrames);

            // if the card gives utilities or frameworks an animation will play
            if ( cardType == CardType.Utilities && resource > 0)
            {
                Debug.Log("Utilities:" + resource);
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Util", true);
                AudioManager.instance.PlaySFX("Add Resource");
               

            }

            if (cardType == CardType.Framework && resource > 0)
            {
                Debug.Log("Frameworks:" + resource);
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Frames", true);
                // AudioManager.instance.PlaySFX("Add Resource");

            }

            //wait for 0.05 seconds to turn off animation to stop looping
            yield return new WaitForSeconds(0.05f);
            UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Util", false);
            UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Frames", false);
        }
    }

    private bool eop = false;
    public IEnumerator EndOfPlay()
    {
        eop = true;
        //apply changes in budge frames and utilities
        turnBudget -= tempBudget;
        utilitiesCount += tempUtil;
        frameworksCount += tempFrames;

        ResetTempPools(); // reset the temporary pools

        // Displays changes to the UI
        UIManager.SetBudgetText(turnBudget.ToString());
        yield return new WaitForSeconds(0.5f);
        UIManager.SetUtilitiesText(utilitiesCount.ToString());
        yield return new WaitForSeconds(0.5f);
        UIManager.SetFrameworksText(frameworksCount.ToString());
        yield return new WaitForSeconds(0.5f);

        playerManager.phase = Phase.Event;
        eop = false;
    }
    public void ResetTempPools()
    {
        //sets all temporary pools as = 0
        tempBudget = 0;
        tempUtil = 0;
        tempFrames = 0;

        //sets all UI temp pools as = 0
        UIManager.DisplayTemporaryBudget(tempBudget);
        UIManager.DisplayTemporaryFramework(tempUtil);
        UIManager.DisplayTemporaryUtilities(tempFrames);

    }
}


