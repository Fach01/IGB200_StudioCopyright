using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        description = "For every Framework card you play, double the output.";
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
