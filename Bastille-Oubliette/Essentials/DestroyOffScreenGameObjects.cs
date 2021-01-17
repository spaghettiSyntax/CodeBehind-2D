// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class DestroyOffScreenGameObjects : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            FindObjectOfType<LoseCondition>().ReduceCounter();
        }

        Destroy(collision.gameObject);
    }
}
