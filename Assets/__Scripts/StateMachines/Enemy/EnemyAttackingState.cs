using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    readonly int ATTACK_HASH = Animator.StringToHash("Attack");


    private const float CROSSFADE_DUR = 0.1F;
    private const float ANIM_DAMP_TIME = 0.1F;
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)  {  }

    public override void Enter()
    {
        _stateMachine.Weapon.SetAttack(_stateMachine.AttackDmg, _stateMachine.AttackKnockback);
        //Debug.Log("IN ATTACK RANGE");
        _stateMachine.Anim.CrossFadeInFixedTime(ATTACK_HASH, CROSSFADE_DUR);
    }
    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(_stateMachine.Anim) >= 1)
        {
            _stateMachine.SwitchState(new EnemyChasingState(_stateMachine));
            
        }
    }

    public override void Exit()
    {
        //Debug.Log("OUT OF ATTACK RANGE");
    }

    }

