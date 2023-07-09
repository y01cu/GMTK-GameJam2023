using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //play hurt animation

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        //die animation

        GetComponent<Collider>().enabled = false;
        this.enabled = false;
    }









}
