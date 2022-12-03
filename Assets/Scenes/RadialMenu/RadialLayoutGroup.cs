using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialLayoutGroup : MonoBehaviour
{
    public float radius = 100f;

    [HideInInspector] public Vector2 normalizedMousePosition;
    [HideInInspector] public float currentAngle;

    [HideInInspector] public RadialOption[] radialLayouts;

    [HideInInspector] public int selectionIndex;
    [HideInInspector] public int previousSelectionIndex;

    [HideInInspector] public RadialOption currentRadialOption;
    [HideInInspector] public RadialOption previousRadialOption;

    void OnValidate()
    {
        UpdateGroup();
    }

    void Start()
    {
        UpdateGroup();
    }

    void Update()
    {
        ManageInput();
        if (Input.GetMouseButtonDown(0)) ClickOption();
    }

    public void ClickOption() // * Untuk select option yang dipilih
    {
        currentRadialOption?.Click();
    }

    [ContextMenu("Update Group")]
    public void UpdateGroup() // * Untuk Mengupdate layout dari Radial Layout
    {
        InitializeChildOptions();
        UpdateLayout();
    }

    void ManageInput()
    {
        float anglePerElement = 360f / radialLayouts.Length;

        Vector2 mousePosition = Input.mousePosition;
        normalizedMousePosition = new Vector2(mousePosition.x - Screen.width / 2, mousePosition.y - Screen.height / 2).normalized;

        currentAngle = Mathf.Atan2(normalizedMousePosition.y, normalizedMousePosition.x) * Mathf.Rad2Deg;
        currentAngle = (currentAngle + 360 + (anglePerElement / 2)) % 360;

        selectionIndex = (int)((currentAngle) / (anglePerElement));

        if (selectionIndex != previousSelectionIndex)
        {
            previousRadialOption?.SetHighlight(false);

            currentRadialOption = radialLayouts[selectionIndex];
            currentRadialOption.SetHighlight(true);

            previousSelectionIndex = selectionIndex;
            previousRadialOption = currentRadialOption;
        }
    }

    public void InitializeChildOptions()
    {
        radialLayouts = GetComponentsInChildren<RadialOption>();
    }

    public void UpdateLayout()
    {
        float anglePerElement = 360f / radialLayouts.Length;
        for (int i = 0; i < radialLayouts.Length; i++)
        {
            RadialOption radialOption = radialLayouts[i];
            float angle = -anglePerElement * i;

            radialOption.transform.localPosition = GetPositionOnCircle(angle, radius);
        }
    }

    Vector3 GetPositionOnCircle(float angle, float radius)
    {
        float angleInRadians = (angle + 90) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(angleInRadians) * radius, Mathf.Cos(angleInRadians) * radius);
    }
}
