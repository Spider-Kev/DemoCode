using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Character
{
    public int currentLevel;
    private int characterIndex = -1;
    public override void StartValues()
    {
        base.StartValues();

        

        //if (characterIndex < 0)
            SearchCurrentLevel();

        totalHealt = currentHealth = stats.levelsForThisCharacter.levels[stats.currentLevel].health;
        currentDamage = stats.levelsForThisCharacter.levels[stats.currentLevel].damage;

        
    }

    private void SearchCurrentLevel()
    {
        
        for (int i = 0; i < GameSetUp.instance.totalAllies.Length; i++)
            if (GameSetUp.instance.totalAllies[i] == stats)
            {
                characterIndex = i;
                break;
            }

        currentLevel = ProgressManager.GetCurrentLevelForCharacter(characterIndex);
    }
}
