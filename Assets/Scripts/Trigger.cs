using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent Calls;

    [Multiline] public string DescriptiveText;

    enum OPERATOR { GREATER_THAN, LESS_THAN, EQUAL_TO, NOT_EQUAL_TO };

    [Header("Condition")]
    [SerializeField] private bool RequiresSpecificCount = true;
    [SerializeField, ShowIf("RequiresSpecificCount")] private OPERATOR _operator = OPERATOR.EQUAL_TO;
    [SerializeField, ShowIf("RequiresSpecificCount")] private int RequiredHalfLoopCount;

    public void OnTrigger()
    {
        if (RequiresSpecificCount)
        {
            Operate();
        }
        else
        {
            Calls?.Invoke();
        }
    }

    private void Operate()
    {
        if (_operator == OPERATOR.EQUAL_TO)
        {
            if (GameEventManager.Instance.HalfLoopsCompleted == RequiredHalfLoopCount)
            {
                Calls?.Invoke();
            }
        }

        if (_operator == OPERATOR.GREATER_THAN)
        {
            if (GameEventManager.Instance.HalfLoopsCompleted > RequiredHalfLoopCount)
            {
                Calls?.Invoke();
            }
        }

        if (_operator == OPERATOR.LESS_THAN)
        {
            if (GameEventManager.Instance.HalfLoopsCompleted < RequiredHalfLoopCount)
            {
                Calls?.Invoke();
            }
        }

        if (_operator == OPERATOR.NOT_EQUAL_TO)
        {
            if (GameEventManager.Instance.HalfLoopsCompleted != RequiredHalfLoopCount)
            {
                Calls?.Invoke();
            }
        }
    }
    
}
