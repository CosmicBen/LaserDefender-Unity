using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float health = 100.0f;
    [SerializeField] private int scoreValue = 150;

    [Header("Death FX")]
    [SerializeField] private GameObject deathVfx = null;
    [SerializeField] private float deathVfxDuration = 0.5f;
    [SerializeField] private AudioClip deathSfx = null;
    [SerializeField] [Range(0.0f, 1.0f)] private float deathSfxVolume = 1.0f;

    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 10.0f;
    private float shotCounter = 0;
    [SerializeField] private float minTimeBetweenShots = 0.2f;
    [SerializeField] private float maxTimeBetweenShots = 3.0f;
    [SerializeField] private AudioClip shootSfx = null;
    [SerializeField] [Range(0.0f, 1.0f)] private float shootSfxVolume = 1.0f;

    private void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);    
    }

    private void Update()
    {
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            ProcessHit(damageDealer);
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0.0f)
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (laserPrefab != null)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -projectileSpeed);
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

            if (shootSfx != null && Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(shootSfx, Camera.main.transform.position, shootSfxVolume);
            }
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject explosionGameObject = Instantiate(deathVfx, transform.position, transform.rotation);
        Destroy(explosionGameObject,deathVfxDuration);

        Destroy(gameObject);
        
        if (deathSfx != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolume);
        }

        GameSession session = FindObjectOfType<GameSession>();
        if (session != null)
        {
            session.AddToScore(scoreValue);
        }
    }
}
