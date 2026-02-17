using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public void OnTrigger()
    {
        PauseController.IsOnTitleScreen = false;
        PauseController.Instance.PauseMenuPause.SetActive(true);
        PauseController.Instance.PauseMenuTitle.SetActive(false);
        PauseController.Instance.Unpause();
        PauseController.Instance.SwitchMenuActivationTarget(PauseController.Instance.BackButtons, PauseController.Instance.PauseMenuPause);
    }
}
