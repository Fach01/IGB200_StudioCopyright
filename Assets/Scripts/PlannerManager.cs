using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlannerManager : MonoBehaviour
{
    private GameObject selectedPlanner;
    private GameObject cardGlow;
    public GameObject confirmButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
}
