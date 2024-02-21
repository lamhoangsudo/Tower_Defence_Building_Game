using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountCounstruct : MonoBehaviour
{
    [SerializeField]
    private BuildingConstruct buildingConstruct;
    [SerializeField]
    private Image timeCount;
    private void Update()
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        float count = buildingConstruct.GetTimeBuildNormalized();
        timeCount.fillAmount = 1 - count;
    }
}
