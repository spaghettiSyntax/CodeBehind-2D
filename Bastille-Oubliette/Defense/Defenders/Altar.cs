using UnityEngine;

public class Altar : Defender
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
    }
}
