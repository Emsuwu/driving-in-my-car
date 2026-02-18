using UnityEngine;

public class LoadingZone : MonoBehaviour
{
    [Header("LoadingZone targets")]
    public GameObject[] deactivateTargets = null;
    public GameObject[] activateTargets = null;

    public void OnTrigger()
    {
        Activate();
        Deactivate();
    }

    private void Deactivate()
    {
        if (deactivateTargets.Length == 0) return;
        foreach (GameObject target in deactivateTargets)
        {
            target.SetActive(false);
        }
    }
    private void Activate()
    {
        if (activateTargets.Length == 0) return;
        foreach (GameObject target in activateTargets)
        {
            target.SetActive(true);
        }
    }
}
