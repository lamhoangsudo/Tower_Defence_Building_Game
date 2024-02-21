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
    [SerializeField] private GameObject headQuarter;
    //private const string stoneHarvester = "SH1";
    //private const string woodHarvester = "WH1";
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
        headQuarter.GetComponent<HealthSystem>().OnDead += BuildingManager_OnDead;
    }

    private void BuildingManager_OnDead(object sender, EventArgs e)
    {
        GameOverUI.Instance.Show();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && activebuilding != null)
        {
            if (CanSpawnBuilding(activebuilding, UtilClass.GetMouseWorldPositions(), out string ErrorMessage))
            {
                if (ResourceManager.Instance.CanAffort(activebuilding.resourceAmount))
                {
                    ResourceManager.Instance.SetResourceAfterBuild(activebuilding.resourceAmount);
                    BuildingConstruct.CreateBuildConstructTitle(UtilClass.GetMouseWorldPositions(), activebuilding);
                    SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
                }
                else
                {
                    ToolTipUI.Instance.Show("Can't affort " + activebuilding.GetConstructionResourceNeedString(), new ToolTipUI.ToolTipTimerShow { timer = 2f });
                }
            }
            else
            {
                ToolTipUI.Instance.Show(ErrorMessage, new ToolTipUI.ToolTipTimerShow { timer = 2f });
            }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnemySystem.CreateEnemy(UtilClass.GetMouseWorldPositions());
        }
    }
    public void SetActivebuilding(BuildingTypeSO buildingType)
    {
        activebuilding = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(this, buildingType);
    }
    private bool CanSpawnBuilding(BuildingTypeSO buildingType, Vector3 position, out string ErrorMessage)
    {
        bool check = false;
        ErrorMessage = "";
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] colliderBuilding = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        if (colliderBuilding.Length > 0)
        {
            ErrorMessage = "Area is not clear";
            check = false;
            return check;
        }
        colliderBuilding = Physics2D.OverlapCircleAll(position, buildingType.minConstructionRadius);
        foreach (Collider2D collider2D in colliderBuilding)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (colliderBuilding.Length == 0)
            {
                ErrorMessage = "";
                check = true;
            }
            else
            {
                if (buildingTypeHolder != null)
                {
                    if (!buildingTypeHolder.buildingTypeSO.Equals(buildingType))
                    {
                        ErrorMessage = "";
                        check = true;
                    }
                    else
                    {
                        ErrorMessage = "Build to close to another building";
                        check = false;
                        return check;
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
                check = true;
            }
            else
            {
                if (buildingTypeHolder == null)
                {
                    ErrorMessage = "Build to far to another building";
                    check = false;
                }
                else
                {
                    ErrorMessage = "";
                    if (buildingType.resourceGeneratorData.resourceTypeSO != null)
                    {
                        if (ResourceGeneretor.GetNearByResourceNode(buildingType.resourceGeneratorData, position) > 0)
                        {
                            check = true;
                        }
                        else
                        {
                            check = false;
                            ErrorMessage = "There are no nearby resources";
                        }
                    }
                    return check;
                }
            }
        }
        return check;
    }
    public GameObject GetHeatQuarter()
    {
        return headQuarter;
    }
}
