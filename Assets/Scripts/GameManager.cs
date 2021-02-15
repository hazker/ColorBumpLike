using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public SceneController sceneController;

    Vector3 startPos;
    Vector3 finish;

    [HideInInspector]
    public bool gameIsActive=false;

    float currentTime;
    float levelComplition;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                
            }
            return instance;
        }
    }

    private void GetPositions()
    {
        finish = FindObjectOfType<Finish>().transform.position;
        startPos = player.transform.position;
    }

    private void LateUpdate()
    {
        if (gameIsActive)
        {
            currentTime += Time.deltaTime;
            levelComplition =100-Mathf.FloorToInt(100 * Vector3.Distance(player.transform.position,finish)/Vector3.Distance(startPos, finish));
            LevelComplition();
        }
            
    }

    public void GameStarted()
    {
        GetPositions();
        gameIsActive = true;
        player.GetComponent<PlayerController>().Run();
    }

    public void GameEnded()
    {
        gameIsActive = false;
        UIMananger.Instance.gameEnd();
    }

    public void LevelComplition()
    {
        UIMananger.Instance.InGameUIChange(levelComplition,currentTime);
    }

    public void Win()
    {
        gameIsActive = false;
        UIMananger.Instance.Win();
    }
    
}
