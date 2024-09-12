using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;

public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject uiManager;

    public int startBudget;
    public int utilitiesGoal;
    public int frameworksGoal;

    private PlayerController PlayerController;
    private UIManager UIManager;

    private int levelBudget;
    private int turnBudget;
    private int utilitiesCount;
    private int frameworksCount;

    void Awake()
    {
        PlayerController = player.GetComponent<PlayerController>();
        UIManager = uiManager.GetComponent<UIManager>();

        BeginLevel();
    }

    
    void BeginLevel()
    {
        levelBudget = startBudget;
        turnBudget = levelBudget;
        utilitiesCount = 0;
        frameworksCount = 0;
        PlayerController.phase = Phase.Setup;
    }

    // Update is called once per frame
    void Update()
    {
        switch (PlayerController.phase)
        {
            case Phase.Setup:
                StartTurn();
                break;
            case Phase.Play:
                PlayPhase();
                break;
            case Phase.Event:
                PlayEvents();
                break;
            case Phase.End:
                OnTurnEnd();
                break;
            default:
                break;
        }
    }

    public void StartTurn()
    {
        PlayerController.phase = Phase.Play;
    }

    public void PlayPhase()
    {
        PlayerController.phase = Phase.Event;
    }

    public void PlayEvents()
    {
        // go through events queue
        PlayerController.phase = Phase.End;
    }

    public void OnTurnEnd()
    {
        PlayerController.phase = Phase.Setup;
    }
}


