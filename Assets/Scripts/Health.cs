using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 20;

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>(); //Get the DamageDelear component from the collided object
        if (damageDealer != null) //Check if the collided object has a DamageDealer component
        {
            TakeDamage(damageDealer.GetDamage()); //Call the TakeDamage method with the damage value from the DamageDealer
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
