// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject startMenuCanvas = null;
    [SerializeField] private GameObject selectLevelCanvas = null;
    [SerializeField] private GameObject goblinRushCanvas = null;
    [SerializeField] private Text goblinRushButtonText = null;
    [SerializeField] private GameObject orcBattleCanvas = null;
    [SerializeField] private Text orcBattleButtonText = null;
    [SerializeField] private GameObject creatureAttackCanvas = null;
    [SerializeField] private Text creatureAttackButtonText = null;

    private void Awake()
    {
        if (LevelLoaderController.instance.levelSelected.Equals(""))
        {
            startMenuCanvas.SetActive(true);
            selectLevelCanvas.SetActive(false);
        }
        else
        {
            startMenuCanvas.SetActive(false);
            selectLevelCanvas.SetActive(true);
        }
                
        goblinRushCanvas.SetActive(false);
        orcBattleCanvas.SetActive(false);
        creatureAttackCanvas.SetActive(false);
    }

    public void SelectLevel()
    {
        if (!selectLevelCanvas.activeInHierarchy)
        {
            selectLevelCanvas.SetActive(true);
            startMenuCanvas.SetActive(false);
        }
        else
        {
            DisableSelectLevelCanvas();
            startMenuCanvas.SetActive(true);
        }
    }

    private void DisableSelectLevelCanvas()
    {
        LevelLoaderController.instance.levelSelected = "";
        selectLevelCanvas.SetActive(false);
        goblinRushCanvas.SetActive(false);
        orcBattleCanvas.SetActive(false);
        creatureAttackCanvas.SetActive(false);
    }

    public void GoblinRush()
    {
        if (!goblinRushCanvas.activeInHierarchy)
        {
            LevelLoaderController.instance.levelSelected = "GoblinRush";
            goblinRushCanvas.SetActive(true);
            goblinRushButtonText.color = Color.green;

            orcBattleCanvas.SetActive(false);
            orcBattleButtonText.color = Color.black;
            creatureAttackCanvas.SetActive(false);
            creatureAttackButtonText.color = Color.black;
        }
        else
        {
            goblinRushCanvas.SetActive(false);
            goblinRushButtonText.color = Color.black;
        }
    }

    public void OrcBattle()
    {
        if (!orcBattleCanvas.activeInHierarchy)
        {
            LevelLoaderController.instance.levelSelected = "OrcBattle";
            orcBattleCanvas.SetActive(true);
            orcBattleButtonText.color = Color.green;

            goblinRushCanvas.SetActive(false);
            goblinRushButtonText.color = Color.black;
            creatureAttackCanvas.SetActive(false);
            creatureAttackButtonText.color = Color.black;
        }
        else
        {
            orcBattleCanvas.SetActive(false);
            orcBattleButtonText.color = Color.black;
        }
    }

    public void CreatureAttack()
    {
        if (!creatureAttackCanvas.activeInHierarchy)
        {
            LevelLoaderController.instance.levelSelected = "CreatureAttack";
            creatureAttackCanvas.SetActive(true);
            creatureAttackButtonText.color = Color.green;

            goblinRushCanvas.SetActive(false);
            goblinRushButtonText.color = Color.black;
            orcBattleCanvas.SetActive(false);
            orcBattleButtonText.color = Color.black;
        }
        else
        {
            creatureAttackCanvas.SetActive(false);
            creatureAttackButtonText.color = Color.black;
        }
    }

    public void BeginLevel()
    {
        SceneLoader.instance.ExitScene("2_BattleScene");
    }
}
