using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine _stateMachine;


    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        _stateMachine.Controller.Move((motion + _stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void FaceTarget()
    {
        if (_stateMachine.Targeter.CurrentTarget == null) return;

        Vector3 lookDir = (_stateMachine.Targeter.CurrentTarget.transform.position - _stateMachine.transform.position);
        lookDir.y = 0;

        _stateMachine.transform.rotation = Quaternion.LookRotation(lookDir);
        

    }
}
