using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] AudioClip clickClip;
    [SerializeField] AudioClip equipClip;
    [SerializeField] AudioClip purchaseClip;
    [SerializeField] AudioClip coinGetClip;
    [SerializeField] AudioClip failureClip;
    [SerializeField] AudioClip popClip;
    [SerializeField] AudioClip stoneSlideClip;
    [SerializeField] AudioClip ghostyClip;

    [SerializeField] List<AudioSource> audioSources;

    public static SFXPlayer _instance;

    [SerializeField] float purgeFrequency = 2;
    [SerializeField] int sourcePool = 3;

    private void Awake()
    {
        _instance = this;

        // go ahead and make a pool of them at the start
        for (int i = 0; i < sourcePool; i++)
        {
            audioSources.Add(gameObject.AddComponent<AudioSource>());
        }

        Invoke("PurgeLoop", purgeFrequency);
    }

    void PurgeLoop()
    {
        List<AudioSource> toBeDestroyed = new List<AudioSource>();
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (i < sourcePool) continue;

            if (!audioSources[i].isPlaying)
            {
                toBeDestroyed.Add(audioSources[i]);
            }
        }

        foreach (var source in toBeDestroyed.ToArray())
        {
            audioSources.Remove(source);
            Destroy(source);
        }

        Invoke("PurgeLoop", purgeFrequency);
    }

    void PlaySound(AudioClip clip)
    {
        GetFreeAudioSource().PlayOneShot(clip);
    }

    AudioSource GetFreeAudioSource()
    {
        foreach (var source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.playOnAwake = false;
        newSource.loop = false;

        audioSources.Add(newSource);

        return newSource;
    }



    public static void PlayClickSound()
    {
        _instance.PlaySound(_instance.clickClip);
    }

    public static void PlayEquipSound()
    {
        _instance.PlaySound(_instance.equipClip);
    }

    public static void PlayPurchaseSound()
    {
        _instance.PlaySound(_instance.purchaseClip);
    }

    public static void PlayCoinSound()
    {
        _instance.PlaySound(_instance.coinGetClip);
    }
    public static void PlayPopSound()
    {
        _instance.PlaySound(_instance.popClip);
    }
    public static void PlayFailureSound()
    {
        _instance.PlaySound(_instance.failureClip);
    }
    public static void PlayStoneSlideSound()
    {
        _instance.PlaySound(_instance.stoneSlideClip);
    }
    public static void PlayGhostSound()
    {
        _instance.PlaySound(_instance.ghostyClip);
    }
}
