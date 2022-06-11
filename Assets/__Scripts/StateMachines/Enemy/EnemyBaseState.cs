using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public abstract class EnemyBaseState : State
    {

        protected EnemyStateMachine _stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
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

    protected bool IsInChaseRange()
    {
        float dist = (_stateMachine.Player.transform.position - _stateMachine.transform.position).sqrMagnitude;

        return dist <= _stateMachine.ChasingRange * _stateMachine.ChasingRange;
    }

    protected bool IsInAttackRange()
    {
        float dist = (_stateMachine.Player.transform.position - _stateMachine.transform.position).sqrMagnitude;

        return dist <= _stateMachine.AttackRange * _stateMachine.AttackRange;
    }
    protected void FacePlayer()
    {
        if (_stateMachine.Player == null) return;

        Vector3 lookDir = (_stateMachine.Player.transform.position - _stateMachine.transform.position);
        lookDir.y = 0;

        _stateMachine.transform.rotation = Quaternion.LookRotation(lookDir);
    }

}
