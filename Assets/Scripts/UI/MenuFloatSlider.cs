using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuFloatSlider : MenuButton
{
    [Header("Set minimum and maximum for slider")]
    [SerializeField] float _clampMin;
    [SerializeField] float _clampMax;
    [Header("What PlayerPref key should be set?")]
    [SerializeField] string _prefKey;
    [Header("What is the initial value if key doesn't exist?")]
    [SerializeField] float _defaultValue;
    [Header("How quickly should the slider change?")]
    [SerializeField] float _changeSpeed = 1f;
    [SerializeField] float _dragChangeSpeed = .005f;
    [Header("Percentage denominator")]
    [SerializeField] private float _denominator = 1f;

    private float _lastMouseX;

    new private void Awake()
    {
        base.Awake();
    }
    new private void OnEnable()
    {
        base.OnEnable();

        if (!PlayerPrefs.HasKey(_prefKey))
        {
            PlayerPrefs.SetFloat(_prefKey, _defaultValue);
        }
    }
    private void Update()
    {
        text.text = buttonTextUnselected + $" [{Mathf.Round(PlayerPrefs.GetFloat(_prefKey) / _denominator * 100)}%]";

        if (!isSelected) return;

        if (PauseController.Instance.NavigateAxis.x > .1)
        {
            float n = PlayerPrefs.GetFloat(_prefKey) + Time.deltaTime * _changeSpeed;
            PlayerPrefs.SetFloat(_prefKey, n);
            UpdateSettings();
        }
        else if (PauseController.Instance.NavigateAxis.x < -.1)
        {
            float n = PlayerPrefs.GetFloat(_prefKey) - Time.deltaTime * _changeSpeed;
            PlayerPrefs.SetFloat(_prefKey, PlayerPrefs.GetFloat(_prefKey) - Time.deltaTime);
            UpdateSettings();
        }
        else if (Mouse.current.leftButton.isPressed)
        {
            float mxDelta = Mouse.current.position.ReadValue().x - _lastMouseX;

            float n = PlayerPrefs.GetFloat(_prefKey) + mxDelta * _dragChangeSpeed;
            PlayerPrefs.SetFloat(_prefKey, n);
            UpdateSettings();
        }
        else if (Mouse.current.rightButton.isPressed)
        {
            PlayerPrefs.SetFloat(_prefKey, _defaultValue);
            UpdateSettings();
        }

        _lastMouseX = Mouse.current.position.ReadValue().x;

        PlayerPrefs.SetFloat(_prefKey, Mathf.Clamp(PlayerPrefs.GetFloat(_prefKey), _clampMin, _clampMax));

        text.text = buttonTextSelected + $" [{Mathf.Round(PlayerPrefs.GetFloat(_prefKey) / _denominator * 100)}%]";

        /* Really I should make a MenuAudioBusFloatSlider child class that only 
        updates buses from there but whatever this works :3*/
        
    }

    new public void Select()
    {
        base.Select();
        text.text = buttonTextSelected + $" [{Mathf.Round(PlayerPrefs.GetFloat(_prefKey) / _denominator * 100)}%]";
    }
    new public void Deselect()
    {
        base.Deselect();
        text.text = buttonTextUnselected + $" [{Mathf.Round(PlayerPrefs.GetFloat(_prefKey) / _denominator * 100)}%]";
    }
}
