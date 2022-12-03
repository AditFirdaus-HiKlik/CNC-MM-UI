using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CommandView : MonoBehaviour
{
    public Transform content;
    public GameObject commandPanelPrefab;
    public ToggleGroup toggleGroup;

    public CommandPanel[] commandPanels = new CommandPanel[0];
    public UnityEvent<int> OnIndexSelected;

    [HideInInspector] public List<Command> commands = new List<Command>();

    public void Select(CommandPanel commandPanel)
    {
        OnIndexSelected.Invoke(commandPanel.index);
    }

    public void UpdatePanels(List<Command> commands)
    {
        if (!content) return;
        if (!commandPanelPrefab) return;

        ClearPanels();
        CreatePanels(commands);
    }

    void CreatePanels(List<Command> commands)
    {
        this.commands = commands;

        commandPanels = new CommandPanel[commands.Count];

        for (int i = 0; i < commandPanels.Length; i++)
        {
            CommandPanel commandPanel = Instantiate(commandPanelPrefab, content).GetComponent<CommandPanel>();

            commandPanel.Init(this, commands[i], i);

            commandPanels[i] = commandPanel;
        }

        if (commandPanels.Length > 0) commandPanels[0].refToggle.isOn = true;
    }

    void ClearPanels()
    {
        foreach (CommandPanel commandPanel in commandPanels)
        {
            if (commandPanel) Destroy(commandPanel.gameObject);
        }
    }
}
