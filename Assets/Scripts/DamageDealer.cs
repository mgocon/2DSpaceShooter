using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{

    [SerializeField] int damage = 10;

    public int GetDamage()
    {
        return damage; //Return the damage value
    }//Method to get the damage value of the DamageDealer

    public void Hit()
    {
        Destroy(gameObject); //Destroy the game object
    }
}
