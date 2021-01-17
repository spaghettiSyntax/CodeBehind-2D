// (~˘▾˘)~ spaghettiSyntax
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0.1f, 5f)] private float currentSpeed = 1f;
    [SerializeField] private float health = 100f;

    public GameObject currentTarget = null;

    private void Awake()
    {
        FindObjectOfType<BattleSceneController>().EnemySpawned();
    }

    public void Update()
    {
        transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
        UpdateAnimationState();
    }

    public void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
        }

        if (FindObjectOfType<LoseCondition>().gameLost)
        {
            DestroyEnemy();
        }
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        currentSpeed = movementSpeed;
    }

    public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }

    public void StrikeCurrentTarget(float damage)
    {
        if (!currentTarget) 
        {
            currentTarget = null;
            return; 
        }
        Defender defender = currentTarget.GetComponent<Defender>();
        if (defender)
        {
            defender.DealDamage(damage);
        }
    }

    public void DealDamage(float damageToDeal)
    {
        health -= damageToDeal;
        if (health <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("healthIsZero", true);
        }
    }

    private void OnDestroy()
    {
        BattleSceneController battleSceneController = FindObjectOfType<BattleSceneController>();

        if (battleSceneController != null)
        {
            try
            {
                FindObjectOfType<BattleSceneController>().EnemyDestroyed();
            }
            catch (Exception exception)
            {
                Debug.Log(exception.Message);
            }
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
