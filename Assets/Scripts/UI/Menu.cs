using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static Menu Instance { get; private set; }
    [SerializeField] private Button _backButton;
    public Button BackButton
    {
        get { return _backButton; }
        private set { _backButton = value; }
    }

    private void OnEnable()
    {
        PauseController.Instance.CurrentMenu = this;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void TriggerBack()
    {
        if (_backButton != null)
        {
            _backButton.onClick.Invoke();
        }
    }
}
