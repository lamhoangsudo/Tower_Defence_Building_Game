using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float towerRadar;
    private float towerRadarTimeMaxLoad = .2f;
    private float towerRadarTimeLoad;
    [SerializeField]
    private float towerShootMaxLoad;
    private float towerShootTimeLoad;
    private Transform target;
    [SerializeField]
    private Transform pointShot;
    private void Awake()
    {
        towerRadar = 40f;
    }
    private void Update()
    {
        HandlerTargeting();
        HandlerShooting();
    }
    private void HandlerTargeting()
    {
        towerRadarTimeLoad -= Time.deltaTime;
        if (towerRadarTimeLoad <= 0f)
        {
            towerRadarTimeLoad = towerRadarTimeMaxLoad;
            target = UtilClass.LookForTargets(this.transform, towerRadar, "EnemySystem");
        }
    }
    private void HandlerShooting()
    {
        if(target != null)
        {
            towerShootTimeLoad -= Time.deltaTime;
            if (towerShootTimeLoad < 0f)
            {
                towerShootTimeLoad += towerShootMaxLoad;
                ArrowProjectTitle.CreateArrowProjectTitle(pointShot.position, target);
            }           
        }
    }
}
