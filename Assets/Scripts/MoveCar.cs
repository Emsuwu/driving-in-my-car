using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.Splines;

public class MoveCar : MonoBehaviour
{
    [SerializeField] private SplineAnimate _splineAnimate;
    private float _distanceToPlayer;
    public float FollowAtDistance = 80f;

    [SerializeField] private EventReference _carEngineSound;
    private EventInstance _carEngineSoundInstance;

    private void Start()
    {
        GetComponent<SplineAnimate>();

        _carEngineSoundInstance = AudioManager.CreateAndStartInstance(_carEngineSound, transform);
        AudioManager.AttachInstanceToGameObject(_carEngineSoundInstance, gameObject);
    }

    private void Update()
    {

        _distanceToPlayer = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position);

        if (_distanceToPlayer > FollowAtDistance)
        {
            _splineAnimate.Play();
        }
        else
        {
            _splineAnimate.Pause();
        }
    }
}
