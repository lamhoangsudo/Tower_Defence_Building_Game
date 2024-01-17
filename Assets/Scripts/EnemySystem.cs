using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    private float enemyRadar;
    private float enemyRadarTimeMaxLoad = .2f;
    private float enemyRadarTimeLoad;
    private Transform target;
    public static EnemySystem CreateEnemy(Vector3 position)
    {
        Transform enemyTrf = Resources.Load<Transform>("PfEnemy");
        Transform enemy = Instantiate(enemyTrf, position, Quaternion.identity);
        return enemy.GetComponent<EnemySystem>();
    }
    private new Rigidbody2D rigidbody2D;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        enemyRadar = 10f;
        enemyRadarTimeLoad = Random.Range(0f, enemyRadarTimeMaxLoad);
    }
    private void Start()
    {
        target = BuildingManager.Instance.transform;
    }
    private void Update()
    {
        HandlerTargeting();
        HandlerMovemnet();
    }
    private void HandlerMovemnet()
    {
        Vector3 moveDir = Vector3.zero;
        if(target != null)
        {
            moveDir = (target.position - this.transform.position).normalized;
            Debug.DrawLine(transform.position, target.position, Color.red, 0.1f);
        }
        rigidbody2D.velocity = 5f * moveDir;
    }
    private void HandlerTargeting()
    {
        enemyRadarTimeLoad -= Time.deltaTime;
        if (enemyRadarTimeLoad < 0f)
        {
            enemyRadarTimeLoad = enemyRadarTimeMaxLoad;
            LookForTargets();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BuildingTypeHolder>(out var buildingTypeHolder))
        {
            buildingTypeHolder.GetComponent<HealthSystem>().Damage(10);
            Destroy(this.gameObject);
        }
    }
    private void LookForTargets()
    {
        Collider2D[] targetBuildingFound = Physics2D.OverlapCircleAll(transform.position, enemyRadar);
        foreach (Collider2D targetBuild in targetBuildingFound.Reverse())
        {
            if (targetBuild.GetComponent<BuildingTypeHolder>() != null)
            {
                if (target != null)
                {
                    if (Vector3.Distance(transform.position, targetBuild.transform.position) < Vector3.Distance(transform.position, target.position))
                    {
                        target = targetBuild.transform;
                    }
                }
                else
                {
                    if (Vector3.Distance(transform.position, targetBuild.transform.position) < Vector3.Distance(transform.position, transform.position))
                    {
                        target = targetBuild.transform;
                    }
                }
            }
        }
        if(target == null)
        {
            target = BuildingManager.Instance.GetHeatQuarter().transform;
        }
    }
}
