using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ProgressManager
{
	public static PlayerProgress playerProgress;
	public static FullStages fullStagesWaves;
	public static CharacterStats alliesToUse;

	public static string pathToFile;

	public static bool IsReady { get { return playerProgress != null; } }

	public static void LoadPlayerProgress()
	{
		if (string.IsNullOrEmpty(pathToFile))
			pathToFile = Path.Combine(Application.persistentDataPath, "PlayerProgress");

		playerProgress = new PlayerProgress();
		string jsonResult;

		if (File.Exists(pathToFile))
        {
			jsonResult = File.ReadAllText(pathToFile);
			playerProgress = JsonUtility.FromJson<PlayerProgress>(jsonResult);
		}
		else
        {
			jsonResult = ((TextAsset)Resources.Load("PlayerProgress")).text;
			playerProgress = JsonUtility.FromJson<PlayerProgress>(jsonResult);
			SavePlayerProgress();
		}

		
	}

	public static void SavePlayerProgress()
    {
		File.WriteAllText(pathToFile,JsonUtility.ToJson(playerProgress,true));
    }

	public static int GetCurrentLevelForCharacter(int index)
    {
		return playerProgress.unlockedCharacters[index].level;
    }

	public static void UpdateCurrentLevelForCharacter(int index)
    {
		playerProgress.unlockedCharacters[index].level++;
		SavePlayerProgress();
    }

	public static bool IsCharacterUnlocked(int index)
    {
		return playerProgress.unlockedCharacters[index].unlocked;
    }

	public static void UnlockCharacter(int index)
    {
		playerProgress.unlockedCharacters[index].unlocked = true;
		SavePlayerProgress();
    }

	public static int GetGems()
    {
		return playerProgress.gemsCollected;
    }

	public static int GetCurrentStage()
	{
		
		return playerProgress.currentStage;
	}

	public static void NextStage()
    {
		playerProgress.currentStage++;
		SavePlayerProgress();
    }

	public static void LoadEnemyWaves()
	{
		string jsonResult = ((TextAsset) Resources.Load("Stages")).text;

		fullStagesWaves = new FullStages();
		fullStagesWaves = JsonUtility.FromJson<FullStages>(jsonResult);
	}

	public static StageWave LoadStage(int levelIndex)
	{
		return fullStagesWaves.stages [levelIndex];
	}
}