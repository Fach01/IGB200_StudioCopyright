using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardManager : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DiscardCurrentCard()
    {
        player.GetComponent<PlayerManager>().DiscardCard();
    }
}
