using UnityEngine;

public class PlayerLoadingZoneHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LoadingZone>(out LoadingZone lz))
        {
            
        }
    }
}
