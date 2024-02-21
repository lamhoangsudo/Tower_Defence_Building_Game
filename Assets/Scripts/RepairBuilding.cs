using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RepairBuilding : MonoBehaviour
{
    [SerializeField]
    private Transform building;
    [SerializeField]
    private ResourceTypeSO goldType;
    private HealthSystem healthCurrent;
    private BuildingTypeSO buildingTypeSO;
    private BuildResourceAmount[] repairCost;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    private void Start()
    {
        healthCurrent = building.GetComponent<HealthSystem>();
        buildingTypeSO = building.GetComponent<BuildingTypeHolder>().buildingTypeSO;
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            repairCost = CalculatorRepairCost();
            if (ResourceManager.Instance.CanAffort(repairCost))
            {
                ResourceManager.Instance.SetResourceAfterBuild(repairCost);
                BuildingRepair();
            }   
        });
    }
    private void BuildingRepair()
    {
        healthCurrent.HealMax();
    }
    private BuildResourceAmount[] CalculatorRepairCost()
    {
        BuildResourceAmount[] repairCost = buildingTypeSO.resourceAmount;
        float missingHealthPercent = (buildingTypeSO.healthAmount - healthCurrent.health) / buildingTypeSO.healthAmount;
        foreach (BuildResourceAmount cost in repairCost)
        {
            cost.amount = Mathf.FloorToInt(cost.amount * missingHealthPercent * 0.8f);
        }
        int amount = Mathf.FloorToInt(missingHealthPercent * 1f);
        BuildResourceAmount[] gold = new BuildResourceAmount[] { new BuildResourceAmount { resource = goldType, amount = amount } };
        repairCost.AddRange(gold);
        return repairCost;
    }
}
