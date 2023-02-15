using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerDiPista : MonoBehaviour
{

    private float zSpawn = 0;
    public GameObject[] piste;
    public int numeroDiPista = 5;
    public Transform giocatore;
    public List<GameObject> pisteAttive = new List<GameObject>();

    public static int lunghezzaSingolaPista = 30;
    public static int pistaGenerata = 0;
    public static bool isGenerata = false;
    public static float probabilita = 10f;
    public static bool isTerrenoOroActive = false;


   void Start()
    {
        for( int i = 0; i < numeroDiPista; i++)
        {
            GeneraPista(Random.Range(0, piste.Length),pistaGenerata);
            pistaGenerata++;
        }
        if(PlayerPrefs.GetInt("boolProbabilita", 0) == 1)
        {
            probabilita += (PlayerPrefs.GetInt("indiceProbTerrOro1", 0) * PowerManager.aumentaProbabilitaTerrenoOro);
        }
    }


    void Update()
    {
        isGenerata = false;
        //continuo a generare piste ecomincio a cancellarne alcune -> endlessly
        if (giocatore.position.z - 30 > zSpawn - (numeroDiPista * lunghezzaSingolaPista))
        {
            if (Random.Range(0, 101) <= probabilita)
            {
                isTerrenoOroActive = true;
            }
            GeneraPista(Random.Range(0, piste.Length), pistaGenerata);
            pistaGenerata++;
            isGenerata = true;
            CancellaPista();
        }
    }

    public void GeneraPista( int indice, int pistaAttuale)
    {

        //Ogni pista che si istanzia verra salvata sulla lista di pisteAttive per poi essere 
        //successivamente cancellata
        GameObject go = Instantiate(piste[indice], transform.forward * zSpawn, transform.rotation);
        pisteAttive.Add(go);
        zSpawn += lunghezzaSingolaPista;
    }

    private void CancellaPista()
    {
        Destroy(pisteAttive[0]);
        pisteAttive.RemoveAt(0);
    }
}
