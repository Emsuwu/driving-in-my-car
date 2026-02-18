using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LoadingZone>(out LoadingZone lz))
        {
            lz.OnTrigger();
        }
        else if (other.TryGetComponent<Trigger>(out Trigger tr))
        {
            tr.OnTrigger();
        }
    }
}
