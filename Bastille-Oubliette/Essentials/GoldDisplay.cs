// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;
using UnityEngine.UI;

public class GoldDisplay : MonoBehaviour
{
    [SerializeField] private int gold = 100;

    private int maxGold = 500;
    private Text goldText;

    private void Start()
    {
        goldText = GetComponent<Text>();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        goldText.text = gold.ToString() + "G";
    }

    public void AddGold(int amount)
    {
        if ((gold + amount) <= maxGold)
        {
            gold += amount;
            UpdateDisplay();
        }
        else
        {
            gold = 500;
            UpdateDisplay();
        }
    }

    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateDisplay();
        }        
    }

    public bool HaveEnoughGold(int amount)
    {
        return gold >= amount;
    }
}
