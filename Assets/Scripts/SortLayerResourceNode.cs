using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortLayerResourceNode : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float positionOffSet;
    [SerializeField] private bool runOne;
    private void Awake()
    {
        spriteRenderer = transform.GetComponent<SpriteRenderer>();
    }
    private void LateUpdate()
    {
        float precisionMultiplier = 2f;
        spriteRenderer.sortingOrder = (int)(-(transform.position.y - positionOffSet) * precisionMultiplier);
        if (runOne)
        {
            Destroy(this);
        }
    }
}
