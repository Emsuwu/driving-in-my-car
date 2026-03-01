using System.Collections;
using UnityEngine;

public class KidJumpscare : MonoBehaviour
{
    private void OnEnable()
    {
        AudioEvents.Current.StopAmbience();
        StartCoroutine(HideAfter5Secs());
        PlayerMovement.Instance.Disabled = true;
        PauseController.Instance.AllowPausing = false;
        AudioManager.PlayOneShot(AudioEvents.Current.KidScare, PlayerMovement.Instance.transform);
    }

    private IEnumerator HideAfter5Secs()
    {   
        yield return new WaitForSeconds(5);
        AudioEvents.Current.StartAmbience();
        gameObject.SetActive(false);
        PlayerMovement.Instance.Disabled = false;
        PauseController.Instance.AllowPausing = true;
        PauseController.Instance.Unpause();
    }
}
