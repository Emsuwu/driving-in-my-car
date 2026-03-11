using UnityEngine;

public class NewGameMenuButton : ContinueButton
{
    [SerializeField] private GameObject _confirmationMenu;
    public new void OnTrigger()
    {
        _confirmationMenu.SetActive(false);
        base.OnTrigger();
        GameEventManager.Instance.ResetSave();
    }
}
