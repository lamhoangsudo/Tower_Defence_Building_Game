using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuildingTypeUI : MonoBehaviour
{
    [SerializeField] Transform buildingTypeTemplate;
    [SerializeField] Sprite arrowSprite;
    [SerializeField] Sprite demolishSprite;
    private Transform arrowButton;
    private Transform demolishButton;
    private BuildingTypeListSO buildingTypeList;
    private Dictionary<BuildingTypeSO, Transform> buildingTypeTransformDictionary;
    private MouseEnterExitEvent mouseEnterExitEvent;
    public event EventHandler OnModeDemolish;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;
    private bool demolish;
    public static SelectBuildingTypeUI Instance;
    private void Awake()
    {
        Instance = this;
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
            UpdateActiveBuildingTypeButton(null, false);
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
        demolishButton = Instantiate(buildingTypeTemplate, transform);
        demolishButton.position += new Vector3(offsetAmount, 0, 0) * index;
        demolishButton.name = "DemolishTemplate";
        demolishButton.Find("Image").GetComponent<Image>().sprite = demolishSprite;
        demolishButton.Find("Select").gameObject.SetActive(false);
        mouseEnterExitEvent = demolishButton.GetComponent<MouseEnterExitEvent>();
        demolishButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActivebuilding(null);
            UpdateActiveBuildingTypeButton(null, true);
            OnModeDemolish?.Invoke(this, EventArgs.Empty);
        });
        mouseEnterExitEvent.OnEnter += (object sender, EventArgs e) =>
        {
            ToolTipUI.Instance.Show("Demolish");
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
                    UpdateActiveBuildingTypeButton(buildingType, false);
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
    public void UpdateActiveBuildingTypeButton(BuildingTypeSO activebuilding, bool demolish)
    {
        if (activebuilding == null)
        {
            if (demolish == false)
            {
                arrowButton.Find("Select").gameObject.SetActive(true);
                demolishButton.Find("Select").gameObject.SetActive(false);
            }
            else
            {
                arrowButton.Find("Select").gameObject.SetActive(false);
                demolishButton.Find("Select").gameObject.SetActive(true);
            }
            foreach (BuildingTypeSO buildingType in buildingTypeTransformDictionary.Keys)
            {
                if (buildingTypeTransformDictionary[buildingType].Find("Select").gameObject.activeSelf == true)
                {
                    buildingTypeTransformDictionary[buildingType].Find("Select").gameObject.SetActive(false);
                }
            }
        }
        else
        {
            arrowButton.Find("Select").gameObject.SetActive(false);
            demolishButton.Find("Select").gameObject.SetActive(false);
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
}
