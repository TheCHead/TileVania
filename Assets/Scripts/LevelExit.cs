using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    //on collision start coroutine
    // slowdown time
    //wait for 2-3 sec
    //load next scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());

        IEnumerator LoadNextLevel()
        {
            Time.timeScale = 0.5f;
            yield return new WaitForSeconds(1f);
            Time.timeScale = 1f;
            Destroy(FindObjectOfType<ScenePersist>().gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
