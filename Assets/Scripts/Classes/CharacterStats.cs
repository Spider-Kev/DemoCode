using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character_Healt", menuName = "CharacterStats/New Stat", order = 1)]
public class CharacterStats : ScriptableObject
{
    public GameObject prefabCharacter;

    [Header("In-Game stats")]
    public float totalHealt;
    public float damage;
    public float speed;
    public float timeBetweenAttack;

    [Header("For ally properties")]
    public string nameCharacter;
    public Sprite iconCharacter;
    public int spawnCost;
    public float timeToRespawn;
    public int currentLevel;
    public int unlockAtStage;
    public LevelsCharacter levelsForThisCharacter;
}
