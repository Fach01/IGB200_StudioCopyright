using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationtest : MonoBehaviour
{
    // Update is called once per frame
    public GameObject? arrow;
    public void Exit()
    {
        if (arrow == null)
        {
            Animator animator = GetComponent<Animator>();
            animator.SetBool("Exit", false);
            this.gameObject.SetActive(false);
        }
        else
        {
            arrow.SetActive(false);
        }
       
    }
    public void Entry()
    {
        arrow.SetActive(true);
    }
}
