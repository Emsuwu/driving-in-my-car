using UnityEngine;

public class GlobalSettingsUpdater : MonoBehaviour
{
    [SerializeField] private GameObject SmokeParticles;
    public void UpdateGlobalSettings()
    {
        GlobalSettings.Instance.SmokeEnabled = Meowtils.IntToBool( PlayerPrefs.GetInt("SmokeEnabled") );
        SmokeParticles.SetActive(GlobalSettings.Instance.SmokeEnabled);
        Debug.Log($"Updated smoke. Set to ");
    }
}
