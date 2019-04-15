using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeControl : MonoBehaviour
{
    public float sloMoFactor = 0.01f;
    public bool sloMo = false;
    //public int fps = 50;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleSloMo();
        }
    }

    public void ToggleSloMo()
    {
        if (sloMo)
        {
            sloMo = false;
            ManipulateTime(1);
        }
        else
        {
            sloMo = true;
            ManipulateTime(sloMoFactor);
        }
    }

    void ManipulateTime(float sloMoFactor_)
    {
        DOTween.To(() => Time.timeScale, (x) => Time.timeScale = x, sloMoFactor_, 0.2f);
        //Time.timeScale = sloMoFactor_;
        Time.fixedDeltaTime = sloMoFactor * 0.1f;
    }

    public void Action(float time)
    {
        if (sloMo)
        {
            sloMo = false;
            ManipulateTime(5);
            Invoke("ToggleSloMo", time);
        }
    }
}
