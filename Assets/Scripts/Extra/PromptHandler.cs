using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HidePrompt(GameObject panel)
    {
        panel.SetActive(false);
        //GameObject.Find("Level Manager").GetComponent<EventManager>().eventActive = false;
        GameObject.Find("Level Manager").GetComponent<LevelManager>().player.GetComponent<PlayerManager>().phase = Phase.End;
    }
}
