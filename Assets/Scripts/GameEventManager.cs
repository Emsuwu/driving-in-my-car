using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{
    [Header("Open door event")]
    [SerializeField] private GameObject _unopenHouse;
    [SerializeField] private GameObject _openHouse;

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
    }

    private void Start()
    {
        _loopCounter = GameObject.Find("LoopCounter");
    }

    public void IncrementFirstHalf()
    {
        if (HalfLoopsCompleted % 2 != 0)
        {
            HalfLoopsCompleted++;
            _loopCounter.GetComponent<TMPro.TMP_Text>().text = $"Half-loops: {HalfLoopsCompleted}";
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

}
