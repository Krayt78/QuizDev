using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodyAnimationController : MonoBehaviour
{
    Animator animator;
    IEnumerator MoveCodyWhileInAnimation()
    {
        yield return new WaitUntil(() => animator.GetBool("currentlyJumping") == false);
    }
}
