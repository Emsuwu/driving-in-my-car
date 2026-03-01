using System.Collections;
using UnityEngine;

public class HideKidRoundCorner : MonoBehaviour
{
    public void HideRoundCorner()
    {
        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        for (int i = 0; i < 100; i++) {
            yield return new WaitForEndOfFrame();
            transform.position = transform.position + new Vector3(0, 0, -Time.deltaTime*8.5f);
        }
        Destroy(gameObject);
    }
}
