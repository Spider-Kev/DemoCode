using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public Text textGems;
    public GameObject prefabAllyElement;
    public Transform parentPrefab;
    #endregion

    #region PRIVATE_PROPERTIES
    private GameObject createdInstance;
    #endregion

    #region UNITY_METHODS
    private void OnEnable()
    {
        EventManager.StartListening("GemsChanged", OnGemsChanged);
        EventManager.StartListening("FinishedLoadingProgress", OnFinishedLoadingProgress);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GemsChanged", OnGemsChanged);
        EventManager.StopListening("FinishedLoadingProgress", OnFinishedLoadingProgress);
    }
    #endregion

    #region PUBLIC_METHODS
    public void UpdateTotalGemsText()
    {
        textGems.text = GemsCurrency.TotalBalance.ToString();
    }

    public void FillAlliesStore()
    {
        UpdateTotalGemsText();
        EmptyInstancedList();
        GameSetUp.instance.UnlockCharactersAtActualStage();
        for (int i = 0; i < GameSetUp.instance.totalAllies.Length;i++)
        {
            createdInstance = (GameObject)Instantiate(prefabAllyElement, parentPrefab);
            createdInstance.GetComponent<AllyRow>().SetAllyStats(GameSetUp.instance.totalAllies[i], i);
        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void EmptyInstancedList()
    {
        AllyRow[] allyRows = parentPrefab.GetComponentsInChildren<AllyRow>();
        for (int i = 0; i < allyRows.Length; i++)
            Destroy(allyRows[i].gameObject);
    }
    #endregion

    #region LISTENER_METHODS
    public void OnGemsChanged(object param)
    {
        UpdateTotalGemsText();
    }

    public void OnFinishedLoadingProgress(object param)
    {
        GemsCurrency.SetGems(ProgressManager.GetGems());
        UpdateTotalGemsText();
    }
    #endregion
}
