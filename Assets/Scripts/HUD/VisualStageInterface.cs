using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualStageInterface : MonoBehaviour
{
    #region PUBLIC_METHODS
    public Text textNextStage;
    #endregion

    #region UNITY_METHODS
    // CHANGE TO LISTENER FOR FINISHED PROGRESS MANAGER
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        textNextStage.text = "Play Stage \n" + (ProgressManager.GetCurrentStage() + 1).ToString();
    }
    #endregion
}
