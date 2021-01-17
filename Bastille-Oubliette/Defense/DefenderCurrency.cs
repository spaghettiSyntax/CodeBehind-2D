using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderCurrency : MonoBehaviour
{
    public void AddGold(int amount)
    {
        FindObjectOfType<GoldDisplay>().AddGold(amount);
    }
}
