using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyButtonSpawner : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public Image imageIconCharacter;
    public Image imageBlock;
    public Text textCost;
    public Button buttonSpawn;
    #endregion

    #region PUBLIC_REFERENCES
    public SpawnCharacter characterSpawnPool;
    #endregion

    #region PRIVATE_PROPERTIES
    private float timeSinceSpawn;
    private GameObject instancedCharacter;
    private WaitForEndOfFrame waitForEndOfFrame;
    #endregion

    #region UNITY_METHODS
    private void OnEnable()
    {
        EventManager.StartListening("UpdatedCoinBalance", NewCoinBalance);
    }

    private void OnDisable()
    {
        EventManager.StopListening("UpdatedCoinBalance", NewCoinBalance);
    }

    private void Start()
    {
        waitForEndOfFrame = new WaitForEndOfFrame();

        if (characterSpawnPool.assignedCharacter == null)
            return;

        buttonSpawn.interactable = false;
        textCost.text = characterSpawnPool.assignedCharacter.spawnCost.ToString();
        imageIconCharacter.sprite = characterSpawnPool.assignedCharacter.iconCharacter;
        timeSinceSpawn = characterSpawnPool.assignedCharacter.timeToRespawn;


    }
    #endregion

    #region PUBLIC_METHODS
    public void NewCoinBalance(object param)
    {
        if (characterSpawnPool.assignedCharacter == null)
            return;
        buttonSpawn.interactable = CoinsCurrency.TotalBalance >= characterSpawnPool.assignedCharacter.spawnCost;
    }

    public void SpawnCharacter(Vector3 positionSpawn)
    {
        if (timeSinceSpawn < characterSpawnPool.assignedCharacter.timeToRespawn)
            return;

        EventManager.TriggerEvent("SpawnAlly",null);

        instancedCharacter = characterSpawnPool.AskForObject();
        instancedCharacter.transform.position = positionSpawn;
        instancedCharacter.GetComponent<Character>().StartValues();
        CoinsCurrency.SubCurrency(characterSpawnPool.assignedCharacter.spawnCost);
        StartCoroutine(RoutineBlockButton());
    }
    #endregion

    #region COROUTINES
    IEnumerator RoutineBlockButton()
    {
        timeSinceSpawn = 0;
        imageBlock.fillAmount = 1;
        while(timeSinceSpawn< characterSpawnPool.assignedCharacter.timeToRespawn)
        {
            imageBlock.fillAmount = 1 - (timeSinceSpawn/characterSpawnPool.assignedCharacter.timeToRespawn);
            timeSinceSpawn += Time.deltaTime;
            yield return waitForEndOfFrame;
        }
    }
    #endregion
}
