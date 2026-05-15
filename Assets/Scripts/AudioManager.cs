using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip tapClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip crackEggClip;
    private bool hasPlayEffectSound = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public bool HasPlayEffectSound()
    {
        return hasPlayEffectSound;
    }
    public void SetHasPlayEffectSound(bool value)
    {
        hasPlayEffectSound = value;
    }

    void Start()
    {
        effectSource.Stop();
        hasPlayEffectSound = true;
    }
    public void PlayJumpClip()
    {
        effectSource.PlayOneShot(jumpClip);
    }
    public void PlayTapClip()
    {
        effectSource.PlayOneShot(tapClip);
    }
    public void PlayHurtClip()
    {
        effectSource.PlayOneShot(hurtClip);
    }
    public void PlayCrackEgg()
    {
        effectSource.PlayOneShot(crackEggClip);
    }


}
