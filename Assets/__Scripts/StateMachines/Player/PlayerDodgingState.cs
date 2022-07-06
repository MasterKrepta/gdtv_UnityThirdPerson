using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    readonly int DODGING_BLENDTREE_HASH = Animator.StringToHash("DodgeBlendTree");
    const float ANIM_DAMP_TIME = 0.1f;
    const float CROSSfADE_TIME = 0.1f;

    readonly int DODGE_FWD_HASH = Animator.StringToHash("DodgeFwd");
    readonly int DODGE_RIGHT_HASH = Animator.StringToHash("DodgeRight");

    Vector3 dodgeDirInput;
    float remainingDodgeTime;
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgeDirInput) : base(stateMachine)
    {
        this.dodgeDirInput = dodgeDirInput;
    }

    public override void Enter()
    {

        remainingDodgeTime = _stateMachine.DodgeDuration;

        _stateMachine.Anim.SetFloat(DODGE_FWD_HASH, dodgeDirInput.y);
        _stateMachine.Anim.SetFloat(DODGE_RIGHT_HASH, dodgeDirInput.x);
        _stateMachine.Anim.CrossFadeInFixedTime(DODGING_BLENDTREE_HASH, CROSSfADE_TIME);

        _stateMachine.Health.SetInvulnerable(true);
    }
    public override void Tick(float deltaTime)
    {

        Vector3 movement = new Vector3();
        movement += _stateMachine.transform.right * dodgeDirInput.x * _stateMachine.DodgeLength / _stateMachine.DodgeDuration;
        movement += _stateMachine.transform.forward * dodgeDirInput.y * _stateMachine.DodgeLength / _stateMachine.DodgeDuration;

        Move(movement, deltaTime);

        FaceTarget();

        remainingDodgeTime -= deltaTime;

        if (remainingDodgeTime <= 0f)
        {
            _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
        }

        
    }
    public override void Exit()
    {
        _stateMachine.Health.SetInvulnerable(false);
    }


}