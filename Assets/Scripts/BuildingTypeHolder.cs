using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingTypeHolder : MonoBehaviour
{
    public BuildingTypeSO buildingTypeSO;
    [SerializeField]
    private Transform demolish;
    private void OnMouseEnter()
    {
        if (demolish != null) { demolish.gameObject.SetActive(true); }
    }
    private void OnMouseExit()
    {
        if (demolish != null) { demolish.gameObject.SetActive(false); }
    }
}
