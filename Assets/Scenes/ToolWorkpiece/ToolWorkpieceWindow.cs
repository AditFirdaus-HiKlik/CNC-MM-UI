using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolWorkpieceWindow : MonoBehaviour
{
    [Header("Parameter Editors")]
    public ParameterEditor editorMaterial;
    public ParameterEditor editorLength;
    public ParameterEditor editorWidth;
    public ParameterEditor editorHeight;

    [Header("Rules")]
    public int LengthMin = 50;
    public int LengthMax = 150;
    public int WidthMin = 50;
    public int WidthMax = 150;
    public int HeightMin = 50;
    public int HeightMax = 150;

    [HideInInspector] public WorkpieceMaterial workspaceMaterial;
    [HideInInspector] public int workspaceLength;
    [HideInInspector] public int workspaceWidth;
    [HideInInspector] public int workspaceHeight;

    private void Start()
    {
        MoveLength(0);
        MoveWidth(0);
        MoveHeight(0);
        UpdateEditors();
    }

    public void MoveMaterial(int step)
    {
        int numTypes = 2;
        int tt = (int)workspaceMaterial;
        tt = Mathf.Clamp(tt + step, 0, numTypes - 1);
        workspaceMaterial = (WorkpieceMaterial)tt;
    }

    public void MoveLength(int step)
    {
        workspaceLength = Mathf.Clamp(workspaceLength + step, LengthMin, LengthMax);
    }
    public void MoveWidth(int step)
    {
        workspaceWidth = Mathf.Clamp(workspaceWidth + step, WidthMin, WidthMax);
    }
    public void MoveHeight(int step)
    {
        workspaceHeight = Mathf.Clamp(workspaceHeight + step, HeightMin, HeightMax);
    }

    public void UpdateEditors()
    {
        editorMaterial.SetValueText(workspaceMaterial.ToString());
        editorLength.SetValueText(workspaceLength.ToString());
        editorWidth.SetValueText(workspaceWidth.ToString());
        editorHeight.SetValueText(workspaceHeight.ToString());
    }
}

public enum WorkpieceMaterial
{
    Carbida,
    Aluminium
}