using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteBuilding : MonoBehaviour
{
    [SerializeField]
    private Transform building;
    private bool modeDemolish;
    private void Awake()
    {
        modeDemolish = false;
    }
    private void Start()
    {
        SelectBuildingTypeUI.Instance.OnModeDemolish += Instance_OnModeDemolish;
        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            DemolishBuilding();
        });
    }
    private void Update()
    {
        if(modeDemolish && Input.GetMouseButtonDown(0))
        {
            ModeDemolishBuilding();
        }
    }
    private void Instance_OnModeDemolish(object sender, System.EventArgs e)
    {
        modeDemolish = true;
    }
    private void DemolishBuilding()
    {
        BuildingTypeSO buildingSO = building.GetComponent<BuildingTypeHolder>().buildingTypeSO;
        foreach (BuildResourceAmount resourceReturn in buildingSO.resourceAmount)
        {
            ResourceManager.Instance.AddResource(resourceReturn.resource, Mathf.FloorToInt(resourceReturn.amount * .6f));
        }
        Destroy(building.gameObject);
    }
    private void ModeDemolishBuilding()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(UtilClass.GetMouseWorldPositions(), .1f);
        foreach (Collider2D collider in collider2Ds)
        {
            BuildingTypeSO buildingTypeSO = collider.GetComponent<BuildingTypeHolder>().buildingTypeSO;
            if (buildingTypeSO != null && !buildingTypeSO.buildingTypeID.Equals("HQ"))
            {
                DemolishBuilding();
                break;
            }
        }
    }
}
