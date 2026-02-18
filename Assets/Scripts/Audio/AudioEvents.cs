using UnityEngine;
using FMODUnity;
using System.Collections.Generic;
using FMOD.Studio;

public class AudioEvents : MonoBehaviour
{
    public static AudioEvents Current;
    private void Awake() { Current = this; }

    public EventReference CrowCawSound;
    public EventReference AmbienceSound;
    private EventInstance _ambienceInstance;
    public EventReference ButtonHoverSound;

    private void Start()
    {
        //AudioManager.PlayOneShot(AmbienceSound, PlayerMovement.Instance.transform);
        _ambienceInstance = AudioManager.CreateAndStartInstance(AmbienceSound, PlayerMovement.Instance.transform);
        AudioManager.AttachInstanceToGameObject(_ambienceInstance, PlayerMovement.Instance.gameObject);
    }
}
