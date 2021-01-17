// (~˘▾˘)~ spaghettiSyntax
using System.Collections;
using UnityEngine;

public class Sorcere_r_ss : Defender
{
    public static Sorcere_r_ss instance;

    private const string PROJECTILE_CONTAINER_NAME = "ProjectileContainer";

    [SerializeField] private GameObject blueSpellShot = null;
    [SerializeField] private GameObject spellPosition = null;

    private EnemySpawner enemyLaneSpawner;
    private float gestureTimer = 0f;
    private GameObject projectileContainer = null;

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
        CreateProjectileContainer();
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
    }

    private void CreateProjectileContainer()
    {
        projectileContainer = GameObject.Find(PROJECTILE_CONTAINER_NAME);
        if (!projectileContainer)
        {
            projectileContainer = new GameObject(PROJECTILE_CONTAINER_NAME);
        }
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

    public void ShootSpell()
    {
        GameObject newSpell = Instantiate(blueSpellShot, spellPosition.transform.position, transform.rotation);
        newSpell.transform.parent = projectileContainer.transform;
    }
}
