using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CodyAnimationController : MonoBehaviour
{
    Animator animator;
    public Transform targetLocation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public async Task MoveCodyWhileInAnimationAsync(bool isSuccessfullyJump)
    {
        animator.SetBool("currentlyJumping", true);
        animator.SetTrigger(isSuccessfullyJump ? "jumpSuccess" : "jumpFail");
        while (!WaitUntilENdOfQnimation())
        {
            await Task.Yield();
        }
        Debug.Log("Cody has finished jumping");
        animator.Play("Idle");
    }

    public IEnumerator MoveCodyWhileInAnimation(bool isSuccessfullyJump)
    {
        animator.SetBool("currentlyJumping", true);
        animator.SetTrigger(isSuccessfullyJump ? "jumpSuccess" : "jumpFail");
        yield return new WaitUntil(WaitUntilENdOfQnimation);
        Debug.Log("Cody has finished jumping");
        animator.Play("Idle");
    }

    bool WaitUntilENdOfQnimation()
    {
        return animator.GetBool("currentlyJumping") == false;
    }
}
