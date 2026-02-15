using FMOD.Studio;
using FMODUnity;
using System.Text.RegularExpressions;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private EventReference _footstepsSound;
    private PLAYBACK_STATE _playbackState;
    public static PlayerFootsteps Instance;
    private EventInstance _footstepsSoundInstance;
    private float groundType = 1;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _footstepsSoundInstance = AudioManager.CreateInstance(_footstepsSound, transform);
    }
    public void Stop()
    {
        _footstepsSoundInstance.getPlaybackState(out _playbackState);
        if (_playbackState.Equals(PLAYBACK_STATE.STOPPING)
            || _playbackState.Equals(PLAYBACK_STATE.STOPPED)) return;
        AudioManager.FadeOutInstance(_footstepsSoundInstance);
    }
    public void Play()
    {
        AudioManager.AttachInstanceToGameObject(_footstepsSoundInstance, gameObject);
        _footstepsSoundInstance.getPlaybackState(out _playbackState);
        if (_playbackState.Equals(PLAYBACK_STATE.PLAYING)) return;
        AudioManager.StartInstance(_footstepsSoundInstance);
    }
    public void CheckAndSetFootstepType()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hit, 1f, _layerMask))
        {
            switch (hit.collider.tag)
            {
                case "Concrete":
                    groundType = 1f;
                    break;
                case "Grass":
                    groundType = 2f;
                    break;
                default:
                    groundType = 0f;
                    break;
            }
            _footstepsSoundInstance.setParameterByName("FootstepType", groundType);
        }
    }

}
