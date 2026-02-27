using NaughtyAttributes;
using UnityEngine;

public class PlayerPrefsTool : MonoBehaviour
{
    [Button("Reset HasAcceptedWarning")] private void ResetHasAcceptedWarning()
    {
        PlayerPrefs.DeleteKey("HasAcceptedWarning");
    }

    [Button("Reset HalfLoopsCompleted")]
    private void ResetHalfLoopsCompleted()
    {
        PlayerPrefs.DeleteKey("HalfLoopsCompleted");
    }
}
