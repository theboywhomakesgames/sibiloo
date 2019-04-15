using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject hand, hand2;
    public GameObject bulletPref;
    public float runSpeed;
    public float jumpSpeed;
    public bool grounded = false;
    public bool headingRight = true, goingRight = true;
    public Transform rotates;
    public Vector2 dir;

    bool acted = false;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void TurnWhenNecessary()
    {
        float angle = Mathf.Abs(hand.transform.localRotation.eulerAngles.z);
        if ((angle > 90 && angle < 270) || (angle < 270 && angle > 90))
        {
            TurnAround();
            setRunAnimationSpeed();
        }
    }

    protected void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, hand.transform.position, Quaternion.identity);
        bullet.GetComponent<bulletShoot>().Shoot(headingRight, dir);
    }

    // need to pass Camera.main.ScreenToWorldPoint(Input.mousePosition) if you wanna aim at mouse
    protected void Aim(Vector3 toPos)
    {
        Vector3 dif = hand.transform.position - new Vector3(toPos.x, toPos.y, hand.transform.position.z);
        Vector3 dif2 = hand2.transform.position - new Vector3(toPos.x, toPos.y, hand2.transform.position.z);
        dif.Normalize();
        dif2.Normalize();
        dir = headingRight ? -dif : dif;
        hand.transform.right = dir;
        hand2.transform.right = headingRight ? -dif2 : dif2;
    }

    protected void DoJump()
    {
        if (grounded)
            rb.velocity = new Vector2(rb.velocity.x, 0) + (Vector2)transform.up * jumpSpeed;
    }

    protected void Move(int dir)
    {
        float moveSpeed;

        if (grounded)
        {
            // normal 
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            // in air
            moveSpeed = runSpeed;
        }

        if (dir > 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y) + (Vector2)transform.right * moveSpeed;
            //going left
            if (!goingRight)
            {
                GoBackWards();
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y) + (Vector2)transform.right * -moveSpeed;
            //going right
            if (goingRight)
            {
                GoBackWards();
            }
        }

    }

    protected void TurnAround()
    {
        headingRight = !headingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        rotates.Rotate(new Vector3(0, 180, 0));
    }

    protected void GoBackWards()
    {
        goingRight = !goingRight;
        setRunAnimationSpeed();
    }

    protected void setRunAnimationSpeed()
    {
        int multiplier = headingRight ? 1 : -1;
        animator.SetFloat("RunSpeed", goingRight ? multiplier : -multiplier);
    }

    protected void StopMoving()
    {
        animator.SetBool("Running", false);
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Obstacle") && !grounded)
        {
            setGrounded(true);
        }
        CollisionEnter(collision);
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Obstacle")
        {
            setGrounded(false);
        }
        CollisionExit(collision);
    }

    protected void setGrounded(bool val)
    {
        grounded = val;
        animator.SetBool("Grounded", val);
    }

    protected abstract void CollisionEnter(Collision2D c);
    protected abstract void CollisionExit(Collision2D c);
}
