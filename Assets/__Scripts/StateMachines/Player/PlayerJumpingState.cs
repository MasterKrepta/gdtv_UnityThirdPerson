using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    readonly int JUMPING_BLENDTREE = Animator.StringToHash("Jump");
    const float ANIM_DAMP_TIME = 0.1f;
    const float CROSSfADE_TIME = 0.1f;

    Vector3 momentum;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
        _stateMachine.ForceReceiver.Jump(_stateMachine.JumpForce);

        momentum = _stateMachine.Controller.velocity;
        momentum.y = 0f;

        _stateMachine.Anim.CrossFadeInFixedTime(JUMPING_BLENDTREE, CROSSfADE_TIME);
    }

   private void HandleLedgeDetect(Vector3 ledgeFwd, Vector3 closestPoint)
    {
        _stateMachine.SwitchState(new PlayerHangingState(_stateMachine, ledgeFwd, closestPoint));
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if(_stateMachine.Controller.velocity.y <= 0)
        {
            _stateMachine.SwitchState(new PlayerFallingState(_stateMachine));
        }

        FaceTarget();
    }
    public override void Exit()
    {
        _stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetect;
    }

    
}
