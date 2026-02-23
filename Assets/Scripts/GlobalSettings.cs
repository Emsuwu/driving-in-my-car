using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings Instance;

    public bool SmokeEnabled;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SmokeEnabled = GetOrCreateBoolPref("SmokeEnabled", true);
    }

    public bool GetOrCreateBoolPref(string key, bool defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return Meowtils.IntToBool( PlayerPrefs.GetInt(key) );
        }
        else
        {
            PlayerPrefs.SetInt(key, Meowtils.BoolToInt( defaultValue ));
            return Meowtils.IntToBool(PlayerPrefs.GetInt(key));
        }
    }

    public void UpdateSettings()
    {
        GetComponent<GlobalSettingsUpdater>().UpdateGlobalSettings();
        Debug.Log("Updated 2");
    }
}
