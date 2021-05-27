using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnController : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public int sampleWaveToTest = 0; 
    public int totalSpawnedEnemiesInStage;
    public int currentAliveEnemies;
    public Image imagewaveProgress;
    public Transform spawnPoint;
    public GameObject prefabEnemy;
    #endregion

    #region PUBLIC_REFERENCES
    public StageWave stageWave;
    public CharacterStats[] fullEnemiesList;
    #endregion

    #region UNITY_METHODS
    private void OnEnable()
    {
        EventManager.StartListening("GameStarts", StartedGame);
        EventManager.StartListening("EnemyKilled", OnEnemyKilled);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameStarts", StartedGame);
        EventManager.StopListening("EnemyKilled", OnEnemyKilled);
    }

    private void Start()
    {
        StartEnemyList();
    }
    #endregion

    #region PUBLIC_METHODS
    public void CreateEnemyWave()
    {
        for (int i = 0; i < stageWave.enemyWaves.Length; i++)
            StartCoroutine(RoutineCreateEnemyWave(stageWave.enemyWaves[i]));
    }
    #endregion

    #region LISTENER_METHODS
    public void StartedGame(object param)
    {
        CreateEnemyWave();
    }

    public void OnEnemyKilled(object param)
    {
        currentAliveEnemies--;
        imagewaveProgress.fillAmount = 1f - ((float)(totalSpawnedEnemiesInStage) / (float)stageWave.enemiesInThisStage);

        if (totalSpawnedEnemiesInStage >= stageWave.enemiesInThisStage && currentAliveEnemies <= 0)
            EventManager.TriggerEvent("GameOver", true);
        else if (currentAliveEnemies < stageWave.spawnThreshold)
            CreateEnemyWave();

    }
    #endregion

    #region PRIVATE_METHODS
    private void StartEnemyList()
    {
        if (ProgressManager.IsReady)
            stageWave = ProgressManager.LoadStage(ProgressManager.GetCurrentStage());
        else
        {
            ProgressManager.LoadEnemyWaves();
            stageWave = ProgressManager.LoadStage(sampleWaveToTest);
        }
            
        SpawnCharacter spawnCharacter;
        Debug.Log(stageWave.enemyWaves.Length);
        for (int i = 0; i < stageWave.enemyWaves.Length; i++)
        {
            spawnCharacter = ( (GameObject) Instantiate(prefabEnemy, this.transform)).GetComponent<SpawnCharacter>();
            Debug.Log(stageWave.enemyWaves[i].character);
            spawnCharacter.assignedCharacter = fullEnemiesList[stageWave.enemyWaves[i].character];
            spawnCharacter.prefabObject = spawnCharacter.assignedCharacter.prefabCharacter;
            stageWave.enemyWaves[i].spawnPoolCharacter = spawnCharacter;
        }

        totalSpawnedEnemiesInStage = 0;
        currentAliveEnemies = 0;
        imagewaveProgress.fillAmount = 1;
    }
    #endregion

    #region COROUTINES
    IEnumerator RoutineCreateEnemyWave(Wave wave)
    {
        for (int i = 0; i < wave.countEnemies; i++)
        {
            if (totalSpawnedEnemiesInStage >= stageWave.enemiesInThisStage)
                break;

            wave.spawnPoolCharacter.AskForObject().transform.position = spawnPoint.position;
            totalSpawnedEnemiesInStage++;
            currentAliveEnemies++;
            yield return new WaitForSeconds(wave.rateTimeSpawn);
            
        }
    }
    #endregion
}
