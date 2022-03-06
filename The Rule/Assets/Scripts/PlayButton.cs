using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    bool started = false;

    void Start()
    {
        
    }

    public void Update()
    {
        if (started)
        {
            Color col = GetComponentInChildren<SpriteRenderer>().color;
            GetComponentInChildren<SpriteRenderer>().color = new Color(col.r, col.g, col.b, col.a + Time.deltaTime * 0.5f);
        }
    }

    public void Play()
    {
        Debug.Log("PLAY");
        GetComponent<Button>().interactable = false;
        started = true;
        Invoke("StartGame", 2.1f);
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene("MovementTesting"));
    }

    IEnumerator LoadScene(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
