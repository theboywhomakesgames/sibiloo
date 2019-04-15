using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShoot : MonoBehaviour
{
    public float speed = 10f;
    public float killAfter = 2;
    public float blowRadius = 2, blowPower = 10;
    public GameObject blowPref;
    public AudioSource baseAudioSource;
    public AudioClip gunShot, explosionSound;

    private void Start()
    {
        baseAudioSource = Camera.main.GetComponent<AudioSource>();
        baseAudioSource.PlayOneShot(gunShot);
    }

    public void Shoot(bool goingRight, Vector2 dir)
    {
        transform.right = dir;
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        if (goingRight)
        {
            rb.velocity = speed * dir;
        }
        else
        {
            rb.velocity = -speed * dir;
        }
        Invoke("Kill", killAfter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
            Blow();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            Blow();
    }

    void Blow()
    {
        Instantiate(blowPref, transform.position - Vector3.back * 0.5f, Quaternion.identity);
        //baseAudioSource.PlayOneShot(explosionSound);
        gameObject.AddComponent<CircleCollider2D>().isTrigger = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blowRadius);
        foreach (Collider2D c in colliders)
        {
            if (c.tag == "Obstacle")
            {
                Rigidbody2D rbc = c.GetComponent<Rigidbody2D>();
                Vector2 forceDir = (Vector2)(c.transform.position - transform.position).normalized;
                rbc.velocity = rbc.velocity + forceDir * blowPower;
            }
        }
        Kill();
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
