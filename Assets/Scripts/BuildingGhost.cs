using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    [SerializeField] private GameObject BuildingGhostSprite;
    private void Awake()
    {
        Hide();
    }
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += Instance_OnActiveBuildingTypeChanged;
    }

    private void Instance_OnActiveBuildingTypeChanged(object sender, BuildingTypeSO e)
    {
        if (e != null) Show(e.sprite);
        else Hide();
    }

    private void Update()
    {
        transform.position = UtilClass.GetMouseWorldPositions();
    }
    private void Show(Sprite ghostSprite)
    {
        BuildingGhostSprite.SetActive(true);
        BuildingGhostSprite.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    private void Hide()
    {
        BuildingGhostSprite.SetActive(false);
    }
}
