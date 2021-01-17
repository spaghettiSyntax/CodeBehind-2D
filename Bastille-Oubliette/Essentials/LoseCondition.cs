// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.UI;

public class LoseCondition : MonoBehaviour
{
    [SerializeField] private float loseConditionCount = 13f;
    [SerializeField] private float reductionAmount = 1f;
    [SerializeField] private GameObject goldChest = null;

    [HideInInspector] public bool gameLost = false;

    private float loseConditionCounter = 5f;
    private Text counterText = null;

    void Start()
    {
        switch (PlayerPrefsController.GetDifficulty())
        {
            // Easy
            case 0f:
                loseConditionCount = 10f;
                break;
            // Medium
            case 1f:
                loseConditionCount = 6f;
                break;
            // Hard
            case 2f:
                loseConditionCount = 3f;
                break;
            default:
                break;
        }
        loseConditionCounter = loseConditionCount;
        counterText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        counterText.text = loseConditionCounter.ToString();
    }

    public void ReduceCounter()
    {
        loseConditionCounter -= reductionAmount;
        UpdateDisplay();

        if (loseConditionCounter <= 0f)
        {
            gameLost = true;
            goldChest.GetComponent<Animator>().SetBool("gameOver", true);
            FindObjectOfType<BattleSceneController>().HandleLoseCondition();
        }
    }
}
