// (~˘▾˘)~ spaghettiSyntax
using System;
using System.Collections;
using UnityEngine;

public class Cleric : Defender
{
    public static Cleric instance;

    [SerializeField] private GameObject healSpell = null;
    [SerializeField] private GameObject spellPosition = null;
    [SerializeField] private AudioClip areaSpellSFX = null;
    [SerializeField] private GameObject areaSpellPosition = null;
    [SerializeField] private float areaSpellHealAmount = 20f;

    private EnemySpawner enemyLaneSpawner;
    private float gestureTimer = 0f;
    private float healTimer = 0f;
    private bool defenderNotMaxHealthSomewhereOnGrid = false;
    private bool defenderWithinRange = false;

    private void Awake()
    {
        instance = this;
        health = maxHealth;
        defenderMoveable = true;
    }

    private void Start()
    {
        SetLaneSpawner();
    }

    private void Update()
    {
        //bool attackableEnemy = IsEnemyInLane();

        if (defenderWithinRange
            && defenderNotMaxHealthSomewhereOnGrid)
        {
            gameObject.GetComponent<Animator>().SetBool("timeToGesture", false);
            gameObject.GetComponent<Animator>().SetBool("isHealing", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isHealing", false);
        }

        //if (attackableEnemy)
        //{
        //    gameObject.GetComponent<Animator>().SetBool("timeToGesture", false);
        //    gameObject.GetComponent<Animator>().SetBool("isHealing", true);
        //}
        //else
        //{
        //    gameObject.GetComponent<Animator>().SetBool("isHealing", false);
        //}

        if (!gameObject.GetComponent<Animator>().GetBool("isHealing"))
        {
            gestureTimer += Time.deltaTime;
            if (gestureTimer > 3f
                && !gameObject.GetComponent<Animator>().GetBool("isHealing"))
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

    private IEnumerator WaitForGestureToComplete()
    {
        yield return new WaitForSeconds(1f);
        gestureTimer = 0f;
    }

    public void HealSpell()
    {
        // Instantiate healSpell on cleric
        SelfHeal();

        // Instantiate healSpell on all available gridPos surrounding cleric
        AreaHeal();
    }

    private void SelfHeal()
    {
        Instantiate(healSpell, spellPosition.transform.position, transform.rotation);
        health += areaSpellHealAmount;
    }

    private void AreaHeal()
    {
        // Initial Spell Position
        Vector3 newSpellPosition = new Vector3(spellPosition.transform.position.x,
                                               spellPosition.transform.position.y,
                                               spellPosition.transform.position.z);
        areaSpellPosition.transform.position = newSpellPosition;

        // Heal clockwise starting north

        newSpellPosition = GetNorthSpellPosition();
        CheckIfGridPosHasDefenderAtNewSpellPosition(newSpellPosition);

        newSpellPosition = GetNorthEastSpellPosition();
        CheckIfGridPosHasDefenderAtNewSpellPosition(newSpellPosition);

        newSpellPosition = GetEastSpellPosition();
        CheckIfGridPosHasDefenderAtNewSpellPosition(newSpellPosition);

        newSpellPosition = GetSouthEastSpellPosition();
        CheckIfGridPosHasDefenderAtNewSpellPosition(newSpellPosition);

        newSpellPosition = GetSouthSpellPosition();
        CheckIfGridPosHasDefenderAtNewSpellPosition(newSpellPosition);

        newSpellPosition = GetSouthWestSpellPosition();
        CheckIfGridPosHasDefenderAtNewSpellPosition(newSpellPosition);

        newSpellPosition = GetWestSpellPosition();
        CheckIfGridPosHasDefenderAtNewSpellPosition(newSpellPosition);

        newSpellPosition = GetNorthWestSpellPosition();
        CheckIfGridPosHasDefenderAtNewSpellPosition(newSpellPosition);

        GetComponent<AudioSource>().PlayOneShot(areaSpellSFX);

        //// Special accidental note! Awesome diaganol attack below
        //GameObject areaSpell = spellPosition;
        //Vector3 newSpellPosition = new Vector3(areaSpell.transform.position.x - 1,
        //                                       areaSpell.transform.position.y + 1,
        //                                       areaSpell.transform.position.z);
        //areaSpell.transform.position = newSpellPosition;
        //Instantiate(healSpell, areaSpell.transform.position, transform.rotation);
    }

    private Vector3 GetNorthWestSpellPosition()
    {
        // NW
        return new Vector3(spellPosition.transform.position.x - 1,
                           spellPosition.transform.position.y + 1,
                           spellPosition.transform.position.z);
    }

    private Vector3 GetWestSpellPosition()
    {
        // W
        return new Vector3(spellPosition.transform.position.x - 1,
                           spellPosition.transform.position.y,
                           spellPosition.transform.position.z);
    }

    private Vector3 GetSouthWestSpellPosition()
    {
        // SW
        return new Vector3(spellPosition.transform.position.x - 1,
                           spellPosition.transform.position.y - 1,
                           spellPosition.transform.position.z);
    }

    private Vector3 GetSouthSpellPosition()
    {
        // S
        return new Vector3(spellPosition.transform.position.x,
                           spellPosition.transform.position.y - 1,
                           spellPosition.transform.position.z);
    }

    private Vector3 GetSouthEastSpellPosition()
    {
        // SE
        return new Vector3(spellPosition.transform.position.x + 1,
                           spellPosition.transform.position.y - 1,
                           spellPosition.transform.position.z);
    }

    private Vector3 GetEastSpellPosition()
    {
        // E
        return new Vector3(spellPosition.transform.position.x + 1,
                           spellPosition.transform.position.y,
                           spellPosition.transform.position.z);
    }

    private Vector3 GetNorthEastSpellPosition()
    {
        // NE
        return new Vector3(spellPosition.transform.position.x + 1,
                           spellPosition.transform.position.y + 1,
                           spellPosition.transform.position.z);
    }

    private Vector3 GetNorthSpellPosition()
    {
        // N
        return new Vector3(spellPosition.transform.position.x,
                           spellPosition.transform.position.y + 1,
                           spellPosition.transform.position.z);
    }

    private void CheckIfGridPosHasDefenderAtNewSpellPosition(Vector3 newSpellPosition)
    {
        Defender[] defenders = FindObjectsOfType<Defender>();
        for (int i = 0; i < defenders.Length; i++)
        {
            if (defenders[i].transform.position == newSpellPosition)
            {
                //Debug.Log(defenders[i].tag);
                defenderWithinRange = true;
                InstantiateAreaHealAtNewSpellPosition(newSpellPosition);
                AttemptToHealDefender(defenders[i]);
            }
        }
    }

    public void CheckIfAnyDefenderNeedsHealing()
    {
        if (health < maxHealth)
        {
            defenderWithinRange = true;
            defenderNotMaxHealthSomewhereOnGrid = true;
            SelfHeal();
        }
        healTimer += 1f;
        Defender[] defenders = FindObjectsOfType<Defender>();
        for (int i = 0; i < defenders.Length; i++)
        {
            if (defenders[i].health < defenders[i].maxHealth)
            {
                defenderNotMaxHealthSomewhereOnGrid = true;
                AreaHeal();
            }
            else
            {
                if (healTimer > 2f)
                {
                    gameObject.GetComponent<Animator>().SetBool("isHealing", false);
                    healTimer = 0f;
                    defenderWithinRange = false;
                    defenderNotMaxHealthSomewhereOnGrid = false;
                }
            }
        }
    }

    private void InstantiateAreaHealAtNewSpellPosition(Vector3 newSpellPosition)
    {
        areaSpellPosition.transform.position = newSpellPosition;
        Instantiate(healSpell, areaSpellPosition.transform.position, transform.rotation);
    }

    private void AttemptToHealDefender(Defender defender)
    {
        defender.health += areaSpellHealAmount;
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

    //private bool IsEnemyInLane()
    //{
    //    if (enemyLaneSpawner == null) { return false; }
    //    if (enemyLaneSpawner.transform.childCount <= 0)
    //    {
    //        gameObject.GetComponent<Animator>().SetBool("isHealing", false);
    //        return false;
    //    }
    //    else
    //    {
    //        gameObject.GetComponent<Animator>().SetBool("isHealing", true);
    //        return true;
    //    }
    //}
}
