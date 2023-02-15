using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneraSoldi : MonoBehaviour
{
    public GameObject soldo;
    public GameObject giocatore;

    private int numeroSoldiDopoOggetto;

    private List<GameObject> soldiNonPresiDaCancellare = new List<GameObject>();
    private List<GameObject> soldiTerrenoOro = new List<GameObject>();
    //private Dictionary<int, List<GameObject>> intToLista = new Dictionary<int, List<GameObject>>();
    private int noSoldiPerUltimoOggetto = 1;

    void Start()
    {
        numeroSoldiDopoOggetto = NumeroSoldiDopoOstacolo();
        GeneraSoldiIniziale();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        NumeroSoldiDopoOstacolo();
        GeneraSoldiUpdate();
    }

    private void GeneraSoldiIniziale()
    {
        //GeneraOggetti.oggettiPerCoin  e' una lista che contiene i primi oggetti creati
        for (int j = 0; j < GeneraOggetti.oggettiPerCoin.Count; j++)
        {
            //non si creano soldi per l' ultimo oggetto creato nella singola pista che contiene 4 oggetti
            if(noSoldiPerUltimoOggetto % GeneraOggetti.numeroOggettiPerPista != 0)
            {
                // vengono creati i soldi esattamente dopo gli oggetti creati e salvati su una lista
                for (int i = 0; i < numeroSoldiDopoOggetto; i++)
                {
                    Vector3 nuovaPosizione = new Vector3(GeneraOggetti.oggettiPerCoin[j].transform.position.x, 1, GeneraOggetti.oggettiPerCoin[j].transform.position.z + i + 1);
                    GameObject go = Instantiate(soldo, nuovaPosizione, soldo.transform.rotation);
                    soldiNonPresiDaCancellare.Add(go);
                }
            }
            noSoldiPerUltimoOggetto++;
        }
        //la lista degli oggetti creati viene svuotata in quanto ha svolto la sua funzione iniziale
        GeneraOggetti.oggettiPerCoin.Clear();
    }

    private void GeneraSoldiUpdate()
    {
        if(!ManagerDiPista.isTerrenoOroActive)
        {
            // quando il ManagerDiPista crea una nuova pista cominciamo a creare i soldi
            if (ManagerDiPista.isGenerata)
            {
                //i soldi verranno creati fino all ultimo oggetto solo se nella singola pista ci saranno 5 o 6 oggetti
                if (GeneraOggetti.numeroOggettiPerPista != 4 || GeneraOggetti.numeroOggettiPerPista != 7)
                {
                    //GeneraOggetti.oggettiPerCoin  e' una lista che contiene i primi oggetti creati
                    for (int j = 0; j < GeneraOggetti.oggettiPerCoin.Count; j++)
                    {
                        // vengono creati i soldi esattamente dopo gli oggetti creati e salvati su una lista
                        for (int i = 0; i < numeroSoldiDopoOggetto; i++)
                        {
                            Vector3 nuovaPosizione = new Vector3(GeneraOggetti.oggettiPerCoin[j].transform.position.x, 1, GeneraOggetti.oggettiPerCoin[j].transform.position.z + i + 1);
                            GameObject go = Instantiate(soldo, nuovaPosizione, soldo.transform.rotation);
                            soldiNonPresiDaCancellare.Add(go);
                        }
                    }
                }
                // altrimenti usiamo la regola iniziale
                else
                {
                    GeneraSoldiIniziale();
                }
                GeneraOggetti.oggettiPerCoin.Clear();
            }
            EliminaSoldiTerrenoOro();
        }
        else
        {
            float posZ = ManagerDiPista.pistaGenerata * 30;
            float dist = 0.6f;
            for(int i = 0; i < 40; i++)
            {
                Vector3 posCentrale = new Vector3(0, 1, posZ);
                GameObject go = Instantiate(soldo, posCentrale, soldo.transform.rotation);
                soldiTerrenoOro.Add(go);
                Vector3 posSx = new Vector3(-2.5f, 1, posZ);
                GameObject go1 = Instantiate(soldo, posSx, soldo.transform.rotation);
                soldiTerrenoOro.Add(go1);
                Vector3 posDx = new Vector3(2.5f, 1, posZ);
                GameObject go2 = Instantiate(soldo, posDx, soldo.transform.rotation);
                soldiTerrenoOro.Add(go2);
                posZ += dist;
            }
            ManagerDiPista.isTerrenoOroActive = false;
        }
        EliminaSoldiNonPresi();
    }

    private void EliminaSoldiNonPresi()
    {
        for(int i = 0; i < soldiNonPresiDaCancellare.Count; i++)
        {
            if(soldiNonPresiDaCancellare[i] != null && soldiNonPresiDaCancellare[i].transform.position.z + 5 < giocatore.transform.position.z)
            {
                Destroy(soldiNonPresiDaCancellare[i]);
            }
        }
    }

    private void EliminaSoldiTerrenoOro()
    {
        for(int i = 0; i < soldiTerrenoOro.Count; i++)
        {
            if (soldiTerrenoOro[i] != null && soldiTerrenoOro[i].transform.position.z + 30 < giocatore.transform.position.z)
            {
                Destroy(soldiTerrenoOro[i]);
            }
        }
    }

    private int NumeroSoldiDopoOstacolo()
    {
        if (GeneraOggetti.numeroOggettiPerPista == 4)
        {
             return numeroSoldiDopoOggetto = 7;
        }
        else if(GeneraOggetti.numeroOggettiPerPista == 5 || GeneraOggetti.numeroOggettiPerPista == 6)
        {
            return numeroSoldiDopoOggetto = 5;
        }
        else
        {
            return numeroSoldiDopoOggetto = 3;
        }
    }

}
