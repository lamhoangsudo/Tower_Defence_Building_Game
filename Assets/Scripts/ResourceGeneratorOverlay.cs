using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGeneretor resourceGeneretor;
    [SerializeField] private Transform bar;
    [SerializeField] private Transform icon;
    [SerializeField] private Transform text;
    private float timeMaxScale;
    private void Start()
    {
        ResourceGeneratorData data = resourceGeneretor.resourceGeneratorData;
        icon.GetComponent<SpriteRenderer>().sprite = data.resourceTypeSO.sprite;
        timeMaxScale = bar.localScale.x;
        text.GetComponent<TextMeshPro>().text = resourceGeneretor.GetResourceGeneratorPerSecond().ToString("F1");
    }
    private void Update()
    {
        bar.localScale = new Vector3(resourceGeneretor.GetTimeNormalize() * timeMaxScale, 1, 1);
    }
    private void Show()
    {
        this.gameObject.SetActive(true);
    }
    private void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
