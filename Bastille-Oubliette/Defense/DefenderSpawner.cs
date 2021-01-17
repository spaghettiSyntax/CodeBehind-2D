// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    public static DefenderSpawner instance;

    private const string DEFENDER_CONTAINER_NAME = "DefenderContainer";

    private GameObject defenderContainer = null;
    private Defender defenderPrefab = null;
    private float offset = 0.25f;

    [HideInInspector] public string defendersTag;

    // Defenders
    private bool rangerBeardedSpawned = false;
    //private string rangerBeardedObject = "RangerBearded";
    private bool sorceressSpawned = false;
    //private string sorceressObject = "Sorceress";
    private bool warriorManSpawned = false;
    //private string warriorManObject = "WarriorMan";
    private bool clericWomanSpawned = false;
    //private string clericWomanObject = "ClericWoman";
    private bool rogueManSpawned = false;
    //private string rogueManObject = "RogueMan";

    // Structures
    private bool altarSwordSpawned = false;
    // private string altarSwordObject = "AltarSword";
    private bool altarSnakeSpawned = false;
    // private string altarSnakeObject = "AltarSnake";

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CreateDefenderParent();
    }

    private void CreateDefenderParent()
    {
        defenderContainer = GameObject.Find(DEFENDER_CONTAINER_NAME);
        if (!defenderContainer)
        {
            defenderContainer = new GameObject(DEFENDER_CONTAINER_NAME);
        }
    }

    private void OnMouseDown()
    {
        AttemptToPlaceDefender(GetSquareClicked());
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = SnapToGrid(worldPos);
        return gridPos;
    }

    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = Mathf.RoundToInt(rawWorldPos.y);
        return new Vector2(newX, (newY + offset));
    }

    public void SetSelectedDefender(Defender selectedDefender)
    {
        defenderPrefab = selectedDefender;
    }

    public void AttemptToPlaceDefender(Vector2 gridPos)
    {
        if (defenderPrefab == null) { return; }

        GoldDisplay goldDisplay = FindObjectOfType<GoldDisplay>();
        int defenderCost = defenderPrefab.GetGoldCost();

        if (goldDisplay.HaveEnoughGold(defenderCost))
        {
            goldDisplay.SpendGold(defenderCost);

            switch (defenderPrefab.tag.ToString())
            {
                case "RangerBearded":
                    if (rangerBeardedSpawned) 
                    {
                        MoveDefender(gridPos);
                        break; 
                    }
                    else
                    {
                        SpawnDefender(gridPos);
                        rangerBeardedSpawned = true;
                        break;          
                    }
                case "Sorceress":
                    if (sorceressSpawned)
                    {
                        MoveDefender(gridPos);
                        break;
                    }
                    else
                    {
                        SpawnDefender(gridPos);
                        sorceressSpawned = true;
                        break;
                    }
                case "WarriorMan":
                    if (warriorManSpawned)
                    {
                        MoveDefender(gridPos);
                        break;
                    }
                    else
                    {
                        SpawnDefender(gridPos);
                        warriorManSpawned = true;
                        break;
                    }
                case "ClericWoman":
                    if (clericWomanSpawned)
                    {
                        MoveDefender(gridPos);
                        break;
                    }
                    else
                    {
                        SpawnDefender(gridPos);
                        clericWomanSpawned = true;
                        break;
                    }
                case "RogueMan":
                    if (rogueManSpawned)
                    {
                        MoveDefender(gridPos);
                        break;
                    }
                    else
                    {
                        SpawnDefender(gridPos);
                        rogueManSpawned = true;
                        break;
                    }
                case "AltarSword":
                    if (altarSwordSpawned)
                    {
                        MoveDefender(gridPos);
                        break;
                    }
                    else
                    {
                        SpawnDefender(gridPos);
                        altarSwordSpawned = true;
                        break;
                    }
                case "AltarSnake":
                    if (altarSnakeSpawned)
                    {
                        MoveDefender(gridPos);
                        break;
                    }
                    else
                    {
                        SpawnDefender(gridPos);
                        altarSnakeSpawned = true;
                        break;
                    }
                default:
                    break;
            }
        }
    }

    private void SpawnDefender(Vector2 gridPos)
    {
        Defender newDefender = Instantiate(defenderPrefab, gridPos, Quaternion.identity) as Defender;
        //Debug.Log("Defender gridPos: " + newDefender.transform.position.x + " " + newDefender.transform.position.y);

        newDefender.transform.parent = defenderContainer.transform;
    }

    private void MoveDefender(Vector2 gridPos)
    {
        Defender[] defenderArray = FindObjectsOfType<Defender>();
        for (int i = 0; i < defenderArray.Length; i++)
        {
            if (defenderArray[i].CompareTag(defendersTag))
            {
                defenderPrefab = defenderArray[i];
                if (defenderPrefab.defenderMoveable)
                {
                    defenderPrefab.transform.position = gridPos;
                    switch (defendersTag)
                    {
                        case "RangerBearded":
                            Ranger.instance.SetLaneSpawner();
                            break;
                        case "Sorceress":
                            Sorcere_r_ss.instance.SetLaneSpawner();
                            break;
                        case "ClericWoman":
                            Cleric.instance.SetLaneSpawner();
                            break;
                        case "WarriorMan":
                            Warrior.instance.SetLaneSpawner();
                            break;
                        case "RogueMan":
                            Rogue.instance.SetLaneSpawner();
                            break;
                        default:
                            break;
                    }
                }                
            }
        }
    }

    public void ResetDefenderSpawnBool(string destroyedDefender)
    {
        switch (destroyedDefender)
        {
            case "RangerBearded":
                rangerBeardedSpawned = false;
                break;
            case "Sorceress":
                sorceressSpawned = false;
                break;
            case "WarriorMan":
                warriorManSpawned = false;
                break;
            case "ClericWoman":
                clericWomanSpawned = false;
                break;
            case "RogueMan":
                rogueManSpawned = false;
                break;
            default:
                break;
        }
    }
}
