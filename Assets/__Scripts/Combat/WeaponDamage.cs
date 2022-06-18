using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    private List<Collider> collidedWith = new List<Collider>();
    private int dmg;

    private float knockback;

    private void OnEnable()
    {
        collidedWith.Clear();
    }

    public void SetAttack(int Damage, float knockBack)
    {
        this.dmg = Damage;
        this.knockback = knockBack;
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

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 dir = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(dir * knockback);
        }
    }
}
