using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationtest : MonoBehaviour
{
    // Update is called once per frame
    public void Exit()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("Exit", false);
        this.gameObject.SetActive(false);
    }
}
