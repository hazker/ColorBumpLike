using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMananger : MonoBehaviour
{
    private static UIMananger instance;
    public static UIMananger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIMananger>();
            }
            return instance;
        }
    }

    public GameObject startPanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject gamePanel;
    [Header("InGameUI")]
    public Text levelComplition;
    public Text currentTime;
    [Header("WinPanel")]
    public Text winText;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void gameStart()
    {
        GameManager.Instance.GameStarted();
    }

    public void gameEnd()
    {
        gamePanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void Win()
    {
        gamePanel.SetActive(false);
        winPanel.SetActive(true);
    }

    public void InGameUIChange(float level,float time)
    {
        levelComplition.text = "Level completed: "+ level.ToString()+"%";
        currentTime.text ="Time Spend: "+ time.ToString();
    }

}
