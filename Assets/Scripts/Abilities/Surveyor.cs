public class Surveyor : Ability
{
    bool isActive = false;
    public override void ActivateAbility()
    {
        isActive = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        cost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            //if framework card is played, card framework = *2
        }
    }
}
