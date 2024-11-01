using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternAttack : BossAttack
{
    [SerializeField] Transform stageOrigin; // top-left point of stage

    protected new void Start()
    {
        base.Start();
        transform.position = stageOrigin.position;
    }
}
