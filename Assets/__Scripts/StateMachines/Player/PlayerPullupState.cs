using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullupState : PlayerBaseState
{
    readonly int PULLUP_HASH = Animator.StringToHash("Pullup");

private readonly Vector3 offest = new Vector3(0f,2.325f, 0.65f);

    const float CROSSfADE_TIME = 0.1f;
    public PlayerPullupState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Anim.CrossFadeInFixedTime(PULLUP_HASH, CROSSfADE_TIME);
    }

    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(_stateMachine.Anim, "Climbing") < 1f) return;

        _stateMachine.Controller.enabled = false;
        _stateMachine.transform.Translate(offest, Space.Self);
        _stateMachine.Controller.enabled = true;

        _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine, false));
    }
    public override void Exit()
    {
        _stateMachine.Controller.Move(Vector3.zero);
        _stateMachine.ForceReceiver.Reset();
    }


}
