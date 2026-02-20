using UnityEngine;
using UnityEngine.Splines;

public class MoveCar : MonoBehaviour
{
    [SerializeField] private SplineAnimate _splineAnimate;
    private float _distanceToPlayer;
    public float FollowAtDistance = 80f;

    private void Start()
    {
        GetComponent<SplineAnimate>();
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
