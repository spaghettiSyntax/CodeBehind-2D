// (~˘▾˘)~ spaghettiSyntax
using System;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private AudioClip breakingSound = null;
    [SerializeField] private GameObject blockBreakParticles = null;
    [SerializeField] private Sprite[] damageSprites = null;

    private int timesBlockWasHit;

    private void Start()
    {
        if (CompareTag("Breakable"))
        {
            LevelManager.instance.CountBreakableBlocks();
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        PlayVFXAndSFX();
        timesBlockWasHit++;
        int maxHits = damageSprites.Length + 1;
        if (timesBlockWasHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextDamageLevelSprite();
        }
    }

    private void ShowNextDamageLevelSprite()
    {
        int spriteIndex = timesBlockWasHit - 1;
        if (damageSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = damageSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        LevelManager.instance.BlockDestroyed();
        GameSession.instance.AddToScore();
        Destroy(gameObject);
    }

    private void PlayVFXAndSFX()
    {
        TriggerBreakParticles();
        AudioSource.PlayClipAtPoint(breakingSound, Camera.main.transform.position);
    }

    private void TriggerBreakParticles()
    {
        GameObject particles = Instantiate(blockBreakParticles, transform.position, transform.rotation);
        Destroy(particles, 1f);
    }
}
