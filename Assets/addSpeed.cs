using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class addSpeed : MonoBehaviour
{
    public Vector3 bounceSpeed;
    public Rigidbody rb;
    
    Vector3 mp;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            mp = Input.mousePosition;
            print(mp);
        }

        if (Input.GetMouseButtonUp(0))
        {
            mp = Input.mousePosition - mp;
            mp /= 50;
            rb.velocity = new Vector3(mp.x, rb.velocity.y, mp.y);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground" && bounceSpeed.y > rb.velocity.y)
        {
            rb.velocity = new Vector3(rb.velocity.x, bounceSpeed.y, rb.velocity.z);
        }
    }
}
