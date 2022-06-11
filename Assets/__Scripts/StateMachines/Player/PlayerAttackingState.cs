using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;
    float previousFrameTime;
    private bool appliedForce = false;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {

        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        _stateMachine.Anim.CrossFadeInFixedTime(attack.AnimName, attack.TransitionDur);
        _stateMachine.WeaponDamage.SetAttack(attack.Damage);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime > attack.ForceTime)
            {
                TryApplyForce(normalizedTime);
            }

            if (_stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            //go to locomotion
            if (_stateMachine.Targeter.CurrentTarget != null)
            {
                _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
            }
            else
            {
                _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
            }
        }

      

        previousFrameTime = normalizedTime;
    }

    

    public override void Exit()
    {
        
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo =   _stateMachine.Anim.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = _stateMachine.Anim.GetNextAnimatorStateInfo(0);

        if (_stateMachine.Anim.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        } 
        else  if (!_stateMachine.Anim.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < attack.ComboAttackTime) { return; }
        
        _stateMachine.SwitchState
        (
            new PlayerAttackingState(_stateMachine, attack.ComboStateIndex)
        );
    }

    private void TryApplyForce(float normalizedTime)
    {
        if (appliedForce) return;

        appliedForce = true;
        _stateMachine.ForceReceiver.AddForce(_stateMachine.transform.forward * attack.Force);
    }

}
