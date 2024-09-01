using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject hand;
    private HandController handController;

    public float budget = 300000;

    public TMP_Text budgetText;
    public TMP_Text utilText;
    public TMP_Text frameworkText;
    

    public int frameworkGoal;
    public int utilGoal;
    private int currentFramework = 0;
    private int currentUtil = 0;

    private bool turn = false;
    public GameObject playButton;

    [HideInInspector]
    public GameObject selectedCard = null;
    [HideInInspector]
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
    // private Vector3[] plannerCardSlots = new Vector3[3];
    // private GameObject[] activePlannerCards = new GameObject[3];

    public int AP { get => actionPoints; }

    public GameObject playfield;
    public GameObject plannerComponents;
    private GameObject activePlannerMods;
    private GameObject replacePlannerPanel;

    private void Awake()
    {
        handController = hand.GetComponent<HandController>();
        activePlannerMods = plannerComponents.transform.Find("Planner Modifiers").gameObject;
        replacePlannerPanel = plannerComponents.transform.Find("Replace Card").gameObject;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        replacePlannerPanel.SetActive(false);
        budgetText.text = "Budget: " + budget;
        BeginLevel();

        // bad practice but found these through trial and error lol
        /* plannerCardSlots[0] = new Vector3(-158f, 47.5f, 120f);
        plannerCardSlots[1] = new Vector3(-117.5f, 47.5f, 120f);
        plannerCardSlots[2] = new Vector3(-140f, 4f, 120f); */
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
                PlayEvents();
                break;
            case Phase.End:
                OnTurnEnd();
                break;
            default: // we shouldn't hit this but we can fix it later if we do somehow
                break;
        }
        // phaseText.GetComponent<TMP_Text>().text = "turn: " + turnnumber + " phase: " + phase.ToString();
    }

    void BeginLevel()
    {
        Debug.Log("begin level called");
        actionPoints = 4;
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

    // TODO: move this selection logic to the containers with an event listener
    /* ^ maybe? i'm thinking like playphase should call a method that handles player input
     * maybe in the playercontroller or playermanager
     * then we handle playing or selecting another card there, same with drawing cards
     * we should be able to call like playermanager.select(card) or something? discuss later
     */
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
    }

    public void EndPlay()
    {
        phase = Phase.Event;
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

    // TODO: playercontroller for spending money, recieving resources from cards
    public void Spend(float value)
    {
        if (actionPoints <= 0)
        {
            Debug.Log("No action points!");
            return;
        }
        budget -= value;
        if (budget <= 0)
        {
            // game over
        }
        budgetText.text = "Budget: " + budget;
    }

    private bool plannersFull()
    {
        return false;
    }

    // TODO: move to cardmanager
    public void Play(GameObject currentCard)
    {

        actionPoints--;

        // add discarding planner cards

        Card cardDetails = currentCard.GetComponent<CardManager>().card;
        Spend(cardDetails.cost); // move this to after card validation

        if (!cardDetails.IsPlanner())
        {

            currentFramework += cardDetails.framework;
            currentUtil += cardDetails.utilities;
            frameworkText.text = "Framework: " + currentFramework;
            utilText.text = "Utilities: " + currentUtil;
            handController.DeleteCard(currentCard);
        }
        else
        {
            // move glow to card behaviour script
            Transform child = currentCard.transform.Find("Glow");
            if (child != null)
            {
                cardGlow = child.gameObject;
                cardGlow.SetActive(false);
            }

            if (playfield.GetComponent<PlayFieldManager>().AddCard(currentCard))
            {
                handController.hand.Remove(currentCard);
                // currentCard.transform.SetParent(null, false);

                int temp = 0;

                for (int i = 0; i < activePlannerMods.transform.childCount; i++)
                {
                    if (activePlannerMods.transform.GetChild(i).name.Contains("Text Slot"))
                    {
                        Transform textField = activePlannerMods.transform.GetChild(i);
                        if (playfield.GetComponent<PlayFieldManager>().GetCount() <= temp) continue;

                        Card card = playfield.GetComponent<PlayFieldManager>().GetCard(temp).GetComponent<CardManager>().card;
                        textField.GetComponent<TextMeshProUGUI>().text = card.description;
                        temp += 1;
                    }
                }
            }
            else
            {
                replacePlannerPanel.SetActive(true);
            }

            handController.ReorderCards(handController.hand);
        }
    }
}
