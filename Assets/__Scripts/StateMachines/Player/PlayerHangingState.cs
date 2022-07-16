using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    readonly int HANGING_HASH = Animator.StringToHash("Hanging");
    
    private Vector3 ledgeFwd;
    
    private Vector3 closetPoint;
    const float CROSSfADE_TIME = 0.1f;

    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeFwd, Vector3 closestPoint) : base(stateMachine)
    {

        
        this.ledgeFwd = ledgeFwd;
        this.closetPoint = closestPoint;
    }

    public override void Enter()
    {
        _stateMachine.transform.rotation = Quaternion.LookRotation(ledgeFwd, Vector3.up);
         
        // _stateMachine.Controller.enabled = false;
        // _stateMachine.transform.position = closetPoint - (_stateMachine.LedgeDetector.transform.position - _stateMachine.transform.position);
        // _stateMachine.Controller.enabled = true;

        _stateMachine.Anim.CrossFadeInFixedTime(HANGING_HASH, CROSSfADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        if (_stateMachine.InputReader.MovementValue.y < 0f)
        {
            _stateMachine.Controller.Move(Vector3.zero);
            _stateMachine.ForceReceiver.Reset();
            _stateMachine.SwitchState(new PlayerFallingState(_stateMachine));
        }

        if (_stateMachine.InputReader.MovementValue.y > 0f)
        {
            _stateMachine.SwitchState(new PlayerPullupState(_stateMachine));
        }
    }
    public override void Exit()
    {

    }




}
