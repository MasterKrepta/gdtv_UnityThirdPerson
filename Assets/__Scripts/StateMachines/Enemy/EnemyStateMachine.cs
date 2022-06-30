using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public float ChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDmg { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }



    public GameObject Player { get; private set; }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamge;
        Health.OnDie += HandleDie;
        
    }

    private void HandleTakeDamge()
    {
        SwitchState(new EnemyImpactState(this));
    }
    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamge;
        Health.OnDie += HandleDie;
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ChasingRange);
    }
}
