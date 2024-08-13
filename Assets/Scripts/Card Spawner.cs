using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CardSpawner : MonoBehaviour
{
    private Card[] cards;

    // Start is called before the first frame update
    void Start()
    {
        string[] cardAssets = AssetDatabase.FindAssets("t:Card");
        foreach (string cardAsset in cardAssets)
        {
            Card card = (Card) AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(cardAsset), typeof(Card));
            Debug.Log(card.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
