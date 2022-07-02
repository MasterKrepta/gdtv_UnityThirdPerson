using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BLOCK_HASH = Animator.StringToHash("Block");

    private const float CROSSFADEDUR = 0.1f;

    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Health.SetInvulnerable(true);
        _stateMachine.Anim.CrossFadeInFixedTime(BLOCK_HASH, CROSSFADEDUR);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (!_stateMachine.InputReader.IsBlocking)
        {
            _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
            return;
        }

        if(_stateMachine.Targeter.CurrentTarget == null)
        {
            _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
            return;
        }
    }
    public override void Exit()
    {
        _stateMachine.Health.SetInvulnerable(false);
    }


}
