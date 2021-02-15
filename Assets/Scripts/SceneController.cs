using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    int levelsCount;
    int currentlevel;

    private static SceneController instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        levelsCount = SceneManager.sceneCountInBuildSettings;
        currentlevel = 0;
    }

    public void LoadNextLevel()
    {
        if (currentlevel < levelsCount - 1)
        {
            currentlevel += 1;
            SceneManager.LoadScene(currentlevel);
        }
        else
        {
            currentlevel = 0;
            SceneManager.LoadScene(currentlevel);
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene(currentlevel);
    }
}
