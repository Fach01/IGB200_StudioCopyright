using UnityEngine;

public class DrawCard : MonoBehaviour
{
    public GameObject deckObj;
    private Deck deck;
    // Start is called before the first frame update
    void Start()
    {
        deck = deckObj.GetComponent<Deck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deck != null && deck.cardDeck.Count <= 0)
        {

        }
    }
}
