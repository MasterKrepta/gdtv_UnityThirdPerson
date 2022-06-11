using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField]public InputReader InputReader { get; private set; }
    [field: SerializeField]public CharacterController Controller { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; set; }
    [field: SerializeField] public Animator Anim { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field:SerializeField]public float FreeLookMoveSpeed { get; private set; }
    [field: SerializeField] public float TargetingMoveSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }


    public Transform MainCameraTransform { get; private set; }


    // Start is called before the first frame update
    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    
}