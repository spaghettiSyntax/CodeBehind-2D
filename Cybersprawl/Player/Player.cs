// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;

    // State
    public bool isAlive = true;
    private bool isIdlingOnLadder = false;
    private Vector2 damageKick = new Vector2(25f, 10f);

    // Reference Cache
    [HideInInspector] public Animator animator;
    private new Rigidbody2D rigidbody2D;
    private CapsuleCollider2D capsuleCollider2D;
    private BoxCollider2D boxCollider2D;
    private float gravityScaleAtStart;
    private SpriteRenderer spriteRenderer;
    private Sprite staticClimbingSprite;

    // Methods
    private void Start()
    {
        // Cache gameObject's Rigidbody on Start
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isAlive", true);
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidbody2D.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (FindObjectOfType<GameManager>() != null)
        {
            if (FindObjectOfType<GameManager>().playerJustDamaged)
            {
                BlinkSprite();
            }
            else
            {
                MakeSurePlayerSpriteIsNormalizedAfterBlink();
            }
        }        
        CheckForPlayerInput();
        CheckIfPlayerHasHorizontalSpeed();

        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            isIdlingOnLadder = false;
            animator.enabled = true;
        }

        if (isIdlingOnLadder)
        {
            spriteRenderer.sprite.Equals(staticClimbingSprite);
        }
        else
        {
            TrackClimbSprite();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (FindObjectOfType<GameManager>() == null) { return; }

        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("DeathHazards")))
        {
            FindObjectOfType<GameManager>().playerHealth = 0;
            FindObjectOfType<GameManager>().ManagePlayerDamage();
        }

        if (FindObjectOfType<GameManager>().playerJustDamaged) { return; }

        if (capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }
    }

    private void CheckForPlayerInput()
    {
        if (!isAlive) { return; }

        Run();
        Jump();
        Climb();
    }

    private void CheckIfPlayerHasHorizontalSpeed()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            FlipSprite();
        }
    }

    private void FlipSprite()
    {
        float scaleOffset = 0.25f;
        if (Mathf.Sign(rigidbody2D.velocity.x) == 1)
        {
            scaleOffset = -0.25f;
        }
        transform.localScale = new Vector2((Mathf.Sign(rigidbody2D.velocity.x) + scaleOffset), 0.75f);
    }

    private void TrackClimbSprite()
    {
        staticClimbingSprite = spriteRenderer.sprite;
    }

    private void Run()
    {
        // -1 to + 1
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 runVelocity = new Vector2((controlThrow * runSpeed), rigidbody2D.velocity.y);
        rigidbody2D.velocity = runVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        { 
            return; 
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidbody2D.velocity += jumpVelocityToAdd;

            ////TODO: Figure out Jumping animation
            //animator.SetBool("isJumping", true);
        }

        //if (collider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        //{
        //    //TODO: Figure out Jumping animation
        //    animator.SetBool("isJumping", false);
        //}
    }

    private void Climb()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon;

        if (!boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animator.SetBool("isClimbing", false);
            rigidbody2D.gravityScale = gravityScaleAtStart;
            return;
        }

        if (boxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))
            && !playerHasVerticalSpeed)
        {
            isIdlingOnLadder = true;
            animator.enabled = false;
        }
        else
        {
            isIdlingOnLadder = false;
            animator.enabled = true;
        }

        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidbody2D.velocity.x, controlThrow * climbSpeed);
        rigidbody2D.velocity = climbVelocity;
        rigidbody2D.gravityScale = 0f;

        animator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        DamageKick();
        damageDealer.Hit();
    }

    public void BlinkSprite()
    {
        if (FindObjectOfType<GameManager>().playerJustDamaged)
        {
            Color justDamagedBlink = spriteRenderer.color;
            if (justDamagedBlink.g == 255)
            {
                justDamagedBlink.g = 0;
                justDamagedBlink.b = 0;
            }
            else
            {
                justDamagedBlink.g = 255;
                justDamagedBlink.b = 255;
            }
            spriteRenderer.color = justDamagedBlink;
        }
    }

    private void MakeSurePlayerSpriteIsNormalizedAfterBlink()
    {
        Color justDamagedBlinkRecovery = spriteRenderer.color;
        justDamagedBlinkRecovery.g = 255;
        justDamagedBlinkRecovery.b = 255;
        spriteRenderer.color = justDamagedBlinkRecovery;
    }

    public void Die()
    {
        DamageKick();
        isAlive = false;
        animator.SetBool("isAlive", false);
    }

    private void DamageKick()
    {
        animator.SetTrigger("damageKick");
        GetComponent<Rigidbody2D>().velocity = damageKick;
    }
}
