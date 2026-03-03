using UnityEngine;
using UnityEngine.UI;

public class ViewRenderTexture : MonoBehaviour
{
    public static ViewRenderTexture Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void FadeOut(float duration = 2.5f)
    {
        GetComponent<RawImage>().CrossFadeAlpha(0f, duration, false);
    }
    public void FadeIn(float duration = 2.5f)
    {
        GetComponent<RawImage>().CrossFadeAlpha(1f, duration, false);
    }
}
