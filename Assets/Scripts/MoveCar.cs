using UnityEngine;
using UnityEngine.Splines;

public class MoveCar : MonoBehaviour
{
    private SplineAnimate _splineAnimate;
    private float _distanceToPlayer;

    private void Start()
    {
        GetComponent<SplineAnimate>();
    }

    private void Update()
    {
        if (_splineAnimate == null) return;
        _distanceToPlayer = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position);
        if (_distanceToPlayer < 20)
        {
            _splineAnimate.Play();
        }
        else
        {
            _splineAnimate.Pause();
        }
    }
}
