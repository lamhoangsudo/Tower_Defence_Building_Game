using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEnterExitEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public event EventHandler OnEnter;
    public event EventHandler OnExit;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke(this, EventArgs.Empty);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        OnExit?.Invoke(this, EventArgs.Empty);
    }
}
