using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool Parameter Settings", menuName = "ToolParameter/Tool Parameter Settings")]
public class ToolParameterSettings : ScriptableObject
{
    public ToolParameterData data;
}

[System.Serializable]
public class ToolParameterData
{
    public List<ToolParameter> toolParameters;
}