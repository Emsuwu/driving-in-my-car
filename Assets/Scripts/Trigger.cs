using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent Calls;

    public void OnTrigger()
    {
        Calls?.Invoke();
    }
}
