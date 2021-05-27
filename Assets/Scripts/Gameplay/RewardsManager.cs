using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsManager : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    [Range(0f,1f)]
    public float itemSpawnProbability = 0.3f;

    public Text textAwardedGemsThisLevel;
    #endregion

    #region PRIVATE_PROPERTIES
    private int startingGems;
    #endregion

    #region PUBLIC_REFERENCES
    public PoolManager poolCoins;
    public PoolManager poolGems;
    public RewardClass[] rewardsList;
    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        startingGems = GemsCurrency.TotalBalance;
    }

    private void OnEnable()
    {
        EventManager.StartListening("EnemyKilled", OnEnemyDeath);
        EventManager.StartListening("GameOver", OnGameOver);
    }

    private void OnDisable()
    {
        EventManager.StopListening("EnemyKilled", OnEnemyDeath);
        EventManager.StopListening("GameOver", OnGameOver);
    }
    #endregion

    #region PUBLIC_METHODS
    public void GenerateRandomItem(Vector3 positionToSpawn)
    {
        if (Random.Range(0f,1f)<=itemSpawnProbability)
        {
            if (Random.Range(0, 2) == 0)
                poolCoins.AskForObject().transform.position = positionToSpawn;
            else
                poolGems.AskForObject().transform.position = positionToSpawn;
        }
    }
    #endregion

    #region LISTENER_METHODS
    public void OnEnemyDeath(object param)
    {
        GenerateRandomItem(((Character)param).transform.position);
    }

    public void OnGameOver(object param)
    {
        textAwardedGemsThisLevel.text = (GemsCurrency.TotalBalance - startingGems).ToString();

        if ((bool)param)
        {
            GemsCurrency.AddCurrency(rewardsList[ProgressManager.GetCurrentStage()].gemsAwardedWin);
        }
        else
        {
            GemsCurrency.AddCurrency(rewardsList[ProgressManager.GetCurrentStage()].gemsAwardedLose);
        }


    }
    #endregion
}
