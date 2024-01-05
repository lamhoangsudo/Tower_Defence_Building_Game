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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activebuilding != null)
            {
                Instantiate(activebuilding.prefab, UtilClass.GetMouseWorldPositions(), Quaternion.identity);
            }
        }
    }
    public void SetActivebuilding(BuildingTypeSO buildingType)
    {
        activebuilding = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, buildingType);
    }
}
