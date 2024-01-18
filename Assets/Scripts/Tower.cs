using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private float towerRadar;
    private float towerRadarTimeMaxLoad = .2f;
    private float towerRadarTimeLoad;
    private float towerShootMaxLoad = .2f;
    private float towerShootTimeLoad;
    private Transform target;
    [SerializeField]
    private Transform pointShot;
    private void Awake()
    {
        towerRadar = 20f;
    }
    private void Update()
    {
        HandlerTargeting();
        HandlerShooting();
    }
    private void HandlerTargeting()
    {
        towerRadarTimeLoad -= Time.deltaTime;
        if (towerRadarTimeLoad < 0f)
        {
            towerRadarTimeLoad = towerRadarTimeMaxLoad;
            LookForTargets();
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
                ArrowProjectTitle.CreateArrowProjectTitle(transform.position, target);
            }           
        }
    }
    private void LookForTargets()
    {
        Collider2D[] targetEnemyFound = Physics2D.OverlapCircleAll(transform.position, towerRadar);
        foreach (Collider2D targetEnemy in targetEnemyFound)
        {
            if (targetEnemy.GetComponent<EnemySystem>() != null)
            {
                if (target != null)
                {
                    if (Vector3.Distance(transform.position, targetEnemy.transform.position) < Vector3.Distance(transform.position, target.position))
                    {
                        target = targetEnemy.transform;
                    }
                }
                else
                {
                    target = targetEnemy.transform;
                }
            }
        }
    }
}
