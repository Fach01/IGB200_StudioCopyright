using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject hand;
    private Card[] activePlannerCards;
    private HandController handController;

    private float budget = 10000;
    public TMP_Text budgetText;
    public TMP_Text utilText;
    public TMP_Text frameworkText;

    public int frameworkGoal;
    public int utilGoal;

    private int currentFramework = 0;
    private int currentUtil = 0;

    private bool turn = false;
    public GameObject playButton;

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
        GameObject selectedCard = null;
        if (turn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Create a ray from the camera to the point where the mouse clicked
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Card"))
                {
                    selectedCard = hit.collider.gameObject;
                    Debug.Log("Card selected: " + selectedCard.GetComponent<CardManager>().card.name);
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

    public void Play(Card card)
    {
        Spend(card.cost);
        // if card is planner do planner things
        if (!card.planner)
        {
            card.framework += currentFramework;
            card.utilities += currentUtil;
            frameworkText.text = "Framework: " + currentFramework;
            utilText.text = "Utilities: " + currentUtil;

        }
    }
}
