using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGen : MonoBehaviour
{
    public GameObject platformPref;
    public Vector3 Bounds;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + Bounds/2, Bounds);
    }
}
