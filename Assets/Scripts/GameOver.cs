using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        if (player == null)
        {
            StartCoroutine(WaitCoroutine());
        }
    }
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Start");
    }
}
