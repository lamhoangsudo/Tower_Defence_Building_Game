using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    private HealthSystem health;
    public void Start()
    {
        health = this.gameObject.GetComponent<HealthSystem>();
        health.SetMaxHealth(this.gameObject.GetComponent<BuildingTypeHolder>().buildingTypeSO.healthAmount, true);
        health.OnDamage += Health_OnDamage;
        health.OnDead += Health_OnDead;
    }
    private void Health_OnDead(object sender, System.EventArgs e)
    {
        Destroy(this.gameObject);
    }

    private void Health_OnDamage(object sender, System.EventArgs e)
    {
        
    }
}
