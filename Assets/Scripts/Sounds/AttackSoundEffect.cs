using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AttackSoundEffect : MonoBehaviour
{
    #region PUBLIC_PROPERTIES
    public AudioClip attackClipSound;
    public AudioSource source;
    #endregion

    #region PUBLIC_METHODS
    public void LaunchSoundEffect()
    {
        source.PlayOneShot(attackClipSound);
    }
    #endregion
}
