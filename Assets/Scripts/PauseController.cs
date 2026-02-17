using NUnit.Framework;
using System.Collections;
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

    [SerializeField] private SwitchPauseMenu[] _backButtons;
    public SwitchPauseMenu[] BackButtons { get { return _backButtons; } }
    public GameObject PauseMenuPause;
    public GameObject PauseMenuTitle;

    public static bool IsOnTitleScreen = true;
    
    private void Awake()
    {
        Instance = this;
        _pauseAction = InputSystem.actions.FindAction("Pause");
        _navigate = InputSystem.actions.FindAction("Navigate");
    }
    private void Start()
    {
        StartCoroutine(WaitFrameThenPause());
    }

    IEnumerator WaitFrameThenPause()
    {
        yield return new WaitForEndOfFrame();
        Pause();
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
    public void Pause()
    {
        _isPaused = true;
        _pauseMenu.SetActive(_isPaused);
        Cursor.lockState = CursorLockMode.None;
    }

    public void SwitchMenuActivationTarget(SwitchPauseMenu[] buttons, GameObject targetMenu)
    {
        foreach (SwitchPauseMenu button in buttons) 
        {
            button.ActivateMenu = targetMenu;
        }
    }
    public void SwitchMenuActivationTarget(SwitchPauseMenu button, GameObject targetMenu)
    {
        button.ActivateMenu = targetMenu;
    }
}
