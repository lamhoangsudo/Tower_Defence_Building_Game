using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeSO activebuilding;
    private BuildingTypeListSO BuildingTypeList;
    private Camera mainCamera;
    private const string stoneHarvester = "SH1";
    private const string woodHarvester = "WH1";
    public event EventHandler<BuildingTypeSO> OnActiveBuildingTypeChanged;
    public static BuildingManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        BuildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        activebuilding = null;
    }
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && activebuilding != null)
        {
            if (CanSpawnBuilding(activebuilding, UtilClass.GetMouseWorldPositions(), out string ErrorMessage)) 
            {
                if(ResourceManager.Instance.CanAffort(activebuilding.resourceAmount))
                {
                    ResourceManager.Instance.SetResourceAfterBuild(activebuilding.resourceAmount);
                    Instantiate(activebuilding.prefab, UtilClass.GetMouseWorldPositions(), Quaternion.identity);
                }
                else
                {
                    ToolTipUI.Instance.Show("Can't affort " + activebuilding.GetConstructionResourceNeedString(), new ToolTipUI.ToolTipTimerShow { timer = 2f});
                }
            }
            else
            {
                ToolTipUI.Instance.Show(ErrorMessage, new ToolTipUI.ToolTipTimerShow { timer = 2f });
            }
            
        }
    }
    public void SetActivebuilding(BuildingTypeSO buildingType)
    {
        activebuilding = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, buildingType);
    }
    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string ErrorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliderBuilding = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        if (colliderBuilding.Length > 0)
        {
            ErrorMessage = "Area is not clear";
            return false;
        }
        colliderBuilding = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in colliderBuilding)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (colliderBuilding.Length == 0)
            {
                ErrorMessage = "";
                return true;
            }
            else
            {
                if (buildingTypeHolder != null)
                {
                    if (!buildingTypeHolder.buildingTypeSO.Equals(buildingType))
                    {
                        ErrorMessage = "";
                        return true;
                    }
                    else
                    {
                        ErrorMessage = "Build to close to another building";
                        return false;
                    }
                }
            }
        }
        colliderBuilding = Physics2D.OverlapCircleAll(position, buildingType.maxConstructionRadius);
        foreach (Collider2D collider2D in colliderBuilding)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (colliderBuilding.Length == 0) 
            {
                ErrorMessage = "";
                return true; 
            }
            else
            {
                if (buildingTypeHolder != null)
                {
                    ErrorMessage = "";
                    return true;
                }
            }
        }
        ErrorMessage = "Build to far to another building";
        return false;
    }
}
