using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject uiManager;
    public GameObject deck;

    public int startBudget;
    public int utilitiesGoal;
    public int frameworksGoal;

    private PlayerManager playerManager;
    private UIManager UIManager;

    private int levelBudget;
    private int turnBudget;
    private int utilitiesCount;
    private int frameworksCount;

    private void Awake()
    {
        playerManager = player.GetComponent<PlayerManager>();
        UIManager = uiManager.GetComponent<UIManager>();
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
    }

    public void PlayPhase()
    {
        playerManager.phase = Phase.Event;
    }

    public void EventPhase()
    {
        // go through events queue
        playerManager.phase = Phase.End;
    }

    public void EndPhase()
    {
        playerManager.phase = Phase.Setup;
    }
}


