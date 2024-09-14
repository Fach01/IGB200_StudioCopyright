using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class UIManager : MonoBehaviour
{
    public GameObject player;

    public GameObject budgetObject;
    public GameObject budgetTurnLoss;
    public GameObject phase;
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

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    // Update is also called when anything in the scene is changed
    private void Update()
    {
        // to be called by another script
        UpdateTurnLossPosition();
    }

    public void SetBudgetText(string budget)
    {
        TMP_Text budgetText = budgetObject.GetComponent<TMP_Text>();
        budgetText.text = "Budget: " + budget;
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

    public void SetPhaseText(Phase phase)
    {
        TMP_Text phaseText = this.phase.GetComponent<TMP_Text>();
        phaseText.text = "Phase: " + phase.ToString();
    }

    public void UpdateTurnLossPosition()
    {
        TMP_Text budgetText = budgetObject.GetComponent<TMP_Text>();
        float textOffset = budgetText.preferredWidth + 15f;
        budgetTurnLoss.transform.position = new Vector3(budgetObject.transform.position.x + textOffset, budgetObject.transform.position.y - 10f, budgetObject.transform.position.z);
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
        actionPointsTextObject.text = actionPointsText;
    }
}
