using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlannerComponent : MonoBehaviour
{
    private GameObject selectedPlanner;
    private GameObject cardGlow;

    public GameObject confirmButton;
    public GameObject levelObj;
    public GameObject playField;
    public GameObject hand;
    private LevelManager levelManager;

    private GameObject newPlannerCard = null;

    // Start is called before the first frame update
    void Awake()
    {
        levelManager = levelObj.GetComponent<LevelManager>();
 
    }

    // Update is called once per frame
    void Update()
    {
        if (newPlannerCard == null)
        {
            newPlannerCard = levelManager.selectedCard;
        }
      
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

                //potentially loop through planner cards here to make sure this card is in the list

                selectedPlanner = hit.collider.gameObject;

                if (selectedPlanner != null)
                {
                    

                    confirmButton.SetActive(true);

                    Transform child = selectedPlanner.transform.Find("Glow");
                    if (child != null)
                    {
                        cardGlow = child.gameObject;
                        cardGlow.SetActive(true);
                    }

                }
            }
        }
    }

    public void OnCancel()
    {
        if (cardGlow != null)
        {
            cardGlow.SetActive(false);
            cardGlow = null;
        }
        
        selectedPlanner = null;

        levelManager.playButton.SetActive(false);
        levelManager.selectedCard = null;
        this.gameObject.SetActive(false);
    }

    public void OnConfirm()
    {
        Destroy(selectedPlanner);
        if (playField.GetComponent<PlayFieldManager>().ReplaceCard(newPlannerCard, selectedPlanner))
        {
            hand.GetComponent<HandController>().hand.Remove(newPlannerCard);


            for (int i = 0; i < 3; i++)
            {
                if (playField.GetComponent<PlayFieldManager>().GetCard(i) == newPlannerCard)
                {
                    levelManager.UpdateTextSlot(i, newPlannerCard.GetComponent<CardManager>().card);
                    break;
                }
            }

            levelManager.playButton.SetActive(false);
            levelManager.selectedCard = null;
            this.gameObject.SetActive(false);
        }

        
    }




}
