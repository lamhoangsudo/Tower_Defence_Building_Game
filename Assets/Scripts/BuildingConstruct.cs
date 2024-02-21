using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class BuildingConstruct : MonoBehaviour
{
    private BuildingTypeSO buildingType;
    private float timeBuildConstruct;
    private float timeBuildConstructMax;
    private BuildingTypeHolder buildingTypeHolder;
    private BoxCollider2D boxCollider;
    private Material material;
    [SerializeField]
    private Transform sprite;
    private Transform buildingPlacedParticles;
    public static BuildingConstruct CreateBuildConstructTitle(Vector3 position, BuildingTypeSO buildingTypeSO)
    {
        Transform buildConstructTitleTrf = Resources.Load<Transform>("PfBuildConstruct");
        Transform buildConstructTitle = Instantiate(buildConstructTitleTrf, position, Quaternion.identity);
        buildConstructTitle.GetComponent<BuildingConstruct>().Setup(buildingTypeSO);
        return buildConstructTitle.GetComponent<BuildingConstruct>();
    }
    private void Awake()
    {
        buildingTypeHolder = transform.GetComponent<BuildingTypeHolder>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        buildingPlacedParticles = Resources.Load<Transform>("pfBuildingPlacedParticles");
    }
    private void Start()
    {
        sprite.GetComponent<SpriteRenderer>().sprite = buildingType.sprite;
        material = sprite.GetComponent<SpriteRenderer>().material;
        timeBuildConstruct = timeBuildConstructMax;
    }
    private void Update()
    {
        TimeConstruct();
    }
    private void TimeConstruct()
    {
        timeBuildConstruct -= Time.deltaTime;
        material.SetFloat("_Progress", 1 - GetTimeBuildNormalized());
        if (timeBuildConstruct <= 0)
        {
            timeBuildConstruct = timeBuildConstructMax;
            Instantiate(buildingType.prefab, transform.position, Quaternion.identity);
            Instantiate(buildingPlacedParticles, transform.position, Quaternion.identity);
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingPlaced);
            Destroy(gameObject);
        }
    }
    private void Setup(BuildingTypeSO buildingType)
    {
        timeBuildConstructMax = buildingType.timeBuildConstructMax;
        this.buildingType = buildingType;
        buildingTypeHolder.buildingTypeSO = buildingType;
        boxCollider.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        boxCollider.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
    }
    public float GetTimeBuildNormalized()
    {
        return timeBuildConstruct / timeBuildConstructMax;
    }
}
