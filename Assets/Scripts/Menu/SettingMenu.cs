using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public GameObject settingMenu;
    public Slider sottofondo, effetti;

    void Start()
    {
        sottofondo.value = PlayerPrefs.GetFloat("Sottofondo", 1);
        effetti.value = PlayerPrefs.GetFloat("Effetto", 1);
    }


    public void OpenSetting()
    {
        settingMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSetting()
    {
        settingMenu.SetActive(false);
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
