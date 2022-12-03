using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ActionView : MonoBehaviour
{
    public Transform content;
    public GameObject actionPanelPrefab;
    public ToggleGroup toggleGroup;

    public ActionPanel[] actionPanels = new ActionPanel[0];
    public UnityEvent<int> OnIndexSelected;

    [HideInInspector] public List<Action> actions = new List<Action>();

    public void Select(ActionPanel actionPanel)
    {
        OnIndexSelected.Invoke(actionPanel.index);
    }

    public void UpdatePanels(List<Action> actions)
    {
        if (!content) return;
        if (!actionPanelPrefab) return;

        ClearPanels();
        CreatePanels(actions);
    }

    void CreatePanels(List<Action> actions)
    {
        this.actions = actions;

        actionPanels = new ActionPanel[actions.Count];

        for (int i = 0; i < actionPanels.Length; i++)
        {
            ActionPanel actionPanel = Instantiate(actionPanelPrefab, content).GetComponent<ActionPanel>();

            actionPanel.Init(this, i);

            actionPanels[i] = actionPanel;
        }

        if (actionPanels.Length > 0) actionPanels[0].refToggle.isOn = true;
    }

    void ClearPanels()
    {
        foreach (ActionPanel actionPanel in actionPanels)
        {
            if (actionPanel) Destroy(actionPanel.gameObject);
        }
    }
}
