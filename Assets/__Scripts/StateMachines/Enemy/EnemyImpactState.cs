using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    readonly int IMPACT_HASH = Animator.StringToHash("Impact");
    
    const float CROSSfADE_TIME = 0.1f;

    float duration = 1;
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _stateMachine.Anim.CrossFadeInFixedTime(IMPACT_HASH, CROSSfADE_TIME);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0)
        {
            _stateMachine.SwitchState(new EnemyIdleState(_stateMachine));
        }
    }
    public override void Exit()
    {
        
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
