using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 20;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake; //Reference to the CameraShake script
    AudioPlayer audioPlayer; //Reference to the AudioPlayer script

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>(); //Get the CameraShake component from the main camera
        audioPlayer = FindObjectOfType<AudioPlayer>(); //Find the AudioPlayer in the scene
    } 

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>(); //Get the DamageDelear component from the collided object
        if (damageDealer != null) //Check if the collided object has a DamageDealer component
        {
            TakeDamage(damageDealer.GetDamage()); //Call the TakeDamage method with the damage value from the DamageDealer
            PlayHitEffect();
            audioPlayer.PlayDamageClip(); //Play the damage sound effect
            ShakeCamera(); 
            damageDealer.Hit(); //Call the Hit method on the DamageDealer to destroy it 
        }

    }

    void TakeDamage(int damage)
    {
        health -= damage; //Subtract the damage from the health
        if (health <= 0)
        {
            Destroy(gameObject); //Destroy the game object if health is less than or equal to 0
        }
    }

    void PlayHitEffect()
    {                           //Create      //Reference   //current pos       // current pos
        ParticleSystem instance = Instantiate (hitEffect, transform.position, Quaternion.identity); //Instantiate the hit effect at the position of the object
                //particle                  //to ensure particle is done playing
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax); 
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play(); //Call the Play method on the CameraShake script to shake the camera
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
