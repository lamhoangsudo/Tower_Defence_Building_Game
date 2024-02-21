using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    private float enemyRadar;
    private float enemyRadarTimeMaxLoad = .2f;
    private float enemyRadarTimeLoad;
    private bool enemyStartMoving;
    [SerializeField] private float maxHealth;
    private Transform target;
    private Transform EnemyDieParticles;
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
        EnemyDieParticles = Resources.Load<Transform>("pfEnemyDieParticles");
    }
    private void Start()
    {
        HealthSystem healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetMaxHealth(maxHealth, true);
        target = BuildingManager.Instance.transform;
        enemyStartMoving = false;
        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnDead += HealthSystem_OnDead;
        EnemyWaveManager.Instance.OnFullEnemyWaveReady += Instance_OnFullEnemyWaveReady;
    }

    private void Instance_OnFullEnemyWaveReady(object sender, System.EventArgs e)
    {
        enemyStartMoving = true;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        EnemyDead();
    }

    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        CinemachineShake.Instance.setShake(5f, 0.1f);
        ChromaticAberration.Instance.SetWeight(.3f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
    }

    private void Update()
    {
        if (enemyStartMoving)
        {
            HandlerTargeting();
            HandlerMovemnet();
        }
    }
    private void HandlerMovemnet()
    {
        Vector3 moveDir = Vector3.zero;
        if (target != null)
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
            enemyRadarTimeLoad += enemyRadarTimeMaxLoad;
            EnemyLookForTargets();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<BuildingTypeHolder>(out var buildingTypeHolder))
        {
            buildingTypeHolder.GetComponent<HealthSystem>().Damage(10);
            EnemyDead();
        }
    }
    private void EnemyLookForTargets()
    {
        target = UtilClass.LookForTargets<BuildingTypeHolder>(this.transform, enemyRadar);
        if (target == null)
        {
            if (BuildingManager.Instance.GetHeatQuarter() != null)
            {
                target = BuildingManager.Instance.GetHeatQuarter().transform;
            }
        }
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
    private void EnemyDead()
    {
        Instantiate(EnemyDieParticles, this.transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        CinemachineShake.Instance.setShake(6f, 0.15f);
        ChromaticAberration.Instance.SetWeight(.5f);
        Destroy(this.gameObject);
    }
}
