using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public GameObject level;
    public LevelManager levelManager;

    public GameObject hand;
    private HandController handController;
    // Start is called before the first frame update
    void Start()
    {
        handController = hand.GetComponent<HandController>();
        levelManager = level.GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickDraw()
    {
        levelManager.Spend(10000);
        handController.DrawCard();
    }

    public void OnCancelPlay()
    {
        levelManager.selectedCard = null;
        levelManager.playButton.SetActive(false);
    }

    public void OnPlay()
    {
        levelManager.Play(levelManager.selectedCard);
        levelManager.playButton.SetActive(false);
    }
}
