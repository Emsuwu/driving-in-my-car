using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent Calls;

    [SerializeField] private bool RequiresSpecificCount = true;
    [SerializeField, ShowIf("RequiresSpecificCount")] private int RequiredHalfLoopCount;

    public void OnTrigger()
    {
        if (RequiresSpecificCount)
        {
            if (GameEventManager.Instance.HalfLoopsCompleted == RequiredHalfLoopCount)
            {
                Calls?.Invoke();
            }
        }
        else
        {
            Calls?.Invoke();
        }
    }
}
