using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputActionAsset _inputActions;
    [HideInInspector] public static PlayerMovement Instance;
    [SerializeField] private Camera _camera;
    private Rigidbody _rb;
    private PlayerInteract _playerInteractScript;

    [Header("Migrate to Settings later")]
    [Range(0f, 5000f)] public float WalkSpeed = 10f;
    [Range(0.01f, .5f)] public float CameraSensitivity = 10f;
    private float _headBobCounter;
    public float RefreshRate = 60f;
    public bool InvertX;
    public bool InvertY;

    private InputAction _interactAction;
    private InputAction _moveAction;
    private Vector2 _moveAxis;
    private InputAction _lookAction;
    private Vector2 _lookAxis;

    private float _lookPlayerY;
    private float _lookCamX;

    void Awake()
    {
        _lookCamX = _camera.transform.rotation.x;
        _lookPlayerY = transform.rotation.y;
        if (Instance == null) Instance = this;
        else { Debug.LogWarning("Hey! There should only be one Player at one time!"); }
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _interactAction = InputSystem.actions.FindAction("Interact");
        _playerInteractScript = GetComponent<PlayerInteract>();
        _rb = GetComponent<Rigidbody>();
        

        // Get refresh rate
        RefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;
        if (RefreshRate > 0)
        {
            Time.fixedDeltaTime = 1f / RefreshRate;
        }

        gameObject.AddComponent<ConstantForce>().force = new Vector3(0.0f, -20f, 0.0f);
    }
    private void OnEnable()
    {
        _inputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        _inputActions.FindActionMap("Player").Disable();
    }
    void Update()
    {
        if (_interactAction.WasPressedThisFrame()) { _playerInteractScript.Interact(); }
    }
    private void LateUpdate()
    {
        Rotation();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        _moveAxis = _moveAction.ReadValue<Vector2>();
        float storeYVel = _rb.linearVelocity.y;
        Vector3 movement = (transform.forward * _moveAxis.y + transform.right * _moveAxis.x) * WalkSpeed * Time.fixedDeltaTime;
        movement.y = storeYVel;
        _rb.linearVelocity = movement;
        BobHead();
    }

    private void BobHead()
    {
        const float FREQUENCY = 5f;
        const float AMPLITUDE = 8f;
        Vector3 horizontalVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.x).normalized;
        float velocityMagnitude = horizontalVelocity.magnitude;
        float sinCounter = 0f;

        if (velocityMagnitude < .01f)
        {
            _headBobCounter = 0f;
        }
        else
        {
            _headBobCounter += Time.fixedDeltaTime * velocityMagnitude * FREQUENCY;
        }
        sinCounter = Mathf.Sin(_headBobCounter) * (1/AMPLITUDE);
        float newCamY = Mathf.Lerp(_camera.transform.localPosition.y, sinCounter, Time.deltaTime * FREQUENCY);
        _camera.transform.localPosition = new Vector3(0, newCamY, 0);
    }
    private void Rotation()
    {
        _lookAxis = _lookAction.ReadValue<Vector2>();
        Cursor.lockState = CursorLockMode.Locked;

        float playerYRot = _lookAxis.x * CameraSensitivity;
        float camXRot = -_lookAxis.y * CameraSensitivity;
        if (InvertX) playerYRot *= -1;
        if (InvertY) camXRot *= -1;

        _lookPlayerY += playerYRot;
        _lookCamX += camXRot;
        _lookCamX = Mathf.Clamp(_lookCamX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(0, _lookPlayerY, 0);
        _camera.transform.localRotation = Quaternion.Euler(_lookCamX, 0, 0);
    }
}
