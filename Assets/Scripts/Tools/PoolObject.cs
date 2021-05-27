using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    [Header("Respawn properties")]
    public float timeToDisable = 10;
    #endregion

    #region UNITY_METHODS
    private void OnEnable()
    {
        EnableObject();
    }
    #endregion

    #region PUBLIC_METHODS
    public void EnableObject()
    {
        StartCoroutine(RoutineDisableObject());
    }
    #endregion

    #region VIRTUAL_METHODS
    public virtual void DisableThisObject()
    {
        this.gameObject.SetActive(false);
    }
    #endregion

    #region COROUTINES
    IEnumerator RoutineDisableObject()
    {
        yield return new WaitForSeconds(timeToDisable);
        DisableThisObject();
    }
    #endregion

}
