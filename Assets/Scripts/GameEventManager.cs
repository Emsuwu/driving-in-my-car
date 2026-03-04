using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{
    [Header("Open door event")]
    [SerializeField] private GameObject _unopenHouse;
    [SerializeField] private GameObject _openHouse;

    [SerializeField] private GameObject _holeQuadrant;
    [SerializeField] private GameObject _quadrant4;
    [SerializeField] private GameObject _alwaysGreener1;
    [SerializeField] private GameObject _streetlamp24;

    private bool _hideQuadrant4Forevermore;

    private GameObject _loopCounter;

    public static GameEventManager Instance;

    public int HalfLoopsCompleted = 0;

    private void Awake()
    {
        Instance = this;

        if (!PlayerPrefs.HasKey("HasAcceptedWarning") || PlayerPrefs.GetInt("HasAcceptedWarning") == 0) 
        {
            SceneManager.LoadScene(1);
        }

        if (PlayerPrefs.HasKey("HalfLoopsCompleted"))
        {
            HalfLoopsCompleted = PlayerPrefs.GetInt("HalfLoopsCompleted");
        }
        else
        {
            PlayerPrefs.SetInt("HalfLoopsCompleted", 0);
        }
        
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt("HalfLoopsCompleted", 0);
        HalfLoopsCompleted = 0;
        _loopCounter.GetComponent<TMPro.TMP_Text>().text = $"Half-loops: {HalfLoopsCompleted}";
    }

    private void Start()
    {
        _loopCounter = GameObject.Find("LoopCounter");
        if (_loopCounter != null) _loopCounter.GetComponent<TMPro.TMP_Text>().text = $"Half-loops: {HalfLoopsCompleted}";
    }

    public void IncrementFirstHalf()
    {
        if (HalfLoopsCompleted % 2 != 0)
        {
            HalfLoopsCompleted++;
            _loopCounter.GetComponent<TMPro.TMP_Text>().text = $"Half-loops: {HalfLoopsCompleted}";
            PlayerPrefs.SetInt("HalfLoopsCompleted", HalfLoopsCompleted);
            SaveNotification.Instance.Show();
        }
        else
        {
            Debug.Log("Can't increment until other side");
        }
    }
    public void IncrementSecondHalf()
    {
        if (HalfLoopsCompleted % 2 == 0)
        {
            HalfLoopsCompleted++;
            _loopCounter.GetComponent<TMPro.TMP_Text>().text = $"Half-loops: {HalfLoopsCompleted}";
        }
        else
        {
            Debug.Log("Can't increment until other side");
        }
    }

    public void Event1OpenDoor(bool open = true)
    {
        if (!open)
        {
            _openHouse.SetActive(false);
            _unopenHouse.SetActive(true);
            return;
        }
        _openHouse.SetActive(true);
        _unopenHouse.SetActive(false);
    }

    public void HoleEvent(bool on = true)
    {
        _quadrant4.SetActive(!on);
        _streetlamp24.SetActive(!on);
        _alwaysGreener1.SetActive(!on);
        _holeQuadrant.SetActive(on);
        _hideQuadrant4Forevermore = on;
    }

    private void Update()
    {
        if (_hideQuadrant4Forevermore) _quadrant4.SetActive(false);
    }


    public void EnablePlayerDamping(bool on = true)
    {
        Rigidbody _rb = PlayerMovement.Instance.GetComponent<Rigidbody>();
        if (on)
        {
            _rb.linearDamping = 10f;
            _rb.angularDamping = 0.05f;
        }
        else
        {
            _rb.linearDamping = 0f;
            _rb.angularDamping = 0f;
        }
    }

    public void TransitionToEnding()
    {
        StartCoroutine(TransitionToEndingRoutine());
    }

    IEnumerator TransitionToEndingRoutine()
    {
        float DURATION = 2.5f;
        ViewRenderTexture.Instance.FadeOut(DURATION);
        AudioEvents.Current.FadeAmbience();
        yield return new WaitForSeconds(DURATION + .5f);
        SceneManager.LoadScene(2);
    }
}
