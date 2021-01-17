// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class Defender : MonoBehaviour
{
    [SerializeField] private int goldCost = 50;
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float health = 100f;

    [HideInInspector] public bool defenderMoveable = true;

    private void Awake()
    {
        defenderMoveable = true;
        health = maxHealth;
    }

    public int GetGoldCost()
    {
        return goldCost;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void DealDamage(float damageToDeal)
    {
        health -= damageToDeal;
        if (health <= 0)
        {
            defenderMoveable = false;
            gameObject.GetComponent<Animator>().SetBool("healthIsZero", true);
        }
    }

    public void DestroyDefender()
    {
        DefenderButton.instance.ResetButtons();
        Destroy(gameObject);
        DefenderSpawner.instance.ResetDefenderSpawnBool(gameObject.tag.ToString());
    }
}
