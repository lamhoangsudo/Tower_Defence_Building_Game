using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    [SerializeField]
    private Transform repair;
    private HealthSystem health;
    private BuildingTypeSO buildingTypeSO;
    private Transform BuildingDestroyedParticles;
    private void Awake()
    {
        BuildingDestroyedParticles = Resources.Load<Transform>("pfBuildingDestroyedParticles");
    }
    public void Start()
    {
        health = this.gameObject.GetComponent<HealthSystem>();
        buildingTypeSO = this.gameObject.GetComponent<BuildingTypeHolder>().buildingTypeSO;
        health.SetMaxHealth(this.gameObject.GetComponent<BuildingTypeHolder>().buildingTypeSO.healthAmount, true);
        health.OnDead += Health_OnDead;
        health.OnDamage += Health_OnDamage;
        health.OnHeal += Health_OnHeal;
    }
    private void Health_OnDead(object sender, System.EventArgs e)
    {
        CinemachineShake.Instance.setShake(10f, 0.2f);
        Instantiate(BuildingDestroyedParticles, this.transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        Destroy(this.gameObject);
    }
    private void Health_OnHeal(object sender, System.EventArgs e)
    {
        if (health.health < buildingTypeSO.healthAmount)
        {
            repair.gameObject.SetActive(true);
        }
        else
        {
            repair.gameObject.SetActive(false);
        }
    }

    private void Health_OnDamage(object sender, System.EventArgs e)
    {
        repair.gameObject.SetActive(true);
        CinemachineShake.Instance.setShake(7f, 0.15f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
    }
}
