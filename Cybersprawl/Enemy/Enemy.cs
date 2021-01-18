// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float moveSpeed = 1f;

    // Reference Cache
    private new Rigidbody2D rigidbody2D;

    // Methods
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsFacingRight())
        {
            rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        float scaleOffset = 3f;
        if (Mathf.Sign(rigidbody2D.velocity.x) == -1)
        {
            scaleOffset = -3f;
        }
        // Negative sign creates an opposite left to right, right to left effect
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x) + scaleOffset), 4f);
    }
}
