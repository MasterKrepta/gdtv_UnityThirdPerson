using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;

    int health;

    public event Action OnTakeDamage;
    public event Action OnDie;

    bool isInvulnerable = false;
    public bool IsDead => health == 0;

    

    private void Start()
    {
        health = maxHealth;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(int dmg)
    {

        if (health == 0) return;

        if (isInvulnerable == true) return;

        health = Mathf.Max(health - dmg, 0);

        OnTakeDamage?.Invoke();

        if (health <= 0) OnDie?.Invoke();

        //print(this.gameObject.name + " Took damage: currently " + health.ToString());

    }
}
