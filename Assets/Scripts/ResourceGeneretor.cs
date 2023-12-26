using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGeneretor : MonoBehaviour
{
    private float timer;
    private float timerMax = 1f;
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0 )
        {
            timer += timerMax;
            Debug.Log("!Ding");
        }
    }
}
