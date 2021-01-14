// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage = 0.1f;

    public float GetDamage()
    {
        return damage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
