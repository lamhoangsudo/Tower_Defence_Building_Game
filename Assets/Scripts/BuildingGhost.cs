using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private GameObject buildingGhostSprite;
    [SerializeField] private GameObject resourceNearByOverlay;
    private ResourceNearByOverlay resourceNearBy;
    private void Awake()
    {
        Hide();
        resourceNearBy = resourceNearByOverlay.GetComponent<ResourceNearByOverlay>();
    }
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += Instance_OnActiveBuildingTypeChanged;
    }

    private void Instance_OnActiveBuildingTypeChanged(object sender, BuildingTypeSO e)
    {
        if (e != null) Show(e);
        else Hide();
    }

    private void Update()
    {
        transform.position = UtilClass.GetMouseWorldPositions();
    }
    private void Show(BuildingTypeSO buildingGhost)
    {
        buildingGhostSprite.SetActive(true);
        resourceNearByOverlay.SetActive(true);
        resourceNearBy.SetResourceGeneretor(buildingGhost.resourceGeneratorData);
        buildingGhostSprite.GetComponent<SpriteRenderer>().sprite = buildingGhost.sprite;
    }
    private void Hide()
    {
        buildingGhostSprite.SetActive(false);
        resourceNearByOverlay.SetActive(false);
    }
}
