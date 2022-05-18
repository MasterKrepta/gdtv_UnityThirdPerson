using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    readonly int TARGETING_BLEND_TREE = Animator.StringToHash("TargetingBlendTree");
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
        Debug.Log(_stateMachine.Targeter.CurrentTarget.name);
    }
    public override void Exit()
    {
        _stateMachine.InputReader.OnCancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
    }


    
}
