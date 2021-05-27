using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver;

    #region PUBLIC_REFERENCES
    //public GameObject brainCameraCinemachine;
    public MenuGameOver menuGameOver;
    #endregion

    #region UNITY_METHODS
    private void OnEnable()
    {
        EventManager.StartListening("GameOver", OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameOver", OnGameOver);
    }

    void Start()
    {
        isGameOver = false;
        EventManager.TriggerEvent("GameStarts", null);
    }
    #endregion

    #region PUBLIC_METHODS
    public void Continue()
    {
        Time.timeScale = 1;
        EventManager.TriggerEvent("RestoreValues", null);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneLoader.instance.LoadMenuScene();
    }

    public void NextStage()
    {
        ProgressManager.NextStage();
        EventManager.TriggerEvent("NextStage", null);
        Time.timeScale = 1;
        SceneLoader.instance.LoadMenuScene();
        
    }

    public void OnGameOver(object param)
    {
        if (isGameOver)
            return;
        else
            isGameOver = true;

        menuGameOver.ShowGameOver((bool)param);
        isGameOver = true;
        Time.timeScale = 0;
    }
    #endregion

}
