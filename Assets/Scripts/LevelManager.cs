using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject hand;
    private HandController handController;

    public float budget = 300000;

    public TMP_Text budgetText;
    public TMP_Text utilText;
    public TMP_Text frameworkText;
    public GameObject activePlannerMods; 

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

    private Vector3[] plannerCardSlots = new Vector3[3];
    private GameObject[] activePlannerCards = new GameObject[3];


    // Start is called before the first frame update
    void Start()
    {
        handController = hand.GetComponent<HandController>();
        budgetText.text = "Budget: " + budget;
        BeginLevel();

        // bad practice but found these through trial and error lol
        plannerCardSlots[0] = new Vector3(-158f, 47.5f, 120f);
        plannerCardSlots[1] = new Vector3(-117.5f, 47.5f, 120f);
        plannerCardSlots[2] = new Vector3(-140f, 4f, 120f);
    }

    // Update is called once per frame
    void Update()
    {
        if (turn)
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

        }

    }

    void BeginLevel()
    {
        Debug.Log("begin level called");
        for (int i = 0; i < 4; i++)
        {
            handController.DrawCard();

        }
        Turn();
    }

    void Turn()
    {
        //for cards in active planner cards
        //get planners ability and enable
        //
        Debug.Log("starting turn");
        turn = true;
        
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

    private bool plannersFull()
    {
        foreach (GameObject card in activePlannerCards)
        {
            if (card == null)
            {
                return false;
            }
        }
        return true;
    }

    public void Play(GameObject currentCard)
    {
        Card cardDetails = currentCard.GetComponent<CardManager>().card;
        Spend(cardDetails.cost);

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
            // if card is planner do planner things
            if (plannersFull())
            {
                Debug.Log("planner cards are full");
                // one can be discarded, pick between them
            }
            else
            {
                handController.hand.Remove(currentCard);
                
                for (int i = 0; i < activePlannerCards.Length; i++)
                {
                    Debug.Log("for loop round " + i);
                    if (activePlannerCards[i] == null)
                    {
                        activePlannerCards[i] = currentCard;
                        currentCard.transform.position = plannerCardSlots[i];
                        UpdateTextSlot(i, cardDetails);
                        break;
                    }
                }

                Transform child = currentCard.transform.Find("Glow");
                if (child != null)
                {
                    cardGlow = child.gameObject;
                    cardGlow.SetActive(false);
                }
            }
            handController.ReorderCards(handController.hand);
        }

    }
    public void UpdateTextSlot(int slot, Card card)
    {
        if (slot >= 3)
        {
            Debug.Log("only three slots!");
        }
        else
        {
            string textSlot = "Text Slot " + slot;
            Debug.Log("time to add the modifier");
            Transform textField = activePlannerMods.transform.Find(textSlot);
            if (textField != null)
            {
                Debug.Log("found text field");
                textField.GetComponent<TextMeshProUGUI>().text = card.description;
            }
        }
    }
}
