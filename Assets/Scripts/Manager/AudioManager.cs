using System;
using UnityEngine;
/*using UnityEngine.UI;*/
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    /*[SerializeField] Slider volumeSlider;
    public Slider volumeSlider;
    public AudioMixer mixer;
    private float value;*/

    public Suono[] sottofondo, effetti;
    public AudioSource sottofondoSource, effettiSource;
    public static bool isBgmMuted, isSnfxMuted;

    public static AudioManager Istance;

    void Start()
    {
        PlayMusic("BucoNero");
        isBgmMuted = false;
        isSnfxMuted = false;
    }


    void Awake()
    {
        if (Istance == null)
        {
            Istance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string nome)
    {
        Suono s = Array.Find(sottofondo, x => x.name == nome);

        if (s == null)
        {
            Debug.Log("Musica di sottofondo non trovata");
        }
        else
        {
            sottofondoSource.clip = s.clip;
            sottofondoSource.loop = s.loop;
            sottofondoSource.Play();
        }
    }

    public void StopPlaying(string nome)
    {
        Suono s = Array.Find(sottofondo, x => x.name == nome);

        if (s == null)
        {
            s = Array.Find(effetti, x => x.name == nome);
            effettiSource.clip = s.clip;
            effettiSource.Stop();
        }
        else
        {
            sottofondoSource.clip = s.clip;
            sottofondoSource.Stop();
        }
    }

    public void PlayEffect(string nome)
    {
        Suono s = Array.Find(effetti, x => x.name == nome);

        if (s == null)
        {
            Debug.Log("effetto non trovato");
        }
        else
        {
            effettiSource.clip = s.clip;
            effettiSource.loop = s.loop;
            effettiSource.PlayOneShot(s.clip);
        }
    }

    public void MutaSottofondo()
    {
        sottofondoSource.mute = !sottofondoSource.mute;
        isBgmMuted = !isBgmMuted;
    }

    public void MutaEffetti()
    {
        effettiSource.mute = !effettiSource.mute;
        isSnfxMuted = !isSnfxMuted;
    }

    public void VolumeSottofondo( float volume)
    {
        sottofondoSource.volume = volume;
        PlayerPrefs.SetFloat("Sottofondo", volume);
    }

    public void VolumeEffetti(float volume)
    {
        effettiSource.volume = volume;
        PlayerPrefs.SetFloat("Effetto", volume);
    }
}
