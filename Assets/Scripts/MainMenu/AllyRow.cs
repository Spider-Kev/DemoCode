using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyRow : MonoBehaviour
{
    public Image iconAlly;
    public Text allyName;
    public Text currentAttackStat;
    public Text currentHealthStat;
    public Text currentLevel;
    public Text upgradeCost;
    public Text unlockText;
    public GameObject stageMessageObject;
    public GameObject updateSection;
    public CharacterStats asociatedCharacter;

    private int indexCharacter;
    private int characterLevel;

    public void SetAllyStats(CharacterStats stats, int characterIndex)
    {
        indexCharacter = characterIndex;
        characterLevel = ProgressManager.GetCurrentLevelForCharacter(characterIndex);
        asociatedCharacter = stats;
        iconAlly.sprite = stats.iconCharacter;
        allyName.text = stats.nameCharacter;
        currentAttackStat.text = stats.levelsForThisCharacter.levels[characterLevel].damage.ToString();
        currentHealthStat.text = stats.levelsForThisCharacter.levels[characterLevel].health.ToString();
        if (characterLevel >= asociatedCharacter.levelsForThisCharacter.levels.Length - 1)
        {
            updateSection.SetActive(false);
        }
        else
            upgradeCost.text = stats.levelsForThisCharacter.levels[characterLevel + 1].updateCost.ToString();
        currentLevel.text = "Lvl " + characterLevel.ToString();
        stageMessageObject.SetActive(!ProgressManager.IsCharacterUnlocked(characterIndex));
        unlockText.text = "Unlock at stage " + stats.unlockAtStage.ToString();
    }

    public void PurchaseLevel()
    {
        int updateCost = asociatedCharacter.levelsForThisCharacter.levels[characterLevel + 1].updateCost;

        if (GemsCurrency.TotalBalance >= updateCost)
        {
            ProgressManager.UpdateCurrentLevelForCharacter(indexCharacter);
            UpdateRowInfo();
            GemsCurrency.SubCurrency(updateCost);
        }
        
    }

    private void UpdateRowInfo()
    {
        characterLevel = ProgressManager.GetCurrentLevelForCharacter(indexCharacter);
        if (characterLevel + 1 >= asociatedCharacter.levelsForThisCharacter.levels.Length)
            updateSection.SetActive(false);
        else
            upgradeCost.text = asociatedCharacter.levelsForThisCharacter.levels[characterLevel + 1].updateCost.ToString();

        currentAttackStat.text = asociatedCharacter.levelsForThisCharacter.levels[characterLevel].damage.ToString();
        currentHealthStat.text = asociatedCharacter.levelsForThisCharacter.levels[characterLevel].health.ToString();
        
        currentLevel.text = "Lvl " + characterLevel.ToString();

    }
}
