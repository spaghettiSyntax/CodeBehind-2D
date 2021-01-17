// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.UI;

public class DefenderButton : MonoBehaviour
{
    public static DefenderButton instance;

    [SerializeField] private Defender defenderPrefab = null;
    [SerializeField] private Text costText = null;

    [HideInInspector] public string buttonTag;
    [HideInInspector] public string defenderSelectedTag;

    private void Awake()
    {
        instance = this;
        ResetButtons();
    }

    private void Start()
    {
        LabelButtonWithCost();
    }

    private void Update()
    {

    }

    private void LabelButtonWithCost()
    {
        if (costText == null) { return; }
        costText.text = defenderPrefab.GetGoldCost().ToString() + "G";
    }

    private void OnMouseDown()
    {
        GetTag();
        ResetButtons();
        SelectOrDeselectButton();
    }

    private void GetTag()
    {
        buttonTag = gameObject.tag;
    }

    public void ResetButtons()
    {
        DefenderButton[] buttons = FindObjectsOfType<DefenderButton>();
        foreach (DefenderButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = new Color32(41, 41, 41, 255);
        }
    }

    private void SelectOrDeselectButton()
    {
        if (defenderPrefab != null)
        {
            if (buttonTag.Equals("RangerBearded"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[0].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[0].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (buttonTag.Equals("Sorceress"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[1].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[1].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (buttonTag.Equals("WarriorMan"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[2].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[2].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (buttonTag.Equals("ClericWoman"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[3].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[3].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (buttonTag.Equals("RogueMan"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[4].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[4].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (buttonTag.Equals("AltarSword"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[5].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[5].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (buttonTag.Equals("BattleButton6"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[6].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[6].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (buttonTag.Equals("BattleButton7"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[7].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[7].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            if (buttonTag.Equals("BattleButton8"))
            {
                if (BattleCanvasButtonBGs.instance.defenderButtonBGs[8].GetComponent<SpriteRenderer>().color != Color.white)
                {
                    BattleCanvasButtonBGs.instance.defenderButtonBGs[8].GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            DefenderSpawner.instance.defendersTag = defenderPrefab.tag;
        }

        if (GetComponent<SpriteRenderer>().color != Color.white)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        FindObjectOfType<DefenderSpawner>().SetSelectedDefender(defenderPrefab);        
    }
}
