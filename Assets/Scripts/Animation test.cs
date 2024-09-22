using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationtest : MonoBehaviour
{
    public Animator controller;
    public string parameter = "";
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            controller.SetBool(parameter, true);
        }
        else if (Input.GetKeyUp(KeyCode.O))
        {
            controller.SetBool(parameter, false);
        }
    }
}
