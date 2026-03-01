using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private bool DoFadeIn;
    [SerializeField] private GameObject _crossFadeGObj;
    private Image _crossfadeImage;

    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _fadeStartTime;

    private void Awake()
    {
        _crossFadeGObj.SetActive(true);
        _crossfadeImage = _crossFadeGObj.GetComponent<Image>();
        GetComponent<PauseController>().AllowPausing = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        if (DoFadeIn) StartCoroutine(FadeIn());
        else _crossFadeGObj.SetActive(false);
    }

    private IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(_fadeStartTime);
        _crossfadeImage.CrossFadeAlpha(0f, _fadeDuration, false);
    }
}
