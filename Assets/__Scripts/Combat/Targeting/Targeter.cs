using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Targeter : MonoBehaviour
{
    private Camera MainCam;

    [SerializeField] CinemachineTargetGroup targetGroup;

    public  List<Target> targets = new List<Target>();

    public Target CurrentTarget { get; private set; }

    private void Start()
    {
        MainCam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent<Target>( out var target)) { return; }
        
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }



    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out var target)) { return; }
      
        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            Vector2 viewPos = MainCam.WorldToViewportPoint(target.transform.position);
            if (viewPos.x > 1 || viewPos.x < 0 || viewPos.y > 1 || viewPos.y < 0)
                continue;

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude < closestDistance)
            {
                closestTarget = target;
                closestDistance = toCenter.sqrMagnitude;
            }

        
        }

        if (closestTarget == null) return false;

        CurrentTarget = closestTarget;
        targetGroup.AddMember(CurrentTarget.transform, 1f, 2f);
        return true;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) return;

        targetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            targetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);

    }
}


