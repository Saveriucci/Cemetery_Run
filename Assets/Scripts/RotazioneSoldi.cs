using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotazioneSoldi : MonoBehaviour
{
    public int rotateSpeed = 1;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && PlayerPrefs.GetInt("doppiSoldi", 0) == 0)
        {
            SuonoSoldi();
            PlayerManager.soldiCollezionati++;
            DistruggiOggetti();
        }
        else
        {
            SuonoSoldi();
            PlayerManager.soldiCollezionati += 2;
            DistruggiOggetti();
        }
    }

    private void DistruggiOggetti()
    {
        PlayerPrefs.SetInt("Soldi", PlayerManager.soldiCollezionati);
        Destroy(gameObject);
    }

    private void SuonoSoldi()
    {
        FindObjectOfType<AudioManager>().PlayEffect("Soldi");
    }
}
