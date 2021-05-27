using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsCurrency
{
    private static int totalBalance;

    public static int TotalBalance { get { return totalBalance; } }

    public static void AddCurrency(int amount)
    {
        totalBalance += amount;
        ProgressManager.playerProgress.gemsCollected++;
        ProgressManager.SavePlayerProgress();
        EventManager.TriggerEvent("GemsChanged", null);
    }

    public static void SubCurrency(int amount)
    {
        totalBalance -= amount;
        ProgressManager.playerProgress.gemsCollected = totalBalance;
        ProgressManager.SavePlayerProgress();
        EventManager.TriggerEvent("GemsChanged",null);
    }

    public static void ResetGems()
    {
        totalBalance = 0;
        ProgressManager.playerProgress.gemsCollected = 0;
        ProgressManager.SavePlayerProgress();
    }

    public static void SetGems(int amount)
    {
        totalBalance = amount;
    }
}
