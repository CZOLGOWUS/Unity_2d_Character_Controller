using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipSprite(bool isFliped)
    {
        spriteRenderer.flipX = isFliped;
    }

    public void StartJumpingAnimation()
    {
        animator.SetBool("IsJumping", true);
    }

    public void EndJumpingAnimation()
    {
        animator.SetBool("IsJumping", false);
    }

    public void SetRunningVelocity(float velocityX)
    {
        animator.SetFloat("RunningVelocity", Mathf.Abs(velocityX));
        FlipSprite(velocityX > Mathf.Epsilon ? false : true);
    }

    public void SetFallingAnimation(bool isFalling)
    {
        animator.SetBool("IsFalling", isFalling);
    }

}
