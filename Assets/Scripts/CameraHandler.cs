using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private Camera minimap;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float zoomAmount;
    [SerializeField] private float zoomSpeed;
    public bool setEdgeScrolling { private set; get; }
    private float edgeScrolling;
    private float orthographicSize;
    private float targetOrthographicSize;
    public static CameraHandler instance;
    private void Awake()
    {
        instance = this;
        setEdgeScrolling = PlayerPrefs.GetInt("setEdgeScrolling", 0) == 1;
    }
    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
        edgeScrolling = 20f;
    }
    private void Update()
    {
        HandlerMovement();
        HandLerZoom();
    }
    private void HandlerMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (setEdgeScrolling)
        {
            if (Input.mousePosition.x > Screen.width - edgeScrolling)
            {
                x = 1f;
            }
            else if (Input.mousePosition.x < edgeScrolling)
            {
                x = -1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrolling)
            {
                y = 1f;
            }
            else if (Input.mousePosition.y < edgeScrolling)
            {
                y = -1f;
            }
        }
        Vector3 moveDir = new Vector3(x, y, 0).normalized;
        transform.position += moveDir * movementSpeed * Time.deltaTime;
    }
    private void HandLerZoom()
    {
        float maxZoom = 30f;
        float minZoom = 10f;
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minZoom, maxZoom);
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
        minimap.orthographicSize = orthographicSize * 4f;
    }
    public void setScrolling(bool setEdgeScrolling)
    {
        this.setEdgeScrolling = setEdgeScrolling;
        PlayerPrefs.SetInt("setEdgeScrolling", this.setEdgeScrolling ? 1 : 0);
    }
}
