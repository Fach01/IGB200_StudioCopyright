using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;

public class LevelManager : MonoBehaviour
{
    public GameObject uiManager;
    private UIManager UIManager;

    public int startBudget;
    private int levelBudget;
    private int turnBudget;

    public int utilitiesGoal;
    public int frameworksGoal;
    private int utilitiesCount;
    private int frameworksCount;

    public GameObject player;
    private PlayerController PlayerController;

    void Awake()
    {
        UIManager = uiManager.GetComponent<UIManager>();
        PlayerController = player.GetComponent<PlayerController>();

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


