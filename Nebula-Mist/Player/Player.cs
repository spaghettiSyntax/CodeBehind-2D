// (~˘▾˘)~ spaghettiSyntax
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private Image hpImageFill = null;

    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab = null;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float projectileFiringPeriod = 0.05f;
    [SerializeField] private GameObject deathVFX = null;
    [SerializeField] private float deathVFXExplosionDuration = 1f;
    [SerializeField] private AudioClip deathSFX = null;
    [Range(0, 1)] [SerializeField] private float deathSFXVolume = 0.7f;
    [SerializeField] private AudioClip shootSFX = null;
    [Range(0, 1)] [SerializeField] private float shootSFXVolume = 0.25f;

    private Coroutine firingCoroutine;
    private Quaternion currentShipRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (!currentScene.name.Equals("0_StartMenu"))
        {
            Fire();
        }
        else
        {
            if (StartMenuController.instance.controlsCanvasObject.activeInHierarchy)
            {

            }
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, currentShipRotation) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, Quaternion.identity.z);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            ProcessHit(damageDealer);
        }

        if (collision.CompareTag("EnemyWeapon"))
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        hpImageFill.fillAmount -= damageDealer.GetDamage();

        if (hpImageFill.fillAmount <= 0)
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
        SceneLoader.instance.LoadGameOver();
    }
}
