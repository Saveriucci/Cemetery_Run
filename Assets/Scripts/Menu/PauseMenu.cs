using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuDiPausa;
    public Slider sottofondo, effetti;

    void Update()
    {
        sottofondo.value = PlayerPrefs.GetFloat("Sottofondo", 1);
        effetti.value = PlayerPrefs.GetFloat("Effetto", 1);
    }

    public void Pause()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Corsa");
        menuDiPausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        FindObjectOfType<AudioManager>().PlayEffect("Corsa");
        menuDiPausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void MutaSottofondo()
    {
        AudioManager.Istance.MutaSottofondo();
    }

    public void MutaEffetti()
    {
        AudioManager.Istance.MutaEffetti();
    }

    public void VolumeSottofondo()
    {
        AudioManager.Istance.VolumeSottofondo(sottofondo.value);
    }

    public void VolumeEffetti()
    {
        AudioManager.Istance.VolumeEffetti(effetti.value);
    }

}
