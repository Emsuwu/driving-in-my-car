using System.Collections;
using UnityEngine;

public class KidRunOnEnable : MonoBehaviour
{
    [SerializeField] private Vector3 _runVec;
    [SerializeField] private GameObject _kidJumpscare;
    public float DistanceToPlayer
    {
        get
        {
            return Vector3.Distance(PlayerMovement.Instance.transform.position, transform.position);
        }
    }

    private void OnEnable()
    {
        KidAnimation anim = GetComponent<KidAnimation>();
        anim.StandardRun();

        StartCoroutine(DestroyAfter30Secs());
    }

    private void Update()
    {
        if (enabled) 
        {
            transform.position = transform.position + _runVec * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, PlayerMovement.Instance.transform.position.z);
            if (DistanceToPlayer < 3f)
            {
                _kidJumpscare.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DestroyAfter30Secs()
    {
        yield return new WaitForSeconds(30);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        AudioManager.StopInstance(GetComponent<KidAnimation>().RunSoundInstance);
    }
}
