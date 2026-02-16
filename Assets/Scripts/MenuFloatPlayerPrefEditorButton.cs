using FMODUnity;
using UnityEngine;

public class MenuFloatPlayerPrefEditorButton : MenuButton
{
    [SerializeField] float _clampMax;
    [SerializeField] float _clampMin;
    [SerializeField] string _prefKey;
    [SerializeField] float _defaultValue;
    [SerializeField] float _changeSpeed = 1f;
    [SerializeField] private float _denominator = 1f;

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
            PlayerPrefs.SetFloat(_prefKey, PlayerPrefs.GetFloat(_prefKey) - Time.deltaTime * _changeSpeed);
            UpdateSettings();
        }

        PlayerPrefs.SetFloat(_prefKey, Mathf.Clamp(PlayerPrefs.GetFloat(_prefKey), _clampMin, _clampMax) );

        text.text = buttonTextSelected + $" [{Mathf.Round(PlayerPrefs.GetFloat(_prefKey) / _denominator * 100)}%]";

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
