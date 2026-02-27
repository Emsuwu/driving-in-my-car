using NaughtyAttributes;
using UnityEngine;

public class PlayerPrefsTool : MonoBehaviour
{
    [Button("Reset HasAcceptedWarning")] private void ResetHasAcceptedWarning()
    {
        PlayerPrefs.DeleteKey("HasAcceptedWarning");
    }
}
