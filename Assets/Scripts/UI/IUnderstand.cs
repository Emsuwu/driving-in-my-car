using UnityEngine;
using UnityEngine.SceneManagement;

public class IUnderstand : MonoBehaviour
{
    public void OnTrigger()
    {
        PlayerPrefs.SetInt("HasAcceptedWarning", 1);
        SceneManager.LoadScene(0);
    }
}
