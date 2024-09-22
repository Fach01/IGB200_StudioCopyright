using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
using UnityEngine.Playables;

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
        UIManager.EndTurnAnimation.SetActive(true);
        // animation will play at the start of the play phase to high light all cards in play
        for (int i = 0; i < playFieldManager.cards.Count; i++)
        {
            if (playFieldManager.cards[i] == null)
            {
                continue;
            }
            playFieldManager.cards[i].GetComponent<CardManager>().cardanimator.SetBool("Add Resource", true);

        }
        //then the tallying of all the resources 
            for (int i = 0; i < playFieldManager.cards.Count; i++)
        {
            if (playFieldManager.cards[i] == null)
            {
                continue;
            }
            //card animation plays
            
            int cost = playFieldManager.cards[i].GetComponent<CardManager>().m_card.cost;
            int utilities = playFieldManager.cards[i].GetComponent<CardManager>().m_card.utilities;
            int frameworks = playFieldManager.cards[i].GetComponent<CardManager>().m_card.frameworks;

            turnBudget -= cost;
            utilitiesCount += utilities;
            frameworksCount += frameworks;

            
        }
        // TODO: add budged turn loss text animation

        UIManager.SetBudgetText(turnBudget.ToString());
        UIManager.SetUtilitiesText(utilitiesCount.ToString());
        UIManager.SetFrameworksText(frameworksCount.ToString());

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
}


