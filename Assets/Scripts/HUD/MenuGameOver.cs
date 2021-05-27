using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameOver : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public Canvas canvasGameOver;
    public Canvas panel_Lose;
    public Canvas panel_Win;
    #endregion

    #region PUBLIC_METHODS
    public void ShowGameOver(bool playerWon)
    {
        canvasGameOver.enabled = true;
        if (playerWon)
            panel_Win.enabled = true;
        else
            panel_Lose.enabled = true;
        
    }
    #endregion
}
