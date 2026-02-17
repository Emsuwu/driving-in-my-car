using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public void OnTrigger()
    {
        PauseController.Instance.Unpause();
        PauseController.Instance.SwitchMenuActivationTarget(PauseController.Instance.BackButtons, PauseController.Instance.PauseMenuPause);
    }
}
