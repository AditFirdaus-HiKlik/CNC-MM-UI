using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "Action")]
public class Action : ScriptableObject
{
    public int currentCommandIndex = 0;
    public List<Command> commands;
}
