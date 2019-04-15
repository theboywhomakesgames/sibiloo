using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class follow : MonoBehaviour
{
    public Transform target;
    public float duration;

    private void Update()
    {
        transform.DOMoveX(target.position.x, duration);
    }
}
