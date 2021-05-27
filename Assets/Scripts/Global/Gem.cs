using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : PoolObject
{
    #region UNITY_METHODS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GemsCurrency.AddCurrency(1);
        this.StopAllCoroutines();
        EventManager.TriggerEvent("GemPicked", null);
        this.gameObject.SetActive(false);
    }
    #endregion


}
