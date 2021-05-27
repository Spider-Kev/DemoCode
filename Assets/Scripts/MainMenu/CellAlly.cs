using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellAlly : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public Text textCoins;
    public Text textAttack;
    public Text textHealth;
    public Image iconImage;
    #endregion

    #region PUBLIC_METHODS
    public void SetCharacterInfo(CharacterStats stats)
    {
        int characterLevel = ProgressManager.GetCurrentLevelForCharacter(this.transform.GetSiblingIndex());
        textCoins.text = stats.spawnCost.ToString();
        textAttack.text = stats.levelsForThisCharacter.levels[characterLevel].damage.ToString();
        textHealth.text = stats.levelsForThisCharacter.levels[characterLevel].health.ToString();
        iconImage.sprite = stats.iconCharacter;
    }

    public void SelectThisCharacter()
    {
        AlliesSelector.instance.SelectCharacter(this.transform.GetSiblingIndex());
    }
    #endregion
}
