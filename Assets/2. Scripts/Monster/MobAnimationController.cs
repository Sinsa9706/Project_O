using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimationController : MonoBehaviour
{
    private Animator animator;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsAttacking = Animator.StringToHash("IsSearching");
    private static readonly int IsAway = Animator.StringToHash("IsAway");

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(bool moving)
    {
        animator.SetBool(IsMoving, moving);
    }

    public void Attack()
    {
        animator.SetTrigger(IsAttacking);
    }

    public void Away() 
    {
        animator.SetTrigger(IsAway);
    }
}
