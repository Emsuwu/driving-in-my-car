using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
    private string _buttonTextUnselected;
    private string _buttonTextSelected;
    private EventTrigger _eventTrigger;
    [SerializeField] private TMPro.TMP_Text _text;
    private void Awake()
    {
        _buttonTextUnselected = _text.text;
        _buttonTextSelected = "> " + _text.text;

        AddTriggers();
    }
    private void AddTriggers()
    {
        _eventTrigger = gameObject.AddComponent<EventTrigger>();
        
        EventTrigger pointerEnter = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        pointerEnterEntry.callback.AddListener((eventData) => { Select(); });
        _eventTrigger.triggers.Add(pointerEnterEntry);

        EventTrigger pointerExit = GetComponentInParent<EventTrigger>();
        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
        pointerExitEntry.eventID = EventTriggerType.PointerExit;
        pointerExitEntry.callback.AddListener((eventData) => { Deselect(); });
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
    public void Select() {
        _text.text = _buttonTextSelected;
        AudioManager.PlayOneShot(AudioEvents.Current.ButtonHoverSound, PlayerMovement.Instance.transform);
    }
    public void Deselect()
    {
        _text.text = _buttonTextUnselected;
    }
    public void OnTrigger()
    {
        Debug.Log("SayHi");
    }
}
