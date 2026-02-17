using UnityEngine;

public class ExitToTitleButton : MonoBehaviour
{
    public void OnTrigger()
    {
        PauseController.IsOnTitleScreen = true;
        PauseController.Instance.PauseMenuPause.SetActive(false);
        PauseController.Instance.PauseMenuTitle.SetActive(true);
        PauseController.Instance.SwitchMenuActivationTarget(PauseController.Instance.BackButtons, PauseController.Instance.PauseMenuTitle);
    }
}
