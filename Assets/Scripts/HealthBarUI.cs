using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform Bar;
    [SerializeField] private HealthSystem healthSystem;
    private float sizeY, sizeZ;
    public void Awake()
    {
        sizeY = Bar.localScale.y;
        sizeZ = Bar.localScale.z;
    }
    public void Start()
    {
        UpdateVisual();
        healthSystem.OnDamage += HealthSystem_OnDamage;
    }
    private void HealthSystem_OnDamage(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }
    public void UpdateVisual()
    {
        float healthNormalize = healthSystem.HealthNormalize();
        if(healthNormalize < 1 )
        {
            Bar.localScale = new Vector3(healthSystem.HealthNormalize(), sizeY, sizeZ);
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }
}
