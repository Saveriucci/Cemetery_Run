using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public int recordMetri;
    public TextMeshProUGUI testoSoldiTotali;
    public TextMeshProUGUI testoMetriRecord;

    void Start()
    {
        FindObjectOfType<AudioManager>().PlayMusic("BucoNero");
        recordMetri = PlayerPrefs.GetInt("Metri",0);
    }

    void Update()
    {
        if (recordMetri > PlayerPrefs.GetInt("RecordMetri", 0))
        {
            PlayerPrefs.SetInt("RecordMetri", recordMetri);
        }
        testoMetriRecord.text = "" + PlayerPrefs.GetInt("RecordMetri", 0);
        testoSoldiTotali.text = "" + PlayerPrefs.GetInt("Soldi", 0);
    }

    public void Gioca()
    {
        FindObjectOfType<AudioManager>().StopPlaying("BucoNero");
        FindObjectOfType<AudioManager>().PlayMusic("Lavandonia");
        SceneManager.LoadScene("Cimitero");
    }

    public void Negozio()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Lavandonia");
        FindObjectOfType<AudioManager>().PlayMusic("BucoNero");
        SceneManager.LoadScene("Negozio");
    }

    public void ChiudiGioco()
    {
        Application.Quit();
    }
}
