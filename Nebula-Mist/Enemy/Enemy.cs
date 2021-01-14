// (~˘▾˘)~ spaghettiSyntax
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private int health = 100;
    [SerializeField] private int scoreValue = 150;

    [Header("Enemy Weapons")]
    [SerializeField] private float shotCounter;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject projectilePrefab = null;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private int projectileCount = 0;

    [Header("Enemy VFX/SFX")]
    [SerializeField] private GameObject deathVFX = null;
    [SerializeField] private float deathVFXExplosionDuration = 1f;
    [SerializeField] private AudioClip deathSFX = null;
    [Range(0, 1)][SerializeField] private float deathSFXVolume = 0.7f;
    [SerializeField] private AudioClip shootSFX = null;
    [Range(0, 1)] [SerializeField] private float shootSFXVolume = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3, 3), -projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DamageDealer damageDealer = GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }

        if (collision.CompareTag("PlayerWeapon"))
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        //health -= damageDealer.GetDamage();
        health -= 10;

        if (health <= 0)
        {
            Die();
        }

        damageDealer.Hit();
    }

    private void Die()
    {        
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, deathVFXExplosionDuration);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        GameSession.instance.AddToScore(scoreValue);
    }
}
