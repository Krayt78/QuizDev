using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodyAnimationController : MonoBehaviour
{
    Animator animator;
    public Transform targetLocation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public IEnumerator MoveCodyWhileInAnimation(bool isSuccessfullyJump)
    {
        animator.SetBool("currentlyJumping", true);
        animator.SetTrigger(isSuccessfullyJump ? "jumpSuccess" : "jumpFail");
        yield return new WaitUntil(WaitUntilENdOfQnimation);
        animator.Play("Idle");
    }

    bool WaitUntilENdOfQnimation()
    {
        return animator.GetBool("currentlyJumping") == false;
    }
}
