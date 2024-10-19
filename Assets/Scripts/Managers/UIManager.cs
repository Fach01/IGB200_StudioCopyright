using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
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

    public TMP_Text highscore;
    public GameObject pauseScreen;

    public GameObject EndTurnAnimation;

    public TMP_Text Budget;
    public TMP_Text Framework;
    public TMP_Text Utilities;

    public GameObject CardInfo;
    public TMP_Text CardDescription;
    public GameObject Fade;


    public List<Sprite> buildingimages;

    public GameObject? Tutorial; 
    public TMP_Text? tutorialText;

    // Start is called before the first frame update
    private void Start()
    {

        GameManager.instance.Foreground = Fade;
        StartCoroutine(GameManager.instance.TransitionIn());
    }
    private void Awake()
    {

    }
    public void NextLevel()
    {
        GameManager.instance.NextLevel();
    }
    public void ReturnToMain()
    {
        GameManager.instance.ReturntoMain();

    }
    public void Restart()
    {
        GameManager.instance.Restart();
    }
    public void Pause(int budget, bool on)
    {
       if (on) AudioManager.instance.PlaySFX("Nail");
        highscore.text = budget.ToString();
        pauseScreen.SetActive(on);
    }

    // Update is called once per frame
    // Update is also called when anything in the scene is changed
    private void Update()
    {
    }
    public void ChangeScene(string sceneName)
    {
        GameManager.instance.ChangeScene(sceneName);
    }
    public void SetBudgetText(string budget)
    {
        TMP_Text budgetText = budgetObject.GetComponent<TMP_Text>();
        budgetText.text = "Budget: $" + budget;

        UpdateTurnLossPosition();
    }

    public void SetBudgetTurnLossText(string budget)
    {
        TMP_Text budgetText = budgetTurnLoss.GetComponent<TMP_Text>();
        budgetText.text = budget;
        if (!budgetText.text.Contains("-"))
        {
            budgetText.text = "+" + Constants.convertBigNumber(budgetText.text);
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
        turnText.text = "Shift: " + turn;
    }

    public void SetUtilitiesText(string utilitiesText, string utilGoal)
    {
        TMP_Text utilitiesTextObject = utilities.GetComponent<TMP_Text>();
        utilitiesTextObject.text = "Utilities: " + Constants.convertBigNumber(utilitiesText) + "/" + utilGoal;
    }

    public void SetFrameworksText(string frameworksText, string frameGoal)
    {
        TMP_Text frameworksTextObject = frameworks.GetComponent<TMP_Text>();
        frameworksTextObject.text = "Frameworks: " + Constants.convertBigNumber(frameworksText) + "/" + frameGoal;
    }

    public void SetActionPointsText(string actionPointsText)
    {
        TMP_Text actionPointsTextObject = actionPoints.GetComponent<TMP_Text>();
        actionPointsTextObject.text = actionPointsText + "/2";
    }
    // the following 3 methods will display each increase and decrease of the budget at the end of the round 
    
    public void DisplayTemporaryBudget(int Loss)
    {
        string BudgetLoss = Loss.ToString();
        Budget.text = BudgetLoss;
    }
    public void DisplayTemporaryFramework(int Gain)
    {
        string FrameGain = Gain.ToString();
        Framework.text = FrameGain;
    }
    public void DisplayTemporaryUtilities(int Gain)
    {
        string UtilGain = Gain.ToString();
        Utilities.text = UtilGain;
    }
    public void TutorialActive(string tutText)
    {
        tutorialText.text = tutText;
    }
    public void ChangePlane(Sprite developmentStage)
    {
        buildingPlane.GetComponent<Image>().sprite = developmentStage;
    }
    public void HighlightCard(GameObject Cards)
    {
        CardInfo.SetActive(true);
 
        Card carddetails = Cards.GetComponent<CardManager>().m_card;
        CardDescription.text = $"Ability Cost: {carddetails.abilityCost}\n" +
            $"Ability Name: {carddetails.abilityName}\n" +
            $"Ability Description: {carddetails.ability.Description(carddetails.abilityLevel)}";

    }
    public void DeHighlightCard()
    {
        CardInfo.SetActive(false);
    }
}
