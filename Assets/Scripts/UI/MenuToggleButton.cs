using UnityEngine;
using UnityEngine.EventSystems;

public class MenuToggleButton : MenuButton
{
    [SerializeField] private string _prefKey;
    private bool _isOn;
    [SerializeField] private bool _defaultValue;
     
    private void Update()
    {
        string box = _isOn ? " [On]" : " [Off]";

        if (isSelected)
        {
            text.text = buttonTextSelected + box;
        }
        else
        {
            text.text = buttonTextUnselected + box;
        }
    }
    new private void Awake()
    {
        base.Awake();

        EventTrigger eventTrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { OnTrigger(); });
        eventTrigger.triggers.Add(entry);

        EventTrigger eventTrigger2 = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.Submit;
        entry2.callback.AddListener((eventData) => { OnTrigger(); });
        eventTrigger2.triggers.Add(entry2);
    }
    public void OnTrigger()
    {
        _isOn = !_isOn;
        PlayerPrefs.SetInt(_prefKey, _isOn ? 1 : 0);
        UpdateSettings();
    }
    new private void OnEnable()
    {
        base.OnEnable();

        if (!PlayerPrefs.HasKey(_prefKey))
        {
            PlayerPrefs.SetInt(_prefKey, Meowtils.BoolToInt(_defaultValue));
        }
        _isOn = Meowtils.IntToBool( PlayerPrefs.GetInt(_prefKey) );
    }
}
