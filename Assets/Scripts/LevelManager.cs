using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject hand;
    private Card[] activePlannerCards;
    private HandController handController;

    private float budget = 100000;
    public TMP_Text budgetText;
    public TMP_Text utilText;
    public TMP_Text frameworkText;

    public int frameworkGoal;
    public int utilGoal;

    private int currentFramework = 0;
    private int currentUtil = 0;

    private bool turn = false;
    public GameObject playButton;
    public GameObject selectedCard = null;
    public GameObject cardGlow = null;

    // turn/phase stuff
    public GameObject phaseText;
    private Phase phase;
    private enum Phase // play, event, end
    {
        Setup,
        Play,
        Event,
        End
        // could have an emergency phase here idk, phases need further discussion in general
    }
    private int turnnumber;
    private int actionPoints;

    // Start is called before the first frame update
    void Start()
    {
        handController = hand.GetComponent<HandController>();
        budgetText.text = "Budget: " + budget;
        BeginLevel();
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case Phase.Setup:
                StartTurn();
                break;
            case Phase.Play:
                PlayPhase();
                break;
            case Phase.Event:
                // events
                break;
            case Phase.End:
                OnTurnEnd();
                break;
            default: // we shouldn't hit this but we can fix it later if we do somehow
                break;
        }
        phaseText.GetComponent<TMP_Text>().text = "turn: " + turnnumber + " phase: " + phase.ToString();
    }

    void BeginLevel()
    {
        Debug.Log("begin level called");
        for (int i = 0; i < 4; i++)
        {
            handController.DrawCard();

        }
        phase = Phase.Setup;
    }

    void StartTurn()
    {
        //for cards in active planner cards
        //get planners ability and enable
        //
        Debug.Log("starting turn");
        // turn = true;

        actionPoints = 2;
        phase = Phase.Play;
    }

    void PlayPhase()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the point where the mouse clicked
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Card"))
            {
                if (cardGlow != null)
                {
                    cardGlow.SetActive(false);
                }

                selectedCard = hit.collider.gameObject;
                Debug.Log("Card selected: " + selectedCard.GetComponent<CardManager>().card.name);

            }
        }
        if (selectedCard != null)
        {
            playButton.SetActive(true);
            Transform child = selectedCard.transform.Find("Glow");
            if (child != null)
            {
                cardGlow = child.gameObject;
                cardGlow.SetActive(true);
            }
        }
        // phase = Phase.Event;
    }

    private void PlayEvents()
    {
        // go through events queue
        phase = Phase.End;
    }

    private void OnTurnEnd()
    {
        // add resources to total, implement later
        phase = Phase.Setup;
    }

    void LoseGame()
    {
        // if deck.size = 0
        // or budget <= 0 
        // Game over
    }

    public void Spend(float value)
    {
        budget -= value;
        if (budget <= 0)
        {
            // game over
        }
        budgetText.text = "Budget: " + budget;
    }

    public void Play(GameObject currentCard)
    {
        Card cardDetails = currentCard.GetComponent<CardManager>().card;
        Spend(cardDetails.cost);

        // if card is planner do planner things
        if (!cardDetails.IsPlanner())
        {

            currentFramework += cardDetails.framework;
            currentUtil += cardDetails.utilities;
            frameworkText.text = "Framework: " + currentFramework;
            utilText.text = "Utilities: " + currentUtil;
            Debug.Log(currentFramework);

        }
        handController.DeleteCard(currentCard);
    }
}
