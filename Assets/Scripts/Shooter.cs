using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab; //Prefab for the projectile
    [SerializeField] float projectileSpeed = 10f; //Speed of the projectile
    [SerializeField] float projectileLifetime = 5f; //Lifetime of the projectile
    [SerializeField] float fireRate = 0.5f; //Rate of fire
    Coroutine fireCoroutine;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] float baseFiringRate = 0.5f; //Base firing rate

    AudioPlayer audioPlayer;
    public bool isFiring;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            // StopAllCoroutines();
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }

    }
    IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            GameObject projectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity
            );//Create a new projectile
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); //Get the Rigidbody2D component of the projectile
            rb.velocity = transform.up * projectileSpeed; //Set the velocity of the projectile

            Destroy(projectile, projectileLifetime); //Destroy the projectile after its lifetime
            yield return new WaitForSeconds(fireRate); //Wait for the fire rate before firing again

            float timeToNextProjectile = Random.Range(
                baseFiringRate - firingRateVariance,
                baseFiringRate + firingRateVariance
            );

            timeToNextProjectile = Mathf.Clamp(
                timeToNextProjectile,
                minimumFiringRate,
                float.MaxValue
            );
            audioPlayer.PlayShootingClip(); //Play the shooting sound
            yield return new WaitForSeconds(baseFiringRate); //Wait for the fire rate before firing again
        }
    }
}
