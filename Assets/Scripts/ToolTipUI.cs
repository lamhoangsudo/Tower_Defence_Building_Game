using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Internal;

public class ToolTipUI : MonoBehaviour
{
    public static ToolTipUI Instance { get; private set; }
    [SerializeField] private RectTransform backGround;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform canvas;
    private RectTransform rectTransform;
    private ToolTipTimerShow toolTipTimerShow;
    private void Awake()
    {
        Instance = this;
        rectTransform = this.GetComponent<RectTransform>();
        Hide();
    }
    private void Update()
    {
        ToolTipMovement(rectTransform);
        if(toolTipTimerShow != null)
        {
            toolTipTimerShow.timer -= Time.deltaTime;
            if(toolTipTimerShow.timer <= 0)
            {
                Hide();
            }
        }
    }
    private void SetText(string input)
    {
        text.SetText(input);
        text.ForceMeshUpdate(true);
        Vector2 sizeText = text.GetRenderedValues(false);
        Vector2 padding = new(8, 8);
        backGround.sizeDelta = sizeText + padding;
    }
    private void ToolTipMovement(RectTransform rectTransform)
    {
        Vector2 anchoredPosition = Input.mousePosition / (Vector2)canvas.localScale;
        if(anchoredPosition.x + backGround.rect.width >= canvas.rect.width)
        {
            anchoredPosition.x = canvas.rect.width - backGround.rect.width;
        }
        if (anchoredPosition.y + backGround.rect.height >= canvas.rect.height)
        {
            anchoredPosition.y = canvas.rect.height - backGround.rect.height;
        }
        rectTransform.anchoredPosition = anchoredPosition;
    }
    public void Show(string input, ToolTipTimerShow toolTipTimerShow = null)
    {
        SetText(input);
        this.toolTipTimerShow = toolTipTimerShow;
        ToolTipMovement(rectTransform);
        this.gameObject.SetActive(true);
    }
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
    public class ToolTipTimerShow
    {
        public float timer;
    }
}
