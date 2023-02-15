using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneraOggetti : MonoBehaviour
{
    public static int numeroOggettiPerPista = 4;
    public PlayerController giocatore;
    public GameObject[] oggettiDaGenerare; // oggetti che verranno inseriti in unity
    public static List<GameObject> oggettiPerCoin = new List<GameObject>();  //lista di oggetti per memorizzare la posizione dell oggetto in pista

    private Vector3 nuovaPosizione;
    private List<GameObject> oggettiDaCancellare;
    private Dictionary<int, List<GameObject>> intToLista = new Dictionary<int, List<GameObject>>(); // dizionario che associa ad ogni pista una lista di oggetti creati, utile per l eliminazione di questi ultimi
    private int pisteDaCancellare = 0;
    private float[] posizioni = { -2.5f, 0, 2.5f};

    void Start()
    {
       // numeroOggettiPerPista = 4;
        for ( int i = 0; i < ManagerDiPista.pistaGenerata; i++)
        {
            CreaOggettoIniziale(i);
        }
    }

    void LateUpdate()
    {
        CambiaNumeroOggettiInPista();
        CreaOggetto(ManagerDiPista.pistaGenerata);

    }

    private void CambiaNumeroOggettiInPista()
    {
        float velocita = giocatore.velocita;
        if(velocita >11 && velocita <15)
        {
            numeroOggettiPerPista = 5;
        }
        else if(velocita >= 15 && velocita < 20)
        {
            numeroOggettiPerPista = 6;
        }
        else if (velocita >= 20)
        {
            numeroOggettiPerPista = 7;
        }
    }
    
    public void CreaOggetto( int pistaAttuale)
    {
        //ho incontrato la probabilita di avere il terreno d oro?
        if (!ManagerDiPista.isTerrenoOroActive) {
            //se la pista e' stata generata allora creo gli oggetti
            if (ManagerDiPista.isGenerata && pistaAttuale > 4)
            {
                oggettiDaCancellare = new List<GameObject>();

                //creo gli oggetti
                for (int i = 0; i < numeroOggettiPerPista; i++)
                {
                    int interoRandomico = Random.Range(0, 10);
                    nuovaPosizione = ScegliNuovaPosizione(interoRandomico, i, pistaAttuale);
                    GameObject go = Instantiate(oggettiDaGenerare[interoRandomico], nuovaPosizione, oggettiDaGenerare[interoRandomico].transform.rotation);
                    oggettiDaCancellare.Add(go);
                    oggettiPerCoin.Add(go);
                }
                intToLista.Add(pistaAttuale, oggettiDaCancellare);
                EliminaOggetto(pisteDaCancellare); // si eliminano gli oggetti nella
                pisteDaCancellare++;
            }
        }
    }

    public void CreaOggettoIniziale(int pistaAttuale)
    {
        oggettiDaCancellare = new List<GameObject>();

        //creo gli oggetti
        for (int i = 0; i < numeroOggettiPerPista; i++)
        {
            int interoRandomico = Random.Range(0, 10);
            if (pistaAttuale != 0)
            {
                nuovaPosizione = ScegliNuovaPosizione(interoRandomico, i, pistaAttuale);
                GameObject go = Instantiate(oggettiDaGenerare[interoRandomico], nuovaPosizione, oggettiDaGenerare[interoRandomico].transform.rotation);
                oggettiPerCoin.Add(go);
                oggettiDaCancellare.Add(go);
            }
        }
        intToLista.Add(pistaAttuale, oggettiDaCancellare);
    }

    private Vector3 ScegliNuovaPosizione( int numero, int i, int pistaAttuale)
    {
        int lunghezzaSingolaPista = ManagerDiPista.lunghezzaSingolaPista;
        float distanzaTraOggetti = DistanzaTraOggetti(numeroOggettiPerPista);       // gli oggetti si troveranno ad una distanza fissata in base al numero degli oggetti

        if( numero == 5 || numero == 6)
        {
            return  new Vector3(0, 0.5f, (i * distanzaTraOggetti) + pistaAttuale * lunghezzaSingolaPista);
        }
        else if ( numero == 7)
        {
            return new Vector3(0, 3.85f, (i * distanzaTraOggetti) + pistaAttuale * lunghezzaSingolaPista);
        }
        else if (numero == 8)
        {
            return new Vector3(0, 1.89f, (i * distanzaTraOggetti) + pistaAttuale * lunghezzaSingolaPista);
        }
        else if ( numero == 9)
        {
            return new Vector3(posizioni[Random.Range(0, 3)], 1.26f, (i * distanzaTraOggetti) + pistaAttuale * lunghezzaSingolaPista);
        }
        else
        {
            return new Vector3(posizioni[Random.Range(0, 3)], 0.5f, (i * distanzaTraOggetti) + pistaAttuale * lunghezzaSingolaPista);
        }

    }
  

    //EliminaOggetti rimuove tutti quei oggetti generati che sono stati associati alla pista passata per parametro
    public void EliminaOggetto(int pista)
    {
        if( intToLista.ContainsKey(pista) && pista > 0)
        {
            List<GameObject> listaDiAppoggio = intToLista[pista];
            for (int i = 0; i < listaDiAppoggio.Count; i++)
            {
                Destroy(listaDiAppoggio[i]);
            }
            intToLista.Remove(pista);
        }
    }

    //DistanzaTraOggetti ritorna la distanza tra due oggetti in pista
    private float DistanzaTraOggetti(int numeroOggettiPerPista)
    {
        if(numeroOggettiPerPista == 4)
        {
            return 9f;
        }
        else if( numeroOggettiPerPista == 5 )
        {
            return 6f;
        }
        else if( numeroOggettiPerPista == 6)
        {
            return 5f; 
        }
        else 
        {
            return 4.5f;
        }
    }

}
