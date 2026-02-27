using UnityEngine;

public class SaveNotification : MonoBehaviour
{
    private Animator _animator;
    public static SaveNotification Instance;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Instance = this;
    }

    public void Show()
    {
        _animator.SetTrigger("Show");
    }
}
