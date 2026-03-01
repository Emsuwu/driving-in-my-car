using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _kidGObj;
    [SerializeField] private GameObject _kidOrigin;
    [SerializeField] private Animator _kidAnimator;
    [SerializeField] private EventReference _runSound;
    private EventInstance _runSoundInstance;
    public EventInstance RunSoundInstance { get { return _runSoundInstance; } }

    private Dictionary<string, Vector3> _animOffsets = new();

    [SerializeField] private string _startingAnim;

    private void Awake()
    {
        _animOffsets.Add("T-Pose", Vector3.zero);
        _animOffsets.Add("Breakdance", new Vector3(0f, -0.77f, 0f));
        _animOffsets.Add("StandardRun", new Vector3(0f, -0.77f, 0f));
        _animOffsets.Add("Peak", new Vector3(0f, -0.77f, 0f));
    }

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(_startingAnim)) PlayAnim(_startingAnim);
        
    }

    public void PlayAnim(string animationName)
    {
        if (_animOffsets.ContainsKey(animationName)) _kidOrigin.transform.localPosition = _animOffsets[animationName];
        else _kidOrigin.transform.localPosition = Vector3.zero;
        _kidAnimator.Play(animationName);
    }

    public void TPose() { PlayAnim("T-Pose"); }
    public void Breakdance() { PlayAnim("Breakdance"); }
    public void StandardRun() 
    { 
        PlayAnim("StandardRun");
        _runSoundInstance = AudioManager.CreateAndStartInstance(_runSound, transform);
        AudioManager.AttachInstanceToGameObject(_runSoundInstance, gameObject);
    }
}
