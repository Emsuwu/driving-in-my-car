using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    [Header("Open door event")]
    [SerializeField] private GameObject _unopenHouse;
    [SerializeField] private GameObject _openHouse;

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
