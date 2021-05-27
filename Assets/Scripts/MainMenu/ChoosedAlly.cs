using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosedAlly : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public Image iconCharacter;
    public Canvas panelRemove;
    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        panelRemove.enabled = false;
    }
    #endregion

    #region PUBLIC_METHODS
    public void RemoveThisChoosedAlly()
    {
        AlliesSelector.instance.RemoveSelectedCharacter(this.transform.GetSiblingIndex());
    }

    public void SetCharacter(Sprite icon)
    {
        if (icon == null)
        {
            iconCharacter.sprite = null;
            panelRemove.enabled = false;
        }
        else
        {
            iconCharacter.sprite = icon;
            panelRemove.enabled = true;
        }

    }
    #endregion
}
