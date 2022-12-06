using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolParameterWindow : MonoBehaviour
{
    public int toolIndex = 0;
    public ToolParameterSettings toolParameterSettings;

    public ToolParameterData toolParameterData { set => toolParameterSettings.data = value; get => toolParameterSettings.data; }
    public ToolParameterData _toolParameterData;

    [Header("Parameter Editors")]
    public ParameterEditor editorTool;
    public ParameterEditor editorType;
    public ParameterEditor editorDesign;
    public ParameterEditor editorDiameter;
    public ParameterEditor editorLength;

    [Header("Rules")]
    public int diameterMin;
    public int diameterMax;
    public int lengthMin;
    public int lengthMax;

    public ToolParameter current => _toolParameterData.toolParameters[toolIndex];
    [HideInInspector] public ToolCutterType toolType { set => current.type = value; get => current.type; }
    [HideInInspector] public ToolCutterDesign toolDesign { set => current.design = value; get => current.design; }
    [HideInInspector] public int toolDiameter { set => current.diameter = value; get => current.diameter; }
    [HideInInspector] public int toolLength { set => current.length = value; get => current.length; }

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_toolParameterData);
        toolParameterData = JsonUtility.FromJson<ToolParameterData>(json);
    }

    public void Load()
    {
        string json = JsonUtility.ToJson(toolParameterData);
        _toolParameterData = JsonUtility.FromJson<ToolParameterData>(json);

        MoveDiameter(0);
        MoveLength(0);

        UpdateEditors();
    }

    public void MoveTool(int step)
    {
        toolIndex = Mathf.Clamp(toolIndex + step, 0, _toolParameterData.toolParameters.Count - 1);
        Load();
    }

    public void MoveType(int step)
    {
        int numTypes = 2;
        int tt = (int)toolType;
        tt = Mathf.Clamp(tt + step, 0, numTypes - 1);
        toolType = (ToolCutterType)tt;
    }

    public void MoveDesign(int step)
    {
        int numTypes = 2;
        int td = (int)toolDesign;
        td = Mathf.Clamp(td + step, 0, numTypes - 1);
        toolDesign = (ToolCutterDesign)td;
    }

    public void MoveDiameter(int step)
    {
        toolDiameter = Mathf.Clamp(toolDiameter + step, diameterMin, diameterMax);
    }

    public void MoveLength(int step)
    {
        toolLength = Mathf.Clamp(toolLength + step, lengthMin, lengthMax);
    }

    public void SetTool(string value)
    {
        int parsedValue = toolIndex;

        int.TryParse(value, out parsedValue);

        if (parsedValue != toolIndex)
        {
            toolIndex = Mathf.Clamp(toolIndex, 0, _toolParameterData.toolParameters.Count - 1);
            Load();
        }

        UpdateEditors();
    }

    public void SetType(string value)
    {
        int numTypes = 2;
        int tt = (int)toolType;

        int.TryParse(value, out tt);

        tt = Mathf.Clamp(tt, 0, numTypes - 1);
        toolType = (ToolCutterType)tt;

        UpdateEditors();
    }

    public void SetDesign(string value)
    {
        int numTypes = 2;
        int td = (int)toolDesign;

        int.TryParse(value, out td);

        td = Mathf.Clamp(td, 0, numTypes - 1);
        toolDesign = (ToolCutterDesign)td;

        UpdateEditors();
    }

    public void SetDiameter(string value)
    {
        int parsedValue = toolDiameter;

        int.TryParse(value, out parsedValue);

        toolDiameter = Mathf.Clamp(parsedValue, diameterMin, diameterMax);

        UpdateEditors();
    }

    public void SetLength(string value)
    {
        int parsedValue = toolLength;

        int.TryParse(value, out parsedValue);

        toolLength = Mathf.Clamp(parsedValue, lengthMin, lengthMax);

        UpdateEditors();
    }

    public void UpdateEditors()
    {
        editorTool.SetValueText((toolIndex + 1).ToString()); // di ubah menjadi editorTool.SetValueText((toolIndex + 1).ToString());
        editorType.SetValueText(toolType.ToString());
        editorDesign.SetValueText(toolDesign.ToString());
        editorDiameter.SetValueText(toolDiameter.ToString());
        editorLength.SetValueText(toolLength.ToString());
    }
}

[System.Serializable]
public class ToolParameter
{
    public ToolCutterType type;
    public ToolCutterDesign design;

    public int diameter;
    public int length;
}

public enum ToolCutterType
{
    BallEnd,
    FlatEnd
}

public enum ToolCutterDesign
{
    Whole,
    Partial
}