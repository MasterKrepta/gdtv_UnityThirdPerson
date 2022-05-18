using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour


{
    private List<Target> targets = new List<Target>();

    public Target CurrentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<Target>( out var target)) { return; }
        
            if (targets.Contains(target) == false)
            {
                targets.Add(target);
            }
        
        

        

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out var target)) { return; }
      
            if (targets.Contains(target) == true)
            {
                targets.Remove(target);
            }

    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        CurrentTarget = targets[0];
        return true;
    }

    public void Cancel()
    {
        CurrentTarget = null;
    }
}


