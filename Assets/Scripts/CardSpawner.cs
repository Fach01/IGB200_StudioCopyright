using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        string[] cardAssets = AssetDatabase.FindAssets("t:Card");

        int rows = Mathf.CeilToInt(cardAssets.Length / 4f); // 4 cards per row

        int width = 12 * 4;
        int height = 15 * rows;

        float midx = (12f * 3f) / 2f;
        float midy = (15f * (rows - 1)) / 2f;


        for (int i = 0; i < cardAssets.Length; i++) {
            Card cardAsset = (Card)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(cardAssets[i]), typeof(Card));
            // x width: 10, y width: 13

            int row = Mathf.CeilToInt(i / 4f);
            int cardsInRow = row == rows ? cardAssets.Length % 4 : 4;
            Vector3 position = new Vector3(width / cardsInRow * (i % 4) - midx, height / rows * row - midy, 0f);

            GameObject card = Instantiate(cardPrefab, position, transform.rotation, transform);
            CardManager cardManager = card.GetComponent<CardManager>();
            cardManager.SetCost(cardAsset.cost.ToString());
            cardManager.SetName(cardAsset.name);
            cardManager.SetDescription(cardAsset.description);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
