using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PoliceLight : MonoBehaviour
{
    [SerializeField] private GameObject _redLight;
    [SerializeField] private Image _crossfadeImage;
    [SerializeField] private EventReference _sirenSound;

    private void OnEnable()
    {
        AudioManager.PlayOneShot(_sirenSound, transform.position);
        StartCoroutine(EnableRed());
    }

    private IEnumerator EnableRed()
    {
        yield return new WaitForSeconds(0.1f);
        _redLight.SetActive(true);

        yield return new WaitForSeconds(19f);
        _crossfadeImage.CrossFadeAlpha(1f, 8f, false);

        yield return new WaitForSeconds(9f);
        EndingText.Show();

        yield return new WaitForSeconds(30f);
        SceneManager.LoadScene(0);
    }
}
