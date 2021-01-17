using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCanvasButtonBGs : MonoBehaviour
{
    public static BattleCanvasButtonBGs instance;
    public GameObject[] defenderButtonBGs = null;

    private void Awake()
    {
        instance = this;
    }
}
