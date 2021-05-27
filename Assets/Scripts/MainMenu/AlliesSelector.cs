using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesSelector : MonoBehaviour
{
    public static AlliesSelector instance;

    

    #region PUBLIC_PROPERTIES
    public GameObject prefabAllyElement;
    public Transform parentPrefab;
    #endregion

    #region PUBLIC_REFERENCES
    public ChoosedAlly[] choosedAllies;
    #endregion

    #region PRIVATE_PROPERTIES
    private GameObject createdInstance;
    #endregion

    #region UNITY_METHODS
    private void Awake()
    {
        instance = this;
    }

    #endregion

    #region PUBLIC_METHODS
    public void SetAvailableAlliesForSelect()
    {
        EmptyInstancedElements();
        for (int i = 0; i < GameSetUp.instance.totalAllies.Length; i++)
        {
            if (ProgressManager.IsCharacterUnlocked(i))
            {
                createdInstance = (GameObject)Instantiate(prefabAllyElement, parentPrefab);
                createdInstance.GetComponent<CellAlly>().SetCharacterInfo(GameSetUp.instance.totalAllies[i]);
            }
        }
        UpdateAlliesIcons();
    }

    public void SelectCharacter(int indexCharacter)
    {
        if (GameSetUp.instance.selectedAlliesForScene.Contains(indexCharacter))
            return;

        GameSetUp.instance.selectedAlliesForScene.Add(indexCharacter);
        UpdateAlliesIcons();
    }

    public void RemoveSelectedCharacter(int indexCharacter)
    {
        GameSetUp.instance.selectedAlliesForScene.RemoveAt(indexCharacter);
        UpdateAlliesIcons();
    }    

    public void UpdateAlliesIcons()
    {
        for (int i = 0; i < choosedAllies.Length;i++)
        {
            
            try
            {
                choosedAllies[i].SetCharacter(GameSetUp.instance.totalAllies[GameSetUp.instance.selectedAlliesForScene[i]].iconCharacter);
            }
            catch
            {
                choosedAllies[i].SetCharacter(null);
            }


        }
    }
    #endregion

    #region PRIVATE_METHODS
    private void EmptyInstancedElements()
    {
        CellAlly[] cellAllies = parentPrefab.GetComponentsInChildren<CellAlly>();
        for (int i = 0; i < cellAllies.Length; i++)
            Destroy(cellAllies[i].gameObject);
    }
    #endregion
}
