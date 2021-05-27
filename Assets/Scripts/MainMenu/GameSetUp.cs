using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    public static GameSetUp instance;

    #region PUBLIC_REFERENCES
    public List<int> selectedAlliesForScene;
    public CharacterStats[] totalAllies;
    public StageWave nextStage;
    #endregion

    #region UNITY_METHODS
    private void OnEnable()
    {
        EventManager.StartListening("NextStage", OnStageUpdate);
    }

    private void OnDisable()
    {
        EventManager.StopListening("NextStage", OnStageUpdate);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        selectedAlliesForScene = new List<int>();

        LoadGameProgress();

        this.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region PUBLIC_METHODS
    public void LoadGameProgress()
    {
        ProgressManager.LoadEnemyWaves();
        ProgressManager.LoadPlayerProgress();
        UnlockCharactersAtActualStage();
        EventManager.TriggerEvent("FinishedLoadingProgress",null);
    }
    #endregion

    #region PRIVATE_METHODS
    public void UnlockCharactersAtActualStage()
    {
        for (int i = 0; i < totalAllies.Length; i++)
        {
            if (ProgressManager.GetCurrentStage() >= totalAllies[i].unlockAtStage)
                ProgressManager.UnlockCharacter(i);
        }
    }
    #endregion

    #region LISTENER_METHODS
    private void OnStageUpdate(object param)
    {
        UnlockCharactersAtActualStage();
    }
    #endregion

    #region HELPERS
    [ContextMenu("Crete generic JSON stage file")]
    public void CreateGenericJSONStages()
    {
        FullStages fs = new FullStages();
        fs.stages = new StageWave[1];
        fs.stages[0] = new StageWave();
        fs.stages[0].enemyWaves = new Wave[1];

        string pathToSave = Path.Combine(Application.persistentDataPath, "Stages");

        File.WriteAllText(pathToSave, JsonUtility.ToJson(fs,true));

        Debug.Log("Saved at " + pathToSave);
    }

    [ContextMenu("Crete generic JSON player progress file")]
    public void CreateGenericJSONPlayerProgress()
    {
        PlayerProgress pp = new PlayerProgress();
        pp.unlockedCharacters = new AlliesUnlocked[1];
        pp.unlockedCharacters[0] = new AlliesUnlocked();

        string pathToSave = Path.Combine(Application.persistentDataPath, "PlayerProgress");

        File.WriteAllText(pathToSave, JsonUtility.ToJson(pp, true));

        Debug.Log("Saved at " + pathToSave);
    }
    #endregion
}
