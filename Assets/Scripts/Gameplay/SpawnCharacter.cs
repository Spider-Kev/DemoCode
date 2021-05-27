using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : PoolManager
{
    #region PUBLIC_REFERENCES
    public CharacterStats assignedCharacter;
    #endregion

    #region PRIVATE_REFERENCES
    private GameObject gameObjectCharacterSpawned;
    #endregion

    #region PUBLIC_METHODS
    public override GameObject CreateObject(bool enabledAtInstance = true)
    {
        gameObjectCharacterSpawned = base.CreateObject(enabledAtInstance);
        gameObjectCharacterSpawned.GetComponent<Character>().AssignCharacter(assignedCharacter);

        

        return gameObjectCharacterSpawned;
    }

    public override GameObject AskForObject()
    {
        gameObjectCharacterSpawned = base.AskForObject();
        gameObjectCharacterSpawned.GetComponent<Character>().StartValues();
        return gameObjectCharacterSpawned;
    }
    #endregion
}
