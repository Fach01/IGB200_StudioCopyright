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
        
        AudioManager.instance.PlaySFX("Shuffle");

        for (int i = 0; i < 4; i++)
        {
            playerManager.DrawCard();
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
    IEnumerator DrawCards()
    {

       yield return null;
    }
    IEnumerator PlayPhaseAnimation()
    {
        // animation will play at the start of the play phase to show all cards in play
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {

            if (playFieldManager.cards[i] == null)
            {
                // open Set EndTurnAnimation to active then wait for entry animation
                UIManager.EndTurnAnimation.SetActive(true);
                yield return new WaitForSeconds(0.3f);

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
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {
            yield return new WaitForSeconds(1f); // waits at the start to keep consistent timing

            if (playFieldManager.cards[i] == null)
            {
                //after animation deactivate EndTurnPanel
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Exit", true);

                //waits for exit animation to play before resuming with the rest of the code
                yield return new WaitForSeconds(1f);

                //apply changes in budge frames and utilities
                turnBudget -= tempBudget;
                utilitiesCount += tempUtil;
                frameworksCount += tempFrames;

                ResetTempPools(); // reset the temporary pools

                // Displays changes to the UI
                UIManager.SetBudgetText(turnBudget.ToString()); 
                UIManager.SetUtilitiesText(utilitiesCount.ToString());
                UIManager.SetFrameworksText(frameworksCount.ToString());
                
                yield break;
            }

            int cost = playFieldManager.cards[i].GetComponent<CardManager>().m_card.cost;
            int utilities = playFieldManager.cards[i].GetComponent<CardManager>().m_card.utilities;
            int frameworks = playFieldManager.cards[i].GetComponent<CardManager>().m_card.frameworks;

            tempBudget -= cost;
            tempUtil += utilities;
            tempFrames += frameworks;

            UIManager.DisplayTemporaryBudget(tempBudget);
            UIManager.DisplayTemporaryUtilities(tempUtil);
            UIManager.DisplayTemporaryFramework(tempFrames);

            // if the card gives utilities or frameworks an animation will play
            if (utilities > 0)
            {
                Debug.Log("utilities:" + utilities);
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Util", true); 
               

            }

            if (frameworks > 0)
            {
                Debug.Log("Frameworks:" + frameworks);
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Frames", true);

            }

            //wait for 0.05 seconds to turn off animation to stop looping
            yield return new WaitForSeconds(0.05f);
            UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Util", false);
            UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Gives Frames", false);
        }
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


