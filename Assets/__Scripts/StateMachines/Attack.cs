using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
    [field: SerializeField] public string AnimName { get; private set; }
    [field: SerializeField] public float TransitionDur { get; private set; } = .1f;
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1;
    [field: SerializeField] public float ComboAttackTime { get; private set; }

    [field: SerializeField] public float ForceTime { get; private set; }

    [field: SerializeField] public float Force { get; private set; }

    [field: SerializeField] public int Damage { get; private set; }


}

