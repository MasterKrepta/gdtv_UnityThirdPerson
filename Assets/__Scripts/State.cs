using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();

    protected float GetNormalizedTime(Animator Anim)
    {
        AnimatorStateInfo currentInfo = Anim.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = Anim.GetNextAnimatorStateInfo(0);

        if (Anim.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!Anim.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }

    }
}
