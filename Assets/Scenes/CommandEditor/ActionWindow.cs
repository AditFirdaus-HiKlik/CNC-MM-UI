using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionWindow : MonoBehaviour
{
    public List<Action> actions;

    public ActionView actionView;
    public CommandView commandView;
    public CommandController commandController;
    public KeyPad keyPad;

    [HideInInspector] public int actionIndex = 0;
    [HideInInspector] public int commandIndex = 0;

    [HideInInspector] public Action currentAction => GetAction(actionIndex);
    [HideInInspector] public Command currentCommand => GetCommand(currentAction, commandIndex);

    [HideInInspector] public ActionPanel currentActionPanel => actionView.actionPanels[actionIndex];
    [HideInInspector] public CommandPanel currentCommandPanel => commandView.commandPanels[commandIndex];


    private void Start()
    {
        actionView.OnIndexSelected.AddListener(SelectActionIndex);
        commandView.OnIndexSelected.AddListener(SelectCommandIndex);
        keyPad.OnInputKey.AddListener(KeyInput);
        keyPad.OnInputSpecialKey.AddListener(KeyInputSpecial);

        UpdateActionView();
    }

    public void SelectActionIndex(int index)
    {
        actionIndex = index;
        UpdateCommandView(currentAction);
    }

    public void SelectCommandIndex(int index)
    {
        if (index != commandIndex)
        {
            commandIndex = index;

            commandController.actionWindow.commandView.commandPanels[commandIndex]?.Select();

            ClampCommandIndex();

            currentAction.currentCommandIndex = commandIndex;
        }
    }

    public void UpdateActionView()
    {
        actionView.UpdatePanels(actions);
    }

    public void UpdateCommandView(Action action)
    {
        commandView.UpdatePanels(action);
    }

    public Action GetAction(int index)
    {
        if (index < 0 || index >= actions.Count) return null;
        return actions[index];
    }

    public Command GetCommand(Action action, int commandIndex)
    {
        if (!action) return null;
        if (commandIndex < 0 || commandIndex >= action.commands.Count) return null;

        return action.commands[commandIndex];
    }

    public void KeyInput(string input)
    {
        commandController.Insert(input);
    }

    public void KeyInputSpecial(string input)
    {
        commandController.Invoke(input, 0);
    }

    public void ClampCommandIndex()
    {
        if (commandIndex < 0) commandIndex = 0;
        if (commandIndex >= currentAction.commands.Count) commandIndex = currentAction.commands.Count - 1;
    }
}
