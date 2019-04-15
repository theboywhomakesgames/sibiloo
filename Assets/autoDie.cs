using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDie : MonoBehaviour
{
    public float killAfter = 0.2f;


    private void Start()
    {
        Invoke("kill", killAfter);
    }

    void kill()
    {
        Destroy(gameObject);
    }
}
