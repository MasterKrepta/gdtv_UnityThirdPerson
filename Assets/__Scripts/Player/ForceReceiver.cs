using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    private float verticalVelocity;
    private Vector3 impact;

    private Vector3 DampingVel;
    [SerializeField] float drag = 0.3f;
    [SerializeField]CharacterController controller;
    [SerializeField] private NavMeshAgent Agent;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            //To stop random falling from small inclines do this instead of setting to zero
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref DampingVel, drag);

        if(impact.sqrMagnitude < 0.2f * 0.2f)
        {
            if(Agent != null)
            {
                Agent.enabled = true;
            }
            
        }
    }

    internal void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }

    public void AddForce(Vector3 force)
    {
        print(this.name + " got knockedback " + force);
        impact += force;

        if(Agent != null)
        {
            Agent.enabled = false;
        }

    }

    public void Jump(float Force)
    {
        verticalVelocity += Force;
    }
}
