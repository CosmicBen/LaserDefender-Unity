using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float padding = 0.5f;
    [SerializeField] private int health = 200;

    [Header("Death FX")]
    [SerializeField] private GameObject deathVfx = null;
    [SerializeField] private float deathVfxDuration = 0.5f;
    [SerializeField] private AudioClip deathSfx = null;
    [SerializeField] [Range(0.0f, 1.0f)] private float deathSfxVolume = 1.0f;

    [Header("Projectile")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private float projectileFiringPeriod = 0.1f;
    [SerializeField] private AudioClip shootSfx = null;
    [SerializeField] [Range(0.0f, 1.0f)] private float shootSfxVolume = 1.0f;

    private Coroutine fireRoutine;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private void Start()
    {
        SetupMoveBoundaries();
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            ProcessHit(damageDealer);
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
        Destroy(explosionGameObject, deathVfxDuration);

        Destroy(gameObject);

        if (deathSfx != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(deathSfx, Camera.main.transform.position, deathSfxVolume);
        }

        Level level = FindObjectOfType<Level>();
        level.LoadGameOverDelayed();
    }

    public int GetHealth()
    {
        return health;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && fireRoutine == null)
        {
            fireRoutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1") && fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
            fireRoutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            if (laserPrefab != null)
            {
                GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, projectileSpeed);
                yield return new WaitForSeconds(projectileFiringPeriod);

                if (shootSfx != null && Camera.main != null)
                {
                    AudioSource.PlayClipAtPoint(shootSfx, Camera.main.transform.position, shootSfxVolume);
                }
            }
        }
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(deltaX, deltaY);

        if (movement.sqrMagnitude > 1)
        {
            movement.Normalize();
        }

        movement *= Time.deltaTime;
        movement *= movementSpeed;

        float newXPos = Mathf.Clamp(transform.position.x + movement.x, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + movement.y, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        if (gameCamera != null)
        {
            Vector3 cameraMins = gameCamera.ViewportToWorldPoint(Vector3.zero);
            xMin = cameraMins.x + padding;
            yMin = cameraMins.y + padding;

            Vector3 cameraMaxs = gameCamera.ViewportToWorldPoint(Vector3.one);
            xMax = cameraMaxs.x - padding;
            yMax = cameraMaxs.y - padding;
        }
    }
}
