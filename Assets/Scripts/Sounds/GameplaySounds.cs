using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameplaySounds : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public SFXSounds[] sounds;
    public Dictionary<string, AudioClip> clipsToShot;
    public AudioSource backgroundAudioSource;
    public AudioSource audioSource;
    #endregion

    #region UNITY_METHODS
    private void Start()
    {
        clipsToShot = new Dictionary<string, AudioClip>();
        for (int i = 0; i < sounds.Length; i++)
            clipsToShot.Add(sounds[i].eventName, sounds[i].clipToShot);
    }

    private void OnEnable()
    {
        EventManager.StartListening("GameOver", OnGameOver);
        EventManager.StartListening("CoinPicked", OnCoinPickedUp);
        EventManager.StartListening("GemPicked", OnGemPickedUp);
        EventManager.StartListening("SpawnAlly", OnSpawnedAlly);
    }

    private void OnDisable()
    {
        EventManager.StopListening("GameOver", OnGameOver);
        EventManager.StopListening("CoinPicked", OnCoinPickedUp);
        EventManager.StopListening("GemPicked", OnGemPickedUp);
        EventManager.StopListening("SpawnAlly", OnSpawnedAlly);
    }
    #endregion

    #region LISTENER_METHODS
    public void OnGameOver(object param)
    {
        backgroundAudioSource.Stop();
        audioSource.PlayOneShot(clipsToShot[(bool) param ? "GameOverWin" : "GameOverLose"]);
    }

    public void OnCoinPickedUp(object param)
    {
        audioSource.PlayOneShot(clipsToShot["Coin"]);
    }

    public void OnGemPickedUp(object param)
    {
        audioSource.PlayOneShot(clipsToShot["Gem"]);
    }

    public void OnSpawnedAlly(object param)
    {
        audioSource.PlayOneShot(clipsToShot["SpawnAlly"]);
    }
    #endregion
}
