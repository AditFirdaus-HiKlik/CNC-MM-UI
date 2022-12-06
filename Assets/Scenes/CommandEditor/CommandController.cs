using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    public ActionWindow actionWindow;

    public CommandPanel currentCommandPanel => actionWindow.currentCommandPanel;

    public Command currentCommand => currentCommandPanel.command;

    public string commandCode
    {
        set => currentCommand.code = value;
        get => currentCommand.code;
    }

    public int pointerIndex
    {
        get => currentCommandPanel ? currentCommandPanel.caretPosition : 0;
        set => currentCommandPanel.caretPosition = value;
    }

    public static int _pointerIndex;

    public void MovePointer(int offset)
    {
        _pointerIndex += offset;
        pointerIndex += offset;
        ClampPointerIndex();
    }

    public void MovePointerLeft() => MovePointer(-1);

    public void MovePointerRight() => MovePointer(1);

    public void MovePointerToStart()
    {
        currentCommandPanel.refCode.MoveTextStart(false);
    }

    public void MovePointerToEnd()
    {
        currentCommandPanel.refCode.MoveTextEnd(false);
    }

    public void MoveCommand(int offset)
    {
        actionWindow.SelectCommandIndex(actionWindow.commandIndex + offset);
    }

    public void MoveCommandUp() => MoveCommand(-1);

    public void MoveCommandDown() => MoveCommand(1);

    public void Insert(string input = "")
    {
        string code = commandCode;

        _pointerIndex = pointerIndex;

        MovePointerRight();

        if (code.Length == 0) code += input;
        else code = code.Insert(pointerIndex, input);

        currentCommandPanel.SetCode(code, pointerIndex);

        Debug.Log(_pointerIndex);


    }

    public void Backspace()
    {
        ClampPointerIndex();

        if (commandCode.Length != 0)
        {
            commandCode = commandCode.Remove(pointerIndex, 1);
        }

        MovePointerLeft();
        currentCommand.UpdateUI();
    }

    public void ClampPointerIndex()
    {
        pointerIndex = Mathf.Clamp(pointerIndex, 0, currentCommand.code.Length);
    }

    public void DeleteCommand()
    {
        if (actionWindow.commandIndex < actionWindow.currentAction.commands.Count - 1)
        {
            actionWindow.currentAction.commands.RemoveAt(actionWindow.commandIndex);
            actionWindow.UpdateCommandView(actionWindow.currentAction);
        }
    }

    public void CreateCommand()
    {
        actionWindow.currentAction.commands.Add(new Command());
        actionWindow.UpdateCommandView(actionWindow.currentAction);
    }

    public void ExecuteAction()
    {
        Debug.Log("Execute");
    }
}
