using UnityEngine;

public class Slime : Enemy
{
    [Range(0.1f, 5f)] private float currentJump = 0.692f;
    [Range(0.1f, 5f)] private float currentRoll = 0.9f;

    public bool isJumping = false;
    public bool isRolling = false;

    private new void Update()
    {
        if (isJumping)
        {
            JumpForward();
        }

        if (isRolling)
        {
            RollForward();
        }

        if (GetComponent<Animator>().GetBool("isAttacking"))
        {
            isJumping = false;
            isRolling = false;
        }

        UpdateAnimationState();
    }

    private void JumpForward()
    {
        transform.Translate(Vector2.left * currentJump * Time.deltaTime);
    }

    private void RollForward()
    {
        transform.Translate(Vector2.left * currentRoll * Time.deltaTime);
    }

    public void StartJump()
    {
        isJumping = true;
    }

    public void DoneJumping()
    {
        isJumping = false;
    }

    public void StartRoll()
    {
        isRolling = true;
    }

    public void DoneRolling()
    {
        isRolling = false;
    }
}
