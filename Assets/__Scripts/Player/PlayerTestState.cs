using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    float remainingTime = 5;
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        Debug.Log("enter");
        _stateMachine.InputReader.OnJumpEvent += Jump;
    }
    public override void Tick(float deltaTime)
    {
        remainingTime -= deltaTime;
        //Debug.Log(remainingTime);

        if (remainingTime <= 0)
        {
            Debug.Log("****************************SWITCH!!!!");
            _stateMachine.SwitchState(new PlayerTestState(_stateMachine));
        }
    }

    private void Jump()
    {
        _stateMachine.SwitchState(new PlayerTestState(_stateMachine));
        Debug.Log("Jump");
    }

    public override void Exit()
    {

        _stateMachine.InputReader.OnJumpEvent -= Jump;
        Debug.Log("exit");
    }

  

    
}
