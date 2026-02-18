using System.Collections;
using UnityEngine;

public class Crow : MonoBehaviour
{
    public Vector3 Travel = new Vector3 (0f, 500f, -100f);
    [SerializeField] private Animator _animator;

    public void Caw()
    {
        AudioManager.PlayOneShotAttached(AudioEvents.Current.CrowCawSound, gameObject);
    }
    public void Spook()
    {
        _animator.SetTrigger("Fly");
        StartCoroutine(FlyOff());
    }
    private IEnumerator FlyOff()
    {
        yield return new WaitForSeconds(Random.Range(0f,.5f));
        for (int i = 0; i < 500; i++)
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            transform.position = new Vector3(
                x + Travel.x * Time.deltaTime, 
                y + Travel.y * Time.deltaTime, 
                z + Travel.z * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
