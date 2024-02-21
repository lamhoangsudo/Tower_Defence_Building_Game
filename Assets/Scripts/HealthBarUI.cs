using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Transform Bar;
    [SerializeField] private Transform objHealth;
    private HealthSystem healthSystem;
    [SerializeField] private Transform separator;
    [SerializeField] private Transform startSeparator;
    private float sizeY, sizeZ;
    public void Awake()
    {
        sizeY = Bar.localScale.y;
        sizeZ = Bar.localScale.z;
    }
    public void Start()
    {
        healthSystem = objHealth.GetComponent<HealthSystem>();
        healthSystem.OnDamage += HealthSystem_OnDamage;
        healthSystem.OnHeal += HealthSystem_OnHeal;
        SetUpHealthUI();
        UpdateVisual();
    }

    private void HealthSystem_OnHeal(object sender, System.EventArgs e)
    {
        UpdateVisual();
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
    private void UpdateSeparator(int numberSeparator, float distanceEachSeparator)
    {
        for (int i = 1; i < numberSeparator; i++)
        {
            Instantiate(separator, startSeparator.transform.position + new Vector3(distanceEachSeparator * i * transform.localScale.x, 0 ,0), Quaternion.identity, startSeparator);
        }
    }
    private void SetUpHealthUI()
    {
        int amountHealthPart = 10;
        int numberSeparator = 0;
        if (objHealth.GetComponent<BuildingTypeHolder>() != null)
        {
            numberSeparator = Mathf.FloorToInt(objHealth.GetComponent<BuildingTypeHolder>().buildingTypeSO.healthAmount / amountHealthPart);
        }
        else if (objHealth.GetComponent<EnemySystem>() != null)
        {
            numberSeparator = Mathf.FloorToInt(objHealth.GetComponent<EnemySystem>().GetMaxHealth() / amountHealthPart);
        }       
        float distanceEachSeparator = Bar.Find("Bar").transform.localScale.x / numberSeparator;
        UpdateSeparator(numberSeparator, distanceEachSeparator);
    }
}
