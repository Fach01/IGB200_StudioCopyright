using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject hand;
    private HandController handController;
    // Start is called before the first frame update
    void Start()
    {
        handController = hand.GetComponent<HandController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickDraw()
    {
        handController.DrawCard();
    }
}
