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

    public CommandPanel currentCommandPanel;
    public CommandPanel[] commandPanels = new CommandPanel[0];
    public UnityEvent<int> OnIndexSelected;

    public Action action;
    [HideInInspector] public List<Command> commands = new List<Command>();

    public void Select(CommandPanel commandPanel)
    {
        currentCommandPanel?.SetSelectState(false);

        currentCommandPanel = commandPanel;

        currentCommandPanel?.SetSelectState(true);

        OnIndexSelected.Invoke(currentCommandPanel.index);
    }

    public void UpdatePanels(Action action)
    {
        if (!content) return;
        if (!commandPanelPrefab) return;

        ClearPanels();
        CreatePanels(action);
    }

    void CreatePanels(Action action)
    {
        this.action = action;

        commandPanels = new CommandPanel[action.commands.Count];

        for (int i = 0; i < commandPanels.Length; i++)
        {
            Command command = action.commands[i];
            CommandPanel commandPanel = Instantiate(commandPanelPrefab, content).GetComponent<CommandPanel>();

            commandPanel.Init(this, command, i);

            if (i == action.currentCommandIndex)
            {
                commandPanel.Select();
            }

            commandPanels[i] = commandPanel;
        }

        if (commandPanels.Length > 0 && !currentCommandPanel) commandPanels[0].Select();
    }

    void ClearPanels()
    {
        foreach (CommandPanel commandPanel in commandPanels)
        {
            if (commandPanel) Destroy(commandPanel.gameObject);
        }
    }
}
