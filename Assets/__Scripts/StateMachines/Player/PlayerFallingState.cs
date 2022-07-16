using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    readonly int FALLING_BLENDTREE = Animator.StringToHash("Fall");
    const float ANIM_DAMP_TIME = 0.1f;
    const float CROSSfADE_TIME = 0.1f;

    Vector3 momentum;
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
        momentum = _stateMachine.Controller.velocity;
        momentum.y = 0f;

        _stateMachine.Anim.CrossFadeInFixedTime(FALLING_BLENDTREE, CROSSfADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (_stateMachine.Controller.isGrounded)
        {
            ReturnToLocomotion();
        }

        FaceTarget();
    }
    public override void Exit()
    {
        _stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetect;
    }

    private void HandleLedgeDetect(Vector3 ledgeFwd, Vector3 closestPoint)
    {
        _stateMachine.SwitchState(new PlayerHangingState(_stateMachine, ledgeFwd, closestPoint));
    }
}
