using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject hand;
    private Card[] activePlannerCards;
    private HandController handController;

    // Start is called before the first frame update
    void Start()
    {
        handController = hand.GetComponent<HandController>();
        BeginLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginLevel()
    {
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
}
