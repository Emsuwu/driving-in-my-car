using UnityEngine;

public class NewGameMenuButton : ContinueButton
{
    [SerializeField] private GameObject _confirmationMenu;
    public new void OnTrigger()
    {
        base.OnTrigger();

        GameEventManager.Instance.ResetSave();
        _confirmationMenu.SetActive(false);
    }
}
