using FMODUnity;
using UnityEngine;

public class TVSoundEvent : MonoBehaviour
{
    [SerializeField] private bool _active;
    [SerializeField] private EventReference _tvSound;
    
    private void OnEnable()
    {
        if (!_active) return;
        AudioManager.CreateAndStartInstance(_tvSound, transform);
    }

}
