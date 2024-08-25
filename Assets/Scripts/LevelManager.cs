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
        
    }

    void BeginLevel()
    {
        Debug.Log("begin level called");
        for (int i = 0; i < 4; i++)
        {
            handController.DrawCard();
        }
        // turn()
    }

    void Turn()
    {
        //for cards in active planner cards
        //get planners ability and enable
        //
        
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
}
