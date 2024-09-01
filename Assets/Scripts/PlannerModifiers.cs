using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlannerModifiers : MonoBehaviour
{
    public GameObject levelObj;
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        //levelManager = levelObj.GetComponent<levelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTextSlot(int slot, Card card)
    {
        if (slot >= 3)
        {
            Debug.Log("only three slots!");
        }
        else
        {
            Transform textField = transform.Find("Text Slot" + slot);
            if (textField != null)
            {
                //textField.text = card.description;
            }
        }
    }
}
