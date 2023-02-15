using UnityEngine.SceneManagement;
using UnityEngine;

public class Eventi : MonoBehaviour
{
   public void Replay()
    {
        FindObjectOfType<AudioManager>().PlayMusic("Lavandonia");
        SceneManager.LoadScene("Cimitero");
    }

    public void MainManu()
    {
        FindObjectOfType<AudioManager>().StopPlaying("Lavandonia");
        FindObjectOfType<AudioManager>().PlayMusic("BucoNero");
        SceneManager.LoadScene("MainMenu");
    }

}
