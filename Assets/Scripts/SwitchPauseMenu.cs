using UnityEngine;

public class SwitchPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _activateMenu;
    [SerializeField] private GameObject _deactivateMenu;
    public GameObject ActivateMenu
    {
        get { return _activateMenu; }
        set { _activateMenu = value; }
    }
    public void OnTrigger()
    {
        _activateMenu.SetActive(true);
        _deactivateMenu.SetActive(false);
    }
}
