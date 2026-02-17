using FMODUnity;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private static List<EventInstance> _currentEventInstances = new List<EventInstance>();

    public static AudioManager Current;
    private void Awake()
    {
        if (Current == null) Current = this;
        else Debug.LogWarning("There must only be 1 AudioManager in any given scene.");
    }

    // Playing OneShots
    public static void PlayOneShot(EventReference eventReference, Vector3 position)
    {
        RuntimeManager.PlayOneShot(eventReference, position);
    }
    public static void PlayOneShot(EventReference eventReference, Transform transform)
    {
        RuntimeManager.PlayOneShot(eventReference, transform.position);  
    }
    public static void PlayOneShotAttached(EventReference eventReference, GameObject gameObject)
    {
        RuntimeManager.PlayOneShotAttached(eventReference, gameObject);
    }

    // Creating EventInstances
    public static EventInstance CreateInstance(EventReference eventReference, Transform transform)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        _currentEventInstances.Add(eventInstance);
        FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
        eventInstance.set3DAttributes(attributes);
        return eventInstance;
    }
    public static EventInstance CreateInstance(EventReference eventReference, Vector3 position)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        _currentEventInstances.Add(eventInstance);
        FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(position);
        eventInstance.set3DAttributes(attributes);
        return eventInstance;
    }
    public static EventInstance CreateAndStartInstance(EventReference eventReference, Transform transform)
    {
        EventInstance eventInstance = CreateInstance(eventReference, transform);
        eventInstance.start();
        return eventInstance;
    }
    public static EventInstance CreateAndStartInstance(EventReference eventReference, Vector3 position)
    {
        EventInstance eventInstance = CreateInstance(eventReference, position);
        eventInstance.start();
        return eventInstance;
    }

    // Stopping EventInstances
    public static void StopInstance(EventInstance eventInstance, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.IMMEDIATE)
    {
        PLAYBACK_STATE playbackState;
        eventInstance.getPlaybackState(out playbackState);
        if (playbackState.Equals(PLAYBACK_STATE.STOPPED) || playbackState.Equals(PLAYBACK_STATE.STOPPING)) return;
        eventInstance.stop(stopMode);
        _currentEventInstances.Remove(eventInstance);
    }
    public static void FadeOutInstance(EventInstance eventInstance)
    {
        StopInstance(eventInstance, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    
    // Starting EventInstances
    public static void StartInstance(EventInstance eventInstance)
    {
        eventInstance.start();
    }

    // Setting EventInstances position
    public static void SetInstancePosition(EventInstance eventInstance, Transform transform)
    {
        FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(transform);
        eventInstance.set3DAttributes(attributes);
    }
    public static void SetInstancePosition(EventInstance eventInstance, Vector3 position)
    {
        FMOD.ATTRIBUTES_3D attributes = FMODUnity.RuntimeUtils.To3DAttributes(position);
        eventInstance.set3DAttributes(attributes);
    }

    // Attaching instances
    public static void AttachInstanceToGameObject(EventInstance eventInstance, GameObject gameObject)
    {
        if (!eventInstance.isValid())
        {
            Debug.LogWarning("AudioManager: Invalid EventInstance");
            return;
        }
        eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObject);
    }

    // Clean up
    public static void StopAllInstances()
    {
        foreach (EventInstance eventInstance in _currentEventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        _currentEventInstances.Clear();
    }
    public void OnDestroy()
    {
        StopAllInstances();
    }
}
