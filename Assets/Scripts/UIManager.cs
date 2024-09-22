using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class UIManager : MonoBehaviour
{
    public GameObject player;

    public GameObject budgetObject;
    public GameObject budgetTurnLoss;
    public GameObject phase;
    public GameObject turn;
    public GameObject utilities;
    public GameObject frameworks;
    public GameObject buildingPlane;
    public GameObject playField;
    public GameObject hand;
    public GameObject discard;
    public GameObject deck;
    public GameObject endTurn;
    public GameObject actionPoints;
    public GameObject replaceCard;
    public GameObject win;
    public GameObject lose;

    public GameObject EndTurnAnimation;
    public int tempBudget = 0;
    public int tempFramework = 0;
    public int tempUtilities = 0;
    public TMP_Text Budget;
    public TMP_Text Framework;
    public TMP_Text Utilities;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    // Update is also called when anything in the scene is changed
    private void Update()
    {
    }

    public void SetBudgetText(string budget)
    {
        TMP_Text budgetText = budgetObject.GetComponent<TMP_Text>();
        budgetText.text = "Budget: " + budget;

        UpdateTurnLossPosition();
    }

    public void SetBudgetTurnLossText(string budget)
    {
        TMP_Text budgetText = budgetTurnLoss.GetComponent<TMP_Text>();
        budgetText.text = budget;
        if (!budgetText.text.Contains("-"))
        {
            budgetText.text = "+" + budgetText.text;
        }
    }

    public void UpdateTurnLossPosition()
    {
        TMP_Text budgetText = budgetObject.GetComponent<TMP_Text>();
        float textOffset = budgetText.preferredWidth + 15f;
        budgetTurnLoss.transform.position = new Vector3(budgetObject.transform.position.x + textOffset, budgetObject.transform.position.y - 10f, budgetObject.transform.position.z);
    }

    public void SetPhaseText(Phase phase)
    {
        TMP_Text phaseText = this.phase.GetComponent<TMP_Text>();
        phaseText.text = "Phase: " + phase.ToString();
    }
    
    public void SetTurnText(int turn)
    {
        TMP_Text turnText = this.turn.GetComponent<TMP_Text>();
        turnText.text = "Turn: " + turn;
    }

    public void SetUtilitiesText(string utilitiesText)
    {
        TMP_Text utilitiesTextObject = utilities.GetComponent<TMP_Text>();
        utilitiesTextObject.text = "Utilities: " + utilitiesText;
    }

    public void SetFrameworksText(string frameworksText)
    {
        TMP_Text frameworksTextObject = frameworks.GetComponent<TMP_Text>();
        frameworksTextObject.text = "Frameworks: " + frameworksText;
    }

    public void SetActionPointsText(string actionPointsText)
    {
        TMP_Text actionPointsTextObject = actionPoints.GetComponent<TMP_Text>();
        actionPointsTextObject.text = actionPointsText + "/2";
    }
    // the following 3 methods will display each increase and decrease of the budget at the end of the round 
    
    public void DisplayTemporaryBudget(int Loss)
    {
        tempBudget += Loss;
        string BudgetLoss = tempBudget.ToString();
        Budget.text = BudgetLoss;
    }
    public void DisplayTemporaryFramework(int Gain)
    {
        tempFramework += Gain;
        string FrameGain = tempFramework.ToString();
        Framework.text = FrameGain;
    }
    public void DisplayTemporaryUtilities(int Gain)
    {
        tempUtilities += Gain;
        string UtilGain = tempUtilities.ToString();
        Utilities.text = UtilGain;
    }
    public void ResetTemp()
    {
        tempBudget = 0;
        tempFramework = 0;
        tempUtilities = 0;  
    }
}
