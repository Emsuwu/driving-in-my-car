using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuKeybindButton : MenuButton
{
    [Header("Rebind")]
    [SerializeField] private InputActionAsset _inputActions;
    [SerializeField] private InputActionReference _rebindAction;
    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
    [SerializeField] private string _targetMapName = "Player";

    [Header("CompositeSettings")]
    [SerializeField] private bool _isComposite;
    [SerializeField, ShowIf("_isComposite")] private string _compositeName;
    private InputAction _action;

    

    string _boundKeyName = "?";

    public void UpdateBoundKeyName()
    {
        _action = _rebindAction.action;

        int bindingIndex = 0;
        if (_isComposite)
        {
            bindingIndex = _action.bindings.IndexOf(x => x.name == _compositeName);
        }

        string _boundKeyName = _action.bindings[bindingIndex].effectivePath;
    }

    public void Rebind()
    {
        _boundKeyName = "...";

        int bindingIndex = 0;
        if (_isComposite)
        {
            bindingIndex = _action.bindings.IndexOf(x => x.name == _compositeName);
        }
        _inputActions.FindActionMap(_targetMapName).Disable();

        _rebindingOperation = _action.PerformInteractiveRebinding().OnComplete(operation => CompleteRebind());
        _rebindingOperation.Start();
    }

    public void CompleteRebind()
    {
        _rebindingOperation.Dispose();
        _inputActions.FindActionMap(_targetMapName).Enable();
        _boundKeyName = _action.bindings[0].effectivePath;
        var rebinds = _inputActions.SaveBindingOverridesAsJson();
        Debug.Log(rebinds);
        PlayerPrefs.SetString("Rebinds", rebinds);

        UpdateSettings();
    }

    private new void OnEnable()
    {
        base.OnEnable();

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { Rebind(); });
        eventTrigger.triggers.Add(entry);

        EventTrigger eventTrigger2 = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.Submit;
        entry2.callback.AddListener((eventData) => { Rebind(); });
        eventTrigger2.triggers.Add(entry2);


        UpdateBoundKeyName();
    }

    private new void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        string box = $" [{_boundKeyName}]";

        if (isSelected)
        {
            text.text = buttonTextSelected + box;
        }
        else
        {
            text.text = buttonTextUnselected + box;
        }
    }
}
