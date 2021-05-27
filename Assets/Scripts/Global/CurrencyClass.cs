using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoinsCurrency
{
    private static int totalBalance;

    public static int TotalBalance { get { return totalBalance; } }

    public static void AddCurrency(int amount)
    {
        totalBalance += amount;
        EventManager.TriggerEvent("UpdatedCoinBalance", null);
    }

    public static void SubCurrency(int amount)
    {
        totalBalance -= amount;
        EventManager.TriggerEvent("UpdatedCoinBalance", null);
    }

    public static void ResetCoins()
    {
        totalBalance = 0;
    }
}
