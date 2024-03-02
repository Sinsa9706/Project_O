using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimationController : MonoBehaviour
{
    private Animator animator;

    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

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

    public void Dead() 
    {
        animator.SetTrigger(IsDead);
    }
}
