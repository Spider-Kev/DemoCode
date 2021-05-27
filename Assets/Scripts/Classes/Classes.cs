using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CoinsMachine
{
    public CoinsMachineLevel[] coinsMachineLevels;
}

[System.Serializable]
public struct CoinsMachineLevel
{
    public float timeToWaitForCoin;
    public float maxCoinLimit;
    public float upgradeCost;
}

[System.Serializable]
public class LevelsCharacter
{
    public Level[] levels;
}

[System.Serializable]
public class Level
{
    public int updateCost;
    public float damage;
    public float health;
}


[System.Serializable]
public class PlayerProgress
{
    public int currentStage;
    public int gemsCollected;
    public AlliesUnlocked[] unlockedCharacters;
}

[System.Serializable]
public class AlliesUnlocked
{
    public bool unlocked;
    public int level;
}

[System.Serializable]
public class FullStages
{
    public StageWave[] stages;
}



[System.Serializable]
public class StageWave
{
    public Wave[] enemyWaves;
    public int enemiesInThisStage;
    public int spawnThreshold;
}

[System.Serializable]
public class Wave
{
    public int character;
    public SpawnCharacter spawnPoolCharacter;
    public float rateTimeSpawn;
    public int countEnemies;
}

[System.Serializable]
public class RewardClass
{
    public int gemsAwardedWin;
    public int gemsAwardedLose;
}

[System.Serializable]
public class SFXSounds
{
    public string eventName;
    public AudioClip clipToShot;
}

