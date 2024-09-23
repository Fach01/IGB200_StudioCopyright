using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
using UnityEngine.Playables;
using System.Collections;
using UnityEditor.Animations;

public class LevelManager : MonoBehaviour
{
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
    }

    private void Start()
    {
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

        for (int i = 0; i < 4; i++)
        {
            deck.GetComponent<DeckManager>().DrawCard();
        }

        UIManager.SetTurnText(turn);
        UIManager.SetBudgetText(levelBudget.ToString());

        playerManager.phase = Phase.PreTurn;
    }

    public void PreTurn()
    {
        turnBudget = levelBudget;

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

        StartCoroutine(PlayPhaseAnimation());
        // TODO: add budged turn loss text animation

        

        if (utilitiesCount >= utilitiesGoal && frameworksCount >= frameworksGoal)
        {
            win.SetActive(true);
        }
        else if (turnBudget <= 0) 
        {
             lose.SetActive(true);
        }

        playerManager.phase = Phase.Event;

        
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
    
    IEnumerator PlayPhaseAnimation()
    {
        // animation will play at the start of the play phase to show all cards in play
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {

            if (playFieldManager.cards[i] == null)
            {
                //set the temporary pools as equal to 0 then play the the tally gains and losses animation
               

                StartCoroutine(TallyGainsAndLosses());
                UIManager.EndTurnAnimation.SetActive(true);
                yield break;
            }
            playFieldManager.cards[i].GetComponent<CardManager>().cardanimator.SetBool("Add Resource", true);
            yield return new WaitForSeconds(.2f);
        }
    }
    IEnumerator TallyGainsAndLosses()
    {
        // animation will play at the start of the play phase to show all cards in play
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {
            yield return new WaitForSeconds(1f);


            UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Frames",false);

            if (playFieldManager.cards[i] == null)
            {
                UIManager.EndTurnAnimation.SetActive(false);

                turnBudget -= tempBudget;
                utilitiesCount += tempUtil;
                frameworksCount += tempFrames;

                ResetTempPools();

                UIManager.SetBudgetText(turnBudget.ToString());
                UIManager.SetUtilitiesText(utilitiesCount.ToString());
                UIManager.SetFrameworksText(frameworksCount.ToString());
                
                yield break;
            }
            //card animation plays

            int cost = playFieldManager.cards[i].GetComponent<CardManager>().m_card.cost;
            int utilities = playFieldManager.cards[i].GetComponent<CardManager>().m_card.utilities;
            int frameworks = playFieldManager.cards[i].GetComponent<CardManager>().m_card.frameworks;

            // if the card gives utitlities or frameworks an animation will play

            tempBudget -= cost;
            tempUtil += utilities;
            tempFrames += frameworks;

            UIManager.DisplayTemporaryBudget(tempBudget);
            UIManager.DisplayTemporaryFramework(tempUtil);
            UIManager.DisplayTemporaryUtilities(tempFrames);

            if (utilities > 0)
            {
                Debug.Log(utilities);
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Frames", true);

            }

            if (frameworks > 0)
            {
                //Debug.Log(frameworks);
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Util", true);

            }
            yield return new WaitForSeconds(0.1f);
            UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Util", false);
            UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Frames", false);
        }
    }
    public void ResetTempPools()
    {
        tempBudget = 0;
        tempUtil = 0;
        tempFrames = 0;

        UIManager.DisplayTemporaryBudget(tempBudget);
        UIManager.DisplayTemporaryFramework(tempUtil);
        UIManager.DisplayTemporaryUtilities(tempFrames);

    }
}


