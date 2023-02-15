using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class NegozioPrincipale : MonoBehaviour
{
    public TextMeshProUGUI testoSoldiTotali;

    void Update()
    {
        testoSoldiTotali.text = "" + PlayerPrefs.GetInt("Soldi", 0);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PersonaggiStore()
    {
        SceneManager.LoadScene("PersonaggiNegozio");
    }

    public void PoteriStore()
    {
        SceneManager.LoadScene("PoteriNegozio");
    }
}
