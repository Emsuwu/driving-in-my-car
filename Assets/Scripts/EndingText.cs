using UnityEngine;

public class EndingText : MonoBehaviour
{
    public static EndingText Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static void Show()
    {
        EndingText.Instance.GetComponent<Animator>().SetTrigger("EndingText");
    }
}
