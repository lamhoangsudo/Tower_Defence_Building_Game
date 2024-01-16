using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuildingTypeUI : MonoBehaviour
{
    [SerializeField] Transform buildingTypeTemplate;
    [SerializeField] Sprite arrowSprite;
    private Transform arrowButton;
    private BuildingTypeListSO buildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> buildingTypeTransformDictionary;
    private MouseEnterExitEvent mouseEnterExitEvent;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(nameof(BuildingTypeListSO));
        buildingTypeTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();
    }
    private void Start()
    {
        int index = 0;
        float offsetAmount = 190f;
        
        arrowButton = Instantiate(buildingTypeTemplate, transform);
        arrowButton.position += new Vector3(offsetAmount, 0, 0) * index;
        arrowButton.name = "MouseArrowTemplate";
        arrowButton.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        mouseEnterExitEvent = arrowButton.GetComponent<MouseEnterExitEvent>();
        arrowButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActivebuilding(null);
            UpdateActiveBuildingTypeButton(null);
        });
        mouseEnterExitEvent.OnEnter += (object sender, EventArgs e) =>
        {
            ToolTipUI.Instance.Show("Arrow");
        };
        mouseEnterExitEvent.OnExit += (object sender, EventArgs e) =>
        {
            ToolTipUI.Instance.Hide();
        };
        index++;
        foreach (BuildingTypeSO buildingType in buildingTypeList.buildingTypes)
        {
            if (!ignoreBuildingTypeList.Contains(buildingType))
            {
                buildingTypeTemplate = Instantiate(buildingTypeTemplate, transform);
                buildingTypeTemplate.position += new Vector3(offsetAmount, 0, 0) * index;
                buildingTypeTemplate.name = buildingType.name + "Template";
                buildingTypeTemplate.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;
                buildingTypeTemplate.Find("Select").gameObject.SetActive(false);
                buildingTypeTemplate.GetComponent<Button>().onClick.AddListener(() =>
                {
                    BuildingManager.Instance.SetActivebuilding(buildingType);
                    UpdateActiveBuildingTypeButton(buildingType);
                });
                mouseEnterExitEvent = buildingTypeTemplate.GetComponent<MouseEnterExitEvent>();
                mouseEnterExitEvent.OnEnter += (object sender, EventArgs e) =>
                {
                    string output = buildingType.buildingTypeName + "\n" + buildingType.GetConstructionResourceNeedString();
                    ToolTipUI.Instance.Show(output);
                };
                mouseEnterExitEvent.OnExit += (object sender, EventArgs e) =>
                {
                    ToolTipUI.Instance.Hide();
                };
                buildingTypeTransformDictionary.Add(buildingType, buildingTypeTemplate);
                index++;
            }
        }
    }
    public void UpdateActiveBuildingTypeButton(BuildingTypeSO activebuilding)
    {
        if (activebuilding == null)
        {
            arrowButton.Find("Select").gameObject.SetActive(true);
        }
        else
        {
            arrowButton.Find("Select").gameObject.SetActive(false);
        }
        foreach (BuildingTypeSO buildingType in buildingTypeTransformDictionary.Keys)
        {
            if (activebuilding == buildingType)
            {
                buildingTypeTransformDictionary[buildingType].Find("Select").gameObject.SetActive(true);
            }
            else
            {
                buildingTypeTransformDictionary[buildingType].Find("Select").gameObject.SetActive(false);
            }
        }
    }
}
