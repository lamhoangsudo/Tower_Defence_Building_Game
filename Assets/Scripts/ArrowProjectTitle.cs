using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectTitle : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float moveSpeed =  50f;
    private Vector3 lastDir;
    private float ArrowMaxLife = 2f;
    private float ArrowTimeLife;
    public static ArrowProjectTitle CreateArrowProjectTitle(Vector3 position, Transform enemy)
    {
        Transform arrowProjectTitleTrf = Resources.Load<Transform>("PfArrowProjectitle");
        Transform arrowProjectTitle = Instantiate(arrowProjectTitleTrf, position, Quaternion.identity);
        ArrowProjectTitle arrowProject = arrowProjectTitle.GetComponent<ArrowProjectTitle>();
        arrowProject.SetTarget(enemy);
        return arrowProjectTitle.GetComponent<ArrowProjectTitle>();
    }
    private void Awake()
    {
        ArrowTimeLife = ArrowMaxLife;
    }
    public void Update()
    {
        HandlerMovemnet();
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void HandlerMovemnet()
    {
        Vector3 moveDir;
        if (target != null)
        {
            moveDir = (target.position - this.transform.position).normalized;
            lastDir = moveDir;
        }
        else
        {
            moveDir = lastDir;
            ArrowTimeLife -= Time.deltaTime;
            if (ArrowTimeLife < 0f)
            {
                ArrowTimeLife = ArrowMaxLife;
                Destroy(gameObject);
            }
        }
        transform.eulerAngles = new Vector3 (0, 0, UtilClass.GetAngleFromVector(moveDir));
        transform.position += moveSpeed * Time.deltaTime * moveDir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemySystem>(out var enemy)) 
        {
            enemy.GetComponent<HealthSystem>().Damage(20);
            Destroy(this.gameObject);
        };
    }
}
