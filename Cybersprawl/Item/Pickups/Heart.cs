// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class Heart : Pickup
{
    [SerializeField] private AudioClip heartPickUpSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(heartPickUpSFX, Camera.main.transform.position);
        Destroy(gameObject);
        if (FindObjectOfType<GameManager>() == null) { return; }
        else 
        { 
            if (FindObjectOfType<Player>().isAlive)
            {
                FindObjectOfType<GameManager>().HeartPickup();
            }
        }
    }
}
