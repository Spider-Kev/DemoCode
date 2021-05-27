using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackSystem : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public GameObject[] prefabsSpecialAttacks;
    #endregion

    #region PRIVATE_PROPERTIES
    private int indexCastedAttack;
    private WaitForEndOfFrame waitForEndOfFrame;
    #endregion

    #region PUBLIC_REFERENCES
    public Character targetCharacter;
    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        waitForEndOfFrame = new WaitForEndOfFrame();
    }
    #endregion

    #region PUBLIC_METHODS
    public void StartSpecialAttack(int indexAttack)
    {
        targetCharacter.characterState = Character.EnumCharacterState.Busy;
        targetCharacter.animator.SetTrigger("Special_1");
        indexCastedAttack = indexAttack;
    }

    public void CastAttack()
    {
        prefabsSpecialAttacks[indexCastedAttack].transform.position = targetCharacter.transform.position;
        prefabsSpecialAttacks[indexCastedAttack].SetActive(true);
    }

    public void FinishSpecialAttack()
    {
        targetCharacter.characterState = Character.EnumCharacterState.Moving;
    }
    #endregion

    #region COROUTINES

    #endregion
}
