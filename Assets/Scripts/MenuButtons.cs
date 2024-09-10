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
}
