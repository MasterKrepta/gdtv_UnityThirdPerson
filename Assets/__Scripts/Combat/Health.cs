using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    int health;


    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int dmg)
    {

        if (health == 0) return;

        health = Mathf.Max(health - dmg, 0);
        print(this.gameObject.name + " Took damage: currently " + health.ToString());

    }
}