using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [Header("Button text")]
    [SerializeField] public TMPro.TMP_Text text;
    private bool _isSelected;
    public bool isSelected { get { return _isSelected; } }
    [Header("Set this MenuButton as the initial selection?")]
    [SerializeField] private bool _initialSelection;
    [HideInInspector] public string buttonTextUnselected;
    [HideInInspector] public string buttonTextSelected;

    public Menu RootMenu;

    [Header("Set cursor type on hover?")]
    [SerializeField] private MouseType.CURSOR _cursorType;

    private EventTrigger _eventTrigger;
    [HideInInspector] public Button button;
    public void UpdateSettings()
    {
        PlayerMovement.Instance.UpdateSettings();
        AudioManager.UpdateBusesFromPrefs();
        GlobalSettings.Instance.UpdateSettings();
        Debug.Log("Updated 1");
    }
    public void Awake()
    {
        buttonTextUnselected = text.text;
        buttonTextSelected = "> " + text.text;
        button = GetComponent<Button>();

        AddTriggers();
    }

    private void Start()
    {
        if (_initialSelection) {
            RootMenu.InitialSelection = this;
        }
    }

    public void OnEnable()
    {
        text.text = buttonTextUnselected;
        if (_initialSelection)
        {
            text.text = buttonTextSelected;
            GetComponent<Button>().Select();
        }
    }

    private void AddTriggers()
    {
        _eventTrigger = gameObject.AddComponent<EventTrigger>();
        
        EventTrigger pointerEnter = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        pointerEnterEntry.callback.AddListener((eventData) => { MouseHover(); });
        _eventTrigger.triggers.Add(pointerEnterEntry);

        EventTrigger pointerExit = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
        pointerExitEntry.eventID = EventTriggerType.PointerExit;
        pointerExitEntry.callback.AddListener((eventData) => { MouseUnhover(); });
        _eventTrigger.triggers.Add(pointerExitEntry);

        EventTrigger selected = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry selectedEntry = new EventTrigger.Entry();
        selectedEntry.eventID = EventTriggerType.Select;
        selectedEntry.callback.AddListener((eventData) => { Select(); });
        _eventTrigger.triggers.Add(selectedEntry);

        EventTrigger deselected = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry deselectedEntry = new EventTrigger.Entry();
        deselectedEntry.eventID = EventTriggerType.Deselect;
        deselectedEntry.callback.AddListener((eventData) => { Deselect(); });
        _eventTrigger.triggers.Add(deselectedEntry);
    }

    public void MouseHover()
    {
        EventSystem.current.SetSelectedGameObject(null);
        MouseType.SetCursor(_cursorType);
        Select();
    }
    public void MouseUnhover()
    {
        MouseType.Pointer();
        Deselect();
    }
    public void Select() {
        _isSelected = true;
        text.text = buttonTextSelected;
        AudioManager.PlayOneShot(AudioEvents.Current.ButtonHoverSound, PlayerMovement.Instance.transform);
    }
    public void Deselect()
    {
        
        _isSelected = false;
        text.text = buttonTextUnselected;
    }
}
