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
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll   Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector   LedgeDetector { get; private set; }
    [field: SerializeField] public float AttackKnockback { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }

    
    [field: SerializeField] public float JumpForce { get; private set; }

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity; // We need a starting point 

    public Transform MainCameraTransform { get; private set; }



    // Start is called before the first frame update
    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamge;
        Health.OnDie += HandleDie;

    }

    private void HandleTakeDamge()
    {
        SwitchState(new PlayerImpactState(this));
        
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamge;
        Health.OnDie -= HandleDie;
    }

}
