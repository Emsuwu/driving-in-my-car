using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MenuKeybindButton : MenuButton
{
    [Header("Rebind")]
    [SerializeField] private InputActionAsset _inputActions;
    [SerializeField] private InputActionReference _rebindTargetAction;
    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;
    [SerializeField] private string _targetMapName = "Player";
    [SerializeField] private bool _isControllerRebind;

    [Header("Composite Settings")]
    [SerializeField] private bool _isComposite;
    [SerializeField, ShowIf("_isComposite")] private string _compositeName;
    private InputAction _action;


    string _boundKeyName = "?";

    public void UpdateBoundKeyName()
    {
        _action = _rebindTargetAction.action;

        int bindingIndex = 0;
        if (_isComposite)
        {
            bindingIndex = _action.bindings.IndexOf(x => x.name == _compositeName);
        }

        string rawPath = _action.bindings[bindingIndex].effectivePath;

        _boundKeyName = InputControlPath.ToHumanReadableString(
            rawPath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void Rebind()
    {
        string expectedControlType = _isControllerRebind ? "Gamepad" : "Key";
        string excludedControlType = _isControllerRebind ? "Key" : "Gamepad";

        _boundKeyName = "...";

        int bindingIndex = 0;
        if (_isComposite)
        {
            bindingIndex = _action.bindings.IndexOf(x => x.name == _compositeName);
        }
        _inputActions.FindActionMap(_targetMapName).Disable();

        _rebindingOperation = _action.PerformInteractiveRebinding(bindingIndex)
            .WithTargetBinding(bindingIndex)
            .WithControlsExcluding("Mouse")
            .WithControlsExcluding(excludedControlType)
            .WithExpectedControlType(expectedControlType)
            .OnComplete(operation => CompleteRebind())
            .OnCancel(operation => CompleteRebind());
        _rebindingOperation.Start();
    }

    public void CompleteRebind()
    {
        Debug.Log("Rebind complete!");

        _rebindingOperation.Dispose();
        _inputActions.FindActionMap(_targetMapName).Enable();
        _boundKeyName = _action.bindings[0].effectivePath;
        var rebinds = _inputActions.SaveBindingOverridesAsJson();
        Debug.Log(rebinds);
        PlayerPrefs.SetString("Rebinds", rebinds);

        UpdateBoundKeyName();
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
