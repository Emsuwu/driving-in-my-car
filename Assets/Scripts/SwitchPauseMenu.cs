using UnityEngine;

public class SwitchPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _activateMenu;
    [SerializeField] private GameObject _deactivateMenu;

    public void OnTrigger()
    {
        _activateMenu.SetActive(true);
        _deactivateMenu.SetActive(false);
    }
}
