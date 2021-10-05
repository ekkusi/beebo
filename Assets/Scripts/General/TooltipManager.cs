using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{

    public static TooltipManager _instance { get; private set; }
    [SerializeField]
    private RectTransform canvasRect;
    [SerializeField]
    private Camera uiCamera;
    private TextMeshProUGUI tmPro;
    private RectTransform backgroundRect;
    private RectTransform containerRect;
    private Vector3 tooltipPosition;
    void Awake()
    {
        _instance = this;

        backgroundRect = transform.Find("Background").GetComponent<RectTransform>();
        tmPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        containerRect = GetComponent<RectTransform>();

        _instance.HideTooltip_private();
    }

    private void SetText(string text)
    {
        tmPro.SetText(text);
        tmPro.ForceMeshUpdate();

        Vector2 size = tmPro.GetRenderedValues();
        backgroundRect.sizeDelta = new Vector2(size.x + tmPro.margin.x + tmPro.margin.z, size.y + tmPro.margin.y + tmPro.margin.w);
    }

    private void ShowTooltip_private(string text, Vector3 position)
    {
        tooltipPosition = position;
        if (!backgroundRect.gameObject.activeInHierarchy)
        {
            backgroundRect.gameObject.SetActive(true);
            tmPro.gameObject.SetActive(true);
        }
        SetText(text);
    }

    private void HideTooltip_private()
    {
        backgroundRect.gameObject.SetActive(false);
        tmPro.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Update _instance if gets put back to null. This happens on scene change
        if (_instance == null)
        {
            _instance = this;
        }

        if (tooltipPosition != null)
        {

            Vector2 anchoredPosition = tooltipPosition / canvasRect.localScale.x;

            // Don't go over right of screen
            if (anchoredPosition.x + backgroundRect.rect.width > canvasRect.rect.width)
            {
                anchoredPosition.x = canvasRect.rect.width - backgroundRect.rect.width;
            }

            // Don't go over top of screen
            if (anchoredPosition.y + backgroundRect.rect.height > canvasRect.rect.height)
            {
                anchoredPosition.y = canvasRect.rect.height - backgroundRect.rect.height;
            }
            anchoredPosition.y += 20;

            containerRect.anchoredPosition = anchoredPosition;
        }
    }

    public static void ShowtoolTip(string text)
    {
        _instance.ShowTooltip_private(text, Input.mousePosition);
    }

    public static void ShowtoolTip(string text, Vector3 position)
    {
        _instance.ShowTooltip_private(text, position);
    }

    public static void HideTooltip()
    {
        if (_instance.backgroundRect.gameObject.activeInHierarchy)
        {
            _instance.HideTooltip_private();
        }
    }

    public static bool IsActive()
    {
        RectTransform backgroundRect = _instance.transform.Find("Background").GetComponent<RectTransform>();
        return backgroundRect.gameObject.activeInHierarchy;
    }
}
