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
    public bool levelEvents;
    public EventManager eventManager;

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

    public int turn = 1;
    public bool phaseplaying = false;

    public GameObject win;
    public GameObject lose;

    public int highscore = 0;
    [Header("Tutorial stuff")]

    public GameObject tutorial;
    public bool levelWon = false;
    public bool tutorialplayed = false;

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
        if(level == level.level1)
        {
            highscore = buildingManager.BudgetPoolLevel1;
        }
        if (level == level.level2)
        {
            highscore = buildingManager.BudgetPoolLevel2;
            startBudget += buildingManager.BudgetPoolLevel1;
        }
        if (level == level.level3)
        {
            highscore = buildingManager.BudgetPoolLevel3;
            startBudget += buildingManager.BudgetPoolLevel2;
        }
        AudioManager.instance.PlayMusic("LevelMusic");
        BeginLevel();
    }

    // Update is called once per frame
    private void Update()
    {
        if (phaseplaying) return;
        switch (playerManager.phase)
        {
            case Phase.PreTurn:
                StartCoroutine(PreTurn());
                break;
            case Phase.Setup:
                SetupPhase();
                break;
            case Phase.Play:
                PlayPhase();
                break;
            case Phase.Event:
                StartCoroutine(EventPhase());
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

        StartCoroutine(playerManager.DrawXCards(4));

        UIManager.SetTurnText(turn);
        UIManager.SetBudgetText(levelBudget.ToString("N0"));
        UIManager.SetUtilitiesText("0", utilitiesGoal.ToString());
        UIManager.SetFrameworksText("0", frameworksGoal.ToString());

        playerManager.phase = Phase.PreTurn;
    }
    public IEnumerator PreTurn()
    {
        phaseplaying = true;
        turnBudget = levelBudget;

        if(turn != 1) playerManager.DrawCard(); // if its not the very first round draw a card
        yield return new WaitForSeconds(1f); //waits for the animation to stop

        playerManager.ResetPlayer();

        playerManager.phase = Phase.Setup; // sets next phase
        phaseplaying = false;
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
    }
    public IEnumerator EventPhase()
    {
        phaseplaying = true;
        if (!levelEvents)
        {
            playerManager.phase = Phase.End;
            phaseplaying = false;
            yield break; }
        if (!eventManager.eventActive)
        {
            if (eventManager.currentEvent == "Sick Day")
            {
                for (int i = 0; i < playFieldManager.cards.Count; i++)
                {
                    if (playFieldManager.cards[i] == null) continue;
                    CardManager cardManager = playFieldManager.cards[i].GetComponent<CardManager>();
                    if (cardManager.sick)
                    {
                        cardManager.sick = false;
                    }
                }
            } else if (eventManager.currentEvent == "Flood")
            {
                // if not 1 count down
                // otherwise remove all cards from playfield
            }

            if (tutorial != null && tutorialplayed == false) // 100% chance in tutorial level 3 of an event playing once otherwise there is a chance of it occuring
            {
                
                eventManager.nextEvent = GameEvent.SickDay;
                eventManager.PlayEvent();
                tutorialplayed = true;
            }
            else
            {
                var chance = Random.Range(0f, 1f);
                if (chance > 0.75f)
                {
                    eventManager.nextEvent = chance > 0.9f ? GameEvent.Flood : GameEvent.SickDay;
                }
                eventManager.PlayEvent();
            }

            
        }
    }

    public void EndPhase()
    {
        phaseplaying = true;
        if (utilitiesCount > utilitiesGoal / 2 && frameworksCount > frameworksCount / 2 && level == level.level1)
        {
            UIManager.ChangePlane(UIManager.buildingimages[1]);
        }
        else if (utilitiesCount > utilitiesGoal / 3 && frameworksCount > frameworksCount / 3 && level == level.level2)
        {
            UIManager.ChangePlane(UIManager.buildingimages[2]);
        }
        else if (utilitiesCount > (utilitiesGoal / 3)*2 && frameworksCount > (frameworksCount / 3) *2 && level == level.level2)
        {
            UIManager.ChangePlane(UIManager.buildingimages[3]);
        }
        else if(utilitiesCount > (utilitiesGoal / 3) * 2 && frameworksCount > (frameworksCount / 3) * 2 && level == level.level3)
        {
            UIManager.ChangePlane(UIManager.buildingimages[4]);
        }

        levelBudget = turnBudget;
        turn += 1;
        UIManager.SetBudgetText(levelBudget.ToString("N0"));
        UIManager.SetUtilitiesText(utilitiesCount.ToString(), utilitiesGoal.ToString());
        UIManager.SetFrameworksText(frameworksCount.ToString(), frameworksGoal.ToString());
        UIManager.SetTurnText(turn);

        playerManager.phase = Phase.PreTurn;
        phaseplaying = false;

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
        if (budget < highscore) return;
        if (level == level.level1) { PlayerPrefs.SetInt(buildingManager.buildingname + "level1", budget); }
        else if (level == level.level2) { PlayerPrefs.SetInt(buildingManager.buildingname + "level2", budget); }
        else if (level == level.level3) { PlayerPrefs.SetInt(buildingManager.buildingname + "level3", budget); }
    }
    IEnumerator PlayPhaseAnimation()
    {
        phaseplaying = true;
        // animation will play at the start of the play phase to show all cards in play
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {

            if (playFieldManager.cards[i] == null)
            {
                //then start the tallying of the gains and losses
                StartCoroutine(TallyGainsAndLosses());

                yield break;
            }
            if (playFieldManager.cards[i].GetComponent<CardManager>().sick)
            {
                continue;
            }
            playFieldManager.cards[i].GetComponent<CardManager>().cardanimator.SetBool("Add Resource", true);
            yield return new WaitForSeconds(.2f); 
        }
    }
    IEnumerator TallyGainsAndLosses()
    {
        // open EndTurnAnimation 
        UIManager.EndTurnAnimation.SetActive(true);
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {
            yield return new WaitForSeconds(1f); // waits at the start to keep consistent timing

            if (playFieldManager.cards[i] == null)
            {
                //after animation deactivate EndTurnPanel
                UIManager.EndTurnAnimation.GetComponent<Animator>().SetBool("Exit", true);

                yield return new WaitForSeconds(0.5f);

                //apply changes in budge frames and utilities
                turnBudget += tempBudget;
                utilitiesCount += tempUtil;
                frameworksCount += tempFrames;

                ResetTempPools(); // reset the temporary pools

                // Displays changes to the UI
                UIManager.SetBudgetText(turnBudget.ToString("N0")); 
                UIManager.SetUtilitiesText(utilitiesCount.ToString(), utilitiesGoal.ToString());
                UIManager.SetFrameworksText(frameworksCount.ToString(), frameworksGoal.ToString());

                if (utilitiesCount >= utilitiesGoal && frameworksCount >= frameworksGoal)
                {
                    GameManager.instance.UnlockNextLevel();
                    UpdateBuildingBudgetPool(levelBudget);
                    levelWon = true;
                    if(tutorial == null || level == level.level2) win.SetActive(true); // second level does not contain end lines but 1 and 3 do 
                }
                else if (turnBudget <= 0)
                {
                    lose.SetActive(true);
                }

                playerManager.phase = Phase.Event;
                phaseplaying = false;

                yield break;
            }

            if (playFieldManager.cards[i].GetComponent<CardManager>().sick)
            {
                continue;
            }

            int cost = playFieldManager.cards[i].GetComponent<CardManager>().m_card.cost;
            int resource = playFieldManager.cards[i].GetComponent<CardManager>().m_card.resource;
            CardType cardType = playFieldManager.cards[i].GetComponent<CardManager>().m_card.type;

            tempBudget -= cost;

            if (cardType == CardType.Utilities) tempUtil += resource;
            else if (cardType == CardType.Framework) tempFrames += resource;


            UIManager.DisplayTemporaryBudget(tempBudget);
            UIManager.DisplayTemporaryUtilities(tempUtil);
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
                AudioManager.instance.PlaySFX("Add Resource");

                playerManager.phase = Phase.Event;
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


