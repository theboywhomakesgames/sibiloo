using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : CharacterControl
{
    public Transform target;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        Aim(target.position);
    }

    protected override void CollisionEnter(Collision2D c)
    {

    }

    protected override void CollisionExit(Collision2D c)
    {

    }
}
