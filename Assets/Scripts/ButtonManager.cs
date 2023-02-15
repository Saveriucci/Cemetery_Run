using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuonoBottoni : MonoBehaviour
{
    public void ButtonSound()
    {
        FindObjectOfType<AudioManager>().PlayEffect("ButtonPress");
    }
}
