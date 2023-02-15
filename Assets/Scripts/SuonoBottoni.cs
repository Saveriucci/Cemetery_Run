using UnityEngine.UI;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    public Button musica;
    public Button effetti;

    void Start()
    {

        if (AudioManager.isBgmMuted)
        {
            musica.GetComponent<Image>().sprite = sprites[1];
        }
        else
        {
            musica.GetComponent<Image>().sprite = sprites[0];
        }

        if (AudioManager.isSnfxMuted)
        {
            effetti.GetComponent<Image>().sprite = sprites[3];
        }
        else
        {
            effetti.GetComponent<Image>().sprite = sprites[2];
        }
    }


    public void ButtonSound()
    {
        FindObjectOfType<AudioManager>().PlayEffect("ButtonPress");
    }

    public void cambiaSpriteMusica()
    {
        if(musica.GetComponent<Image>().sprite == sprites[0])
        {
            musica.GetComponent<Image>().sprite = sprites[1];
            return;
        }
        else
        {
            musica.GetComponent<Image>().sprite = sprites[0];
        }
    }

    public void cambiaSpriteEffetti()
    {
        if (effetti.GetComponent<Image>().sprite == sprites[2])
        {
            effetti.GetComponent<Image>().sprite = sprites[3];
            return;
        }
        else
        {
            effetti.GetComponent<Image>().sprite = sprites[2];
        }
    }
}
