using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class MenuVolumeButton : MenuButton
{
    private FMOD.Studio.Bus _targetBus;
    [SerializeField] private string _targetBusName;
    private float _vol;

    new private void Awake()
    {
        base.Awake();
        _targetBus = RuntimeManager.GetBus(_targetBusName);
        _targetBus.getVolume(out float _vol);
    }
    new private void OnEnable()
    {
        base.OnEnable();
    }
    private void Update()
    {
        if (!isSelected) return;
        _targetBus.getVolume(out float _vol);

        if (PauseController.Instance.NavigateAxis.x > .1)
        {
            _vol += Time.deltaTime;
            //AudioManager.PlayOneShot(AudioEvents.Current.ButtonHoverSound, PlayerMovement.Instance.transform);
        }
        else if (PauseController.Instance.NavigateAxis.x < -.1)
        {
            _vol -= Time.deltaTime;
            //AudioManager.PlayOneShot(AudioEvents.Current.ButtonHoverSound, PlayerMovement.Instance.transform);
        }

        _vol = Mathf.Clamp( _vol, 0f, 2f );

        text.text = buttonTextSelected + $" [{Mathf.Round(_vol/1f*100)}%]";
        _targetBus.setVolume(_vol);
        
    }
    new public void Select()
    {
        base.Select();
        text.text = buttonTextSelected + $" [{Mathf.Round(_vol / 1f * 100)}%]";
    }
    new public void Deselect()
    {
        base.Deselect();
        text.text = buttonTextUnselected + $" [{Mathf.Round(_vol / 1f * 100)}%]";
    }
}
