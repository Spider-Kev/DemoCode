using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlliesSpawnController : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public Text textCoins;
    public Image imageAmountCoins;
    public CanvasGroup canvasGroup;
    public CoinsMachine coinsMachine;

    [Header("Spawn point for allies")]
    public Transform spawnPoint;
    
    #endregion

    #region PUBLIC_REFERENCES
    public AllyButtonSpawner[] buttonsForSpawnAllies;
    #endregion

    #region PRIVATE_PROPERTIES
    private int currentLevel;
    private float timeToWait;
    #endregion

    #region UNITY_METHODS
    private void OnEnable()
    {
        EventManager.StartListening("GameStarts", StartedGame);
        EventManager.StartListening("UpdatedCoinBalance", NewCoinBalance);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameStarts", StartedGame);
        EventManager.StopListening("UpdatedCoinBalance", NewCoinBalance);
    }

    private void Awake()
    {
        AssignSelectedAllies();
    }

    private void Start()
    {
        currentLevel = 0;
    }
    #endregion

    #region PUBLIC_METHODS
    public void StartedGame(object param)
    {
        timeToWait = coinsMachine.coinsMachineLevels[currentLevel].timeToWaitForCoin;
        CoinsCurrency.ResetCoins();
        imageAmountCoins.fillAmount = 0;
        textCoins.text = "x" + CoinsCurrency.TotalBalance.ToString();

        StartCoroutine(RoutineIncreaseCoins());
    }

    public void FinishedGame()
    {

    }

    public void NewCoinBalance(object param)
    {
        imageAmountCoins.fillAmount = CoinsCurrency.TotalBalance / coinsMachine.coinsMachineLevels[currentLevel].maxCoinLimit;
        textCoins.text = "x" + CoinsCurrency.TotalBalance.ToString();

        if (currentLevel >= coinsMachine.coinsMachineLevels.Length)
        {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0;
            return;
        }
            

        canvasGroup.interactable = CoinsCurrency.TotalBalance >= coinsMachine.coinsMachineLevels[currentLevel].upgradeCost;
        canvasGroup.alpha = canvasGroup.interactable ? 1 : 0;

    }

    public void UpdateCoinsMachineLevel()
    {
        CoinsCurrency.SubCurrency((int)coinsMachine.coinsMachineLevels[currentLevel].upgradeCost);
        currentLevel++;
        


    }

    public void PressButton(int buttonIndex)
    {
        buttonsForSpawnAllies[buttonIndex].SpawnCharacter(spawnPoint.position);
    }
    #endregion

    #region PRIVATE_METHODS
    private void AssignSelectedAllies()
    { 
        for (int i= 0; i < buttonsForSpawnAllies.Length;i++)
        {
            try
            {
                buttonsForSpawnAllies[i].characterSpawnPool.assignedCharacter =
                    GameSetUp.instance.totalAllies[
                        GameSetUp.instance.selectedAlliesForScene[i]];

                buttonsForSpawnAllies[i].characterSpawnPool.prefabObject = buttonsForSpawnAllies[i].characterSpawnPool.assignedCharacter.prefabCharacter;
            }
            catch
            {

            }
            
        }
    }
    #endregion

    #region COROUTINES
    IEnumerator RoutineIncreaseCoins()
    {
        while (!GameManager.isGameOver)
        {
            yield return new WaitForSeconds(timeToWait);
            if (CoinsCurrency.TotalBalance < coinsMachine.coinsMachineLevels[currentLevel].maxCoinLimit)
            {
                CoinsCurrency.AddCurrency(1);
            }
        }
    }
    #endregion
}


