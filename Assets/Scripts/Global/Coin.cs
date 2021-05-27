using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PoolObject
{
    #region UNITY_METHODS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinsCurrency.AddCurrency(5);
        this.StopAllCoroutines();
        EventManager.TriggerEvent("CoinPicked", null);
        this.gameObject.SetActive(false);
    }
    #endregion
}
