using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public bool AllowPausing = true;

    private bool _isPaused = false;
    public static PauseController Instance;
    public bool IsPaused { get { return _isPaused; } }
    [SerializeField] [FormerlySerializedAs("_pauseMenu")] private GameObject _menu;
    [SerializeField] private InputActionAsset _inputActions;
    [SerializeField] private GameObject _debugPrint;
    [SerializeField] public Menu CurrentMenu;
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
        CutsceneController cc;
        TryGetComponent<CutsceneController>(out cc);
        if (cc != null) return;
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

    IEnumerator SelectInitial()
    {
        yield return new WaitForEndOfFrame();
        {
            CurrentMenu.InitialSelection.Select();
            
        }
    }

    void Update()
    {
        //Debug.Log($"T: {IsOnTitleScreen}, ||: {IsPaused}");

        if (_pauseAction.WasPressedThisFrame()) { Esc(); }
        NavigateAxis = _navigate.ReadValue<Vector2>();

        if (_navigate.WasPressedThisFrame())
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                StartCoroutine(SelectInitial());
            }

        }
        
        if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            PrintDebugInfo();
        }
        if (Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            _debugPrint.SetActive(false);
        }

    }

    public void PrintDebugInfo()
    {
        _debugPrint.SetActive(true);
        TMPro.TMP_Text t = _debugPrint.GetComponent<TMPro.TMP_Text>();

        t.text = "Debug info\r\nPress [9] to close. Press [0] to refresh.\r\n_______________________________\r\n";

        t.text += ($"\r\nIsOnTitleScreen: {IsOnTitleScreen}");
    }

    private void Esc()
    {
        if (!_isPaused) {
            if (!AllowPausing) { return; }
            _isPaused = true;
            _menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else{
            CurrentMenu.TriggerBack();
        }
    }
    public void Unpause()
    {
        _isPaused = false;
        _menu.SetActive(_isPaused);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pause()
    {
        _isPaused = true;
        _menu.SetActive(_isPaused);
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
