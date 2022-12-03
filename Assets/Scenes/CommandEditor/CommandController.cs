using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public ActionWindow actionWindow;

    public string commandCode
    {
        set
        {
            actionWindow.currentCommand.code = value;
            ClampPointerIndex();
        }
        get
        {
            return actionWindow.currentCommand.code;
        }
    }

    int _pointerIndex;
    public int pointerIndex
    {
        get
        {
            // Make sure pointerIndex is within bounds
            ClampPointerIndex();
            return _pointerIndex;
        }
        set
        {
            _pointerIndex = value;
            ClampPointerIndex();
            UpdateCaretPosition();
        }
    }

    public void ClampPointerIndex()
    {
        if (_pointerIndex < 0) _pointerIndex = 0;
        if (_pointerIndex >= commandCode.Length) _pointerIndex = commandCode.Length - 1;
    }

    public void UpdateCaretPosition()
    {
        ClampPointerIndex();
        actionWindow.currentCommandPanel.SetCaretIndex(_pointerIndex);
    }

    public void MovePointer(int offset)
    {
        pointerIndex += offset;
    }

    public void MovePointerLeft() => MovePointer(-1);
    public void MovePointerRight() => MovePointer(1);

    public void MovePointerToStart()
    {
        pointerIndex = 0;
    }

    public void MovePointerToEnd()
    {
        pointerIndex = commandCode.Length - 1;
    }

    public void MoveCommand(int offset)
    {
        actionWindow.SelectCommandIndex(actionWindow.commandIndex + offset);

    }

    public void MoveCommandUp() => MoveCommand(-1);
    public void MoveCommandDown() => MoveCommand(1);

    public void Insert(string input = "")
    {
        if (commandCode.Length == 0) commandCode += input;
        else commandCode = commandCode.Insert(pointerIndex + 1, input);

        MovePointerRight();
    }

    // Backspace
    public void Backspace()
    {
        if (commandCode.Length != 0)
        {
            commandCode = commandCode.Remove(pointerIndex, 1);
            MovePointerLeft();
        }
    }

    // Delete command
    public void DeleteCommand()
    {
        if (actionWindow.commandIndex < actionWindow.currentAction.commands.Count - 1)
        {
            actionWindow.currentAction.commands.RemoveAt(actionWindow.commandIndex);
            actionWindow.UpdateCommandView(actionWindow.currentAction.commands);
        }
    }

    // New Command
    public void CreateCommand()
    {
        actionWindow.currentAction.commands.Add(new Command());
        actionWindow.UpdateCommandView(actionWindow.currentAction.commands);
    }

    public void ExecuteAction()
    {
        Debug.Log("Execute");
    }
}
