using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    private float delay = 5.0f;

    void Start()
    {
        StartCoroutine(DestroyAfterDelayCoroutine(delay));

    }

    IEnumerator DestroyAfterDelayCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
