using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    readonly int FREE_LOOK_SPEED =  Animator.StringToHash("FreeLookSpeed");
    const float ANIM_DAMP_TIME = 0.1f;
    

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {

    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        _stateMachine.Controller.Move(movement * deltaTime * _stateMachine.FreeLookMoveSpeed);

        if (_stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            _stateMachine.Anim.SetFloat(FREE_LOOK_SPEED, 0, ANIM_DAMP_TIME, deltaTime);
            return;
        }
        _stateMachine.Anim.SetFloat(FREE_LOOK_SPEED, 1, ANIM_DAMP_TIME, deltaTime);

        FaceMovementDirection(movement, deltaTime);

    }

    public override void Exit()
    {


    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        _stateMachine.transform.rotation = Quaternion.Lerp(_stateMachine.transform.rotation, 
                                                            Quaternion.LookRotation(movement),
                                                            deltaTime * _stateMachine.RotationDamping);
    }



  private Vector3 CalculateMovement()
    {
        Vector3 fwd = _stateMachine.MainCameraTransform.forward;
        Vector3 right = _stateMachine.MainCameraTransform.right;
        
        fwd.y = 0f;
        right.y = 0f;

        fwd.Normalize();
        right.Normalize();

        return fwd * _stateMachine.InputReader.MovementValue.y +
                right * _stateMachine.InputReader.MovementValue.x;

    }


    
}
