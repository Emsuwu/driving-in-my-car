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
    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _pauseAction;
    public Vector2 NavigateAxis;
    private InputAction _navigate;
    private void Awake()
    {
        Instance = this;
        _pauseAction = InputSystem.actions.FindAction("Pause");
        _navigate = InputSystem.actions.FindAction("Navigate");
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
        if (_pauseAction.WasPressedThisFrame()) { TogglePause(); }
        NavigateAxis = _navigate.ReadValue<Vector2>();
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;
        _pauseMenu.SetActive(_isPaused);
        if (_isPaused) { 
            Cursor.lockState = CursorLockMode.None;
        }
        else { Cursor.lockState = CursorLockMode.Locked; }
    }
    public void Unpause()
    {
        _isPaused = false;
        _pauseMenu.SetActive(_isPaused);
        Cursor.lockState = CursorLockMode.Locked;
        
    }
}
