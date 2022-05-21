using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    readonly int TARGETING_BLEND_TREE = Animator.StringToHash("TargetingBlendTree");
    const float ANIM_DAMP_TIME = 0.1f;

    readonly int TARGETING_FWD_HASH = Animator.StringToHash("TargetingFwd");
    readonly int TARGETING_RIGHT_HASH = Animator.StringToHash("TargetingRight");
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        _stateMachine.InputReader.OnCancelEvent += OnCancel;
        _stateMachine.Anim.Play(TARGETING_BLEND_TREE);
    }
    public override void Tick(float deltaTime)
    {
        if(_stateMachine.Targeter.CurrentTarget == null)
        {
            _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
            return;
        }
        Vector3 movement = CalculateMovement();

        Move(movement * _stateMachine.TargetingMoveSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }

    

    public override void Exit()
    {
        _stateMachine.InputReader.OnCancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        _stateMachine.Targeter.Cancel();
        _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += _stateMachine.transform.right * _stateMachine.InputReader.MovementValue.x;
        movement += _stateMachine.transform.forward * _stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        float FwdVal = 0;
        float RightVal = 0;

        if (_stateMachine.InputReader.MovementValue.y == 0)
        {
            FwdVal = 0;
        }
        else
        {
            FwdVal = _stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
        }

        if (_stateMachine.InputReader.MovementValue.x == 0)
        {
            RightVal = 0;
        }
        else
        {
            RightVal = _stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
        }


        _stateMachine.Anim.SetFloat(TARGETING_FWD_HASH, FwdVal, ANIM_DAMP_TIME, deltaTime);
        _stateMachine.Anim.SetFloat(TARGETING_RIGHT_HASH, RightVal, ANIM_DAMP_TIME, deltaTime);
    }
}
