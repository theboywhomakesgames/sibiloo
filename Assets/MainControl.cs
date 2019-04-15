using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControl : CharacterControl
{
    public TimeControl tc;
    public float actionTime = 0.5f;

    bool acted = false;

    protected override void Start()
    {
        tc = GetComponent<TimeControl>();
        base.Start();
    }

    void Update()
    {
        CheckControls();
        Aim(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        TurnWhenNecessary();
    }

    private void CheckControls()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Move(1);
            acted = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Move(-1);
            acted = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            StopMoving();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            StopMoving();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoJump();
            acted = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
            acted = true;
        }
        if (acted)
        {
            acted = false;
            Action();
        }
    }
        
    void Action()
    {
        tc.Action(actionTime);
    }

    protected override void CollisionEnter(Collision2D c)
    {

    }

    protected override void CollisionExit(Collision2D c)
    {

    }
}
