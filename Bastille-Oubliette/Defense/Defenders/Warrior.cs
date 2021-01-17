// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
using UnityEngine;

public class Warrior : Defender
{
    public static Warrior instance;

    private EnemySpawner enemyLaneSpawner;
    private float gestureTimer = 0f;

    private GameObject currentTarget = null;

    private void Awake()
    {
        instance = this;
        health = maxHealth;
        defenderMoveable = true;
    }

    private void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("enemyInLane", false);
        SetLaneSpawner();
    }

    private void Update()
    {
        bool attackableEnemy = IsEnemyInLane();

        if (attackableEnemy)
        {
            gameObject.GetComponent<Animator>().SetBool("timeToGesture", false);
            gameObject.GetComponent<Animator>().SetBool("enemyInLane", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("enemyInLane", false);
        }

        if (!gameObject.GetComponent<Animator>().GetBool("enemyInLane"))
        {
            gestureTimer += Time.deltaTime;
            if (gestureTimer > 3f
                && !gameObject.GetComponent<Animator>().GetBool("enemyInLane"))
            {
                gameObject.GetComponent<Animator>().SetBool("timeToGesture", true);
                StartCoroutine(WaitForGestureToComplete());
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("timeToGesture", false);
            }
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (FindObjectOfType<LoseCondition>().gameLost)
        {
            GetComponent<Animator>().SetBool("gameLost", true);
        }

        UpdateAnimationState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;

        if (collision.GetComponent<Enemy>())
        {
            Attack(otherObject);
        }
    }

    public void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
        }
    }

    public void Attack(GameObject target)
    {
        GetComponent<Animator>().SetBool("isAttacking", true);
        currentTarget = target;
    }

    private IEnumerator WaitForGestureToComplete()
    {
        yield return new WaitForSeconds(1f);
        gestureTimer = 0f;
    }

    public void SetLaneSpawner()
    {
        EnemySpawner[] enemySpawners = FindObjectsOfType<EnemySpawner>();

        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            bool isCloseEnough = (Mathf.RoundToInt(Mathf.Abs(enemySpawner.transform.position.y - transform.position.y)) <= Mathf.Epsilon);
            if (isCloseEnough)
            {
                enemyLaneSpawner = enemySpawner;
            }
        }
    }

    private bool IsEnemyInLane()
    {
        if (enemyLaneSpawner == null) { return false; }
        if (enemyLaneSpawner.transform.childCount <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("enemyInLane", false);
            return false;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("enemyInLane", true);
            return true;
        }
    }

    public void StrikeCurrentTarget(float damage)
    {
        if (!currentTarget) { return; }
        Enemy enemy = currentTarget.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.DealDamage(damage);
        }
    }
}
