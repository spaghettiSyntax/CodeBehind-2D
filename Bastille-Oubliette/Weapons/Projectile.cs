// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float damage = 10f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.DealDamage(damage);
            Destroy(gameObject);
        }
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
