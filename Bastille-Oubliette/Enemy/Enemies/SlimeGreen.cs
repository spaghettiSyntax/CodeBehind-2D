﻿using UnityEngine;

public class SlimeGreen : Slime
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherObject = collision.gameObject;
        if (collision.GetComponent<Defender>())
        {
            GetComponent<Enemy>().Attack(otherObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentTarget = null;
    }
}
