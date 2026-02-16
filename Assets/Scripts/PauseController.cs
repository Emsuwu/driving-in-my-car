using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private bool _isPaused = false;
    public static PauseController Instance;
    public bool IsPaused { get { return _isPaused; } }
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _backToGameButton;
    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _pauseAction;
    private void Awake()
    {
        Instance = this;
        _pauseAction = InputSystem.actions.FindAction("Pause");
    }
    private void OnEnable()
    {
        _inputActions.FindActionMap("UI").Enable();
    }
    private void OnDisable()
    {
        _inputActions.FindActionMap("UI").Disable();
    }
    void Update()
    {
        if (_pauseAction.WasPressedThisFrame()) { Pause(); }
    }

    private void Pause()
    {
        _isPaused = !_isPaused;
        _pauseMenu.SetActive(_isPaused);
        if (_isPaused) { 
            Cursor.lockState = CursorLockMode.None;
            _backToGameButton.Select();
        }
        else { Cursor.lockState = CursorLockMode.Locked; }
    }
}
