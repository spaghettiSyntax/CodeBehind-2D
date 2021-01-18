// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // Methods
    public void Hit()
    {
        if (FindObjectOfType<GameManager>() == null) { return; }
        FindObjectOfType<GameManager>().ManagePlayerDamage();
    }
}
