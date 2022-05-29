using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    private List<Collider> collidedWith = new List<Collider>();
    private int dmg;

    private void OnEnable()
    {
        collidedWith.Clear();
    }

    public void SetAttack(int Damage)
    {
        dmg = Damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;

        if (collidedWith.Contains(other)) { return; }
        collidedWith.Add(other);    

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(dmg);
        }
    }
}
