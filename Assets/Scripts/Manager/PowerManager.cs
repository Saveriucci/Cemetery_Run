using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PowerManager : MonoBehaviour
{
    // 0 doppi soldi, 1 aumenta prob.oro, 2 resurrezione, 3 aumenta metri, 4 doppio salto, 5 aumenta velocita, 6 diminuisci velocita
    public static bool[] isUnlocked = new bool[7];
    public TextMeshProUGUI[] testoSoldi;
    public TextMeshProUGUI[] testoLivMax;
    public Button[] compraPotere;


    public static int costoDoppiosalto = 250;
    public static int costoDoppiSoldi = 2500;

    public static float aumentaVelocitaInPartenza = 0.1f;
    public TextMeshProUGUI testoBottoneAV;
    public static int[] costoAumentaVelocita = { 150, 200, 300, 400, 450, 600, 800, 900, 950, 1000 };

    public static int costoDiminuisciVelocitaInGioco = 1500;
    public static float diminuisciVelocitaInGioco = -2;

    public static float aumentaProbabilitaTerrenoOro = 1;
    public TextMeshProUGUI testoBottoneAPO;
    public static int[] costoAumentoTerrenoOro = { 250, 500, 750, 1000, 1250, 1500, 1750, 2000, 2250, 2500 };

    public TextMeshProUGUI testoSoldiTotali;

    public static int costoResurrezione = 3000;

    public static int aumentaMetri = 10;
    public TextMeshProUGUI testoBottoneAM;

    public static int[] costoAumentaDistanza = { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    private string[] contoInLettere = { "uno", "due", "tre", "quattro", "cinque", "sei", "sette", "otto", "nove", "dieci" };

    private int soldiPosseduti;

    void Start()
    {
        soldiPosseduti = PlayerPrefs.GetInt("Soldi", 0);
        testoSoldiTotali.text = " " + soldiPosseduti;
        InizializzaIsUnlocked();
        AggiornaCostiIniziale();
        AggiornaUI1();
        AggiornaUI2();
    }

    // Update is called once per frame
    void Update()
    {
        AggiornaUI1();
        AggiornaUI2();
        AggiornaSoldi();
    }

    private void AggiornaSoldi()
    {
        testoSoldiTotali.text = " " + PlayerPrefs.GetInt("Soldi", 0);
    }

    private void AggiornaCostiIniziale()
    {
        testoSoldi[4].text = "" + costoDoppiosalto;
        testoSoldi[0].text = "" + costoDoppiSoldi;
        testoSoldi[5].text = "" + costoAumentaVelocita[PlayerPrefs.GetInt("indiceVelocita", 0)];
        testoSoldi[6].text = "" + costoDiminuisciVelocitaInGioco;
        testoSoldi[1].text = "" + costoAumentoTerrenoOro[PlayerPrefs.GetInt("indiceProbTerrOro", 0)];
        testoSoldi[2].text = "" + costoResurrezione;
        testoSoldi[3].text = "" + costoAumentaDistanza[PlayerPrefs.GetInt("indiceAumentoDist", 0)];
    }

    private void InizializzaIsUnlocked()
    {
        for( int i = 0; i < isUnlocked.Length; i++)
        {
            isUnlocked[i] = false;
        }
    }

    public void NegozioPrincipale()
    {
        SceneManager.LoadScene("Negozio");
    }

    public void CompraDoppioSalto()
    {
        isUnlocked[4] = true;
        PlayerPrefs.SetInt("doppioSalto", (isUnlocked[4] ? 1 : 0));
        PlayerPrefs.SetInt("Soldi", soldiPosseduti - costoDoppiosalto);
        AggiornaUI1();
        AggiornaUI2();
        AggiornaSoldi();
    }

    public void CompraDoppiSoldi()
    {
        isUnlocked[0] = true;
        PlayerPrefs.SetInt("doppiSoldi", (isUnlocked[0] ? 1 : 0));
        PlayerPrefs.SetInt("Soldi", soldiPosseduti - costoDoppiosalto);
        AggiornaUI1();
        AggiornaUI2();
        AggiornaSoldi();
    }

    public void CompraAV()
    {
        isUnlocked[5] = true;
        PlayerPrefs.SetInt("boolVelocita", (isUnlocked[5] ? 1 : 0));
        PlayerPrefs.SetInt("Soldi", soldiPosseduti - costoAumentaVelocita[PlayerPrefs.GetInt("indiceVelocita", 0)]);
        PlayerPrefs.SetInt("indiceVelocita", PlayerPrefs.GetInt("indiceVelocita", 0) + 1);
        PlayerPrefs.SetInt("indiceVelocita1", PlayerPrefs.GetInt("indiceVelocita", 0));
        if (PlayerPrefs.GetInt("indiceVelocita", 0) == 10)
        {
            PlayerPrefs.SetInt("indiceVelocita", PlayerPrefs.GetInt("indiceVelocita", 0) - 1);
        }
        AggiornaUI1();
        AggiornaUI2();
        AggiornaSoldi();
    }

    public void CompraDV()
    {
        isUnlocked[6] = true;
        PlayerPrefs.SetInt("diminuisciVelocita", (isUnlocked[6] ? 1 : 0));
        PlayerPrefs.SetInt("soldi", soldiPosseduti - costoDiminuisciVelocitaInGioco);
        AggiornaUI1();
        AggiornaUI2();
        AggiornaSoldi();
    }

    public void CompraAPO()
    {
        isUnlocked[1] = true;
        PlayerPrefs.SetInt("boolProbabilita", (isUnlocked[1] ? 1 : 0));
        PlayerPrefs.SetInt("Soldi", soldiPosseduti - costoAumentoTerrenoOro[PlayerPrefs.GetInt("indiceProbTerrOro", 0)]);
        PlayerPrefs.SetInt("indiceProbTerrOro", PlayerPrefs.GetInt("indiceProbTerrOro", 0) + 1);
        PlayerPrefs.SetInt("indiceProbTerrOro1", PlayerPrefs.GetInt("indiceProbTerrOro", 0));
        if (PlayerPrefs.GetInt("indiceProbTerrOro", 0) == 10)
        {
            PlayerPrefs.SetInt("indiceProbTerrOro", PlayerPrefs.GetInt("indiceProbTerrOro", 0) - 1);
        }
        AggiornaUI1();
        AggiornaUI2();
        AggiornaSoldi();
    }

    public void CompraRES()
    {
        isUnlocked[2] = true;
        PlayerPrefs.SetInt("boolResurrezione", (isUnlocked[2] ? 1 : 0));
        PlayerPrefs.SetInt("soldi", soldiPosseduti - costoResurrezione);
        AggiornaUI1();
        AggiornaUI2();
        AggiornaSoldi();
    }

    public void CompraAM()
    {
        isUnlocked[3] = true;
        PlayerPrefs.SetInt("boolDistanza", (isUnlocked[3] ? 1 : 0));
        PlayerPrefs.SetInt("Soldi", soldiPosseduti - costoAumentaDistanza[PlayerPrefs.GetInt("indiceAumentoDist", 0)]);
        PlayerPrefs.SetInt("indiceAumentoDist", PlayerPrefs.GetInt("indiceAumentoDist", 0) + 1);
        //per uso esterno
        PlayerPrefs.SetInt("indiceAumentoDist1", PlayerPrefs.GetInt("indiceAumentoDist", 0));
        //accortezza sull ultimo indice
        if (PlayerPrefs.GetInt("indiceAumentoDist", 0) == 10)
        {
            PlayerPrefs.SetInt("indiceAumentoDist", PlayerPrefs.GetInt("indiceAumentoDist", 0) - 1);
        }
        AggiornaUI1();
        AggiornaUI2();
        AggiornaSoldi();
    }


    private void AggiornaUI1()
    {
        //aggiorno il doppio salto
        //altrimenti se non ho soldi per comprare il potere il bottone non sara cliccabile
        if (costoDoppiosalto > soldiPosseduti)
        {
            compraPotere[4].interactable = false;
        }
        //altrimenti si
        else
        {
            compraPotere[4].interactable = true;
        }

        //aggiorno il potere dei doppi soldi
        //altrimenti se non ho soldi per comprare il potere il bottone non sara cliccabile
        if (costoDoppiSoldi > soldiPosseduti)
        {
            compraPotere[0].interactable = false;
        }
        //altrimenti si
        else
        {
            compraPotere[0].interactable = true;
        }

        //aggiorno il potere dell aumento della velocita
        ////altrimenti se non ho soldi per comprare il potere il bottone non sara cliccabile
        if (costoAumentaVelocita[PlayerPrefs.GetInt("indiceVelocita",0)] > soldiPosseduti)
        {
            testoBottoneAV.text = "compra liv " + contoInLettere[PlayerPrefs.GetInt("indiceVelocita", 0)];
            testoSoldi[5].text = "" + costoAumentaVelocita[PlayerPrefs.GetInt("indiceVelocita", 0)];
            compraPotere[5].interactable = false;
        }
        //altrimenti si
        else
        {
            testoBottoneAV.text = "compra liv " + contoInLettere[PlayerPrefs.GetInt("indiceVelocita", 0)];
            testoSoldi[5].text = "" + costoAumentaVelocita[PlayerPrefs.GetInt("indiceVelocita", 0)];
            compraPotere[5].interactable = true;
        }

        //aggiorno il potere di diminuzione della velocita durante il gioco
        ////altrimenti se non ho soldi per comprare il potere il bottone non sara cliccabile
        if (costoDiminuisciVelocitaInGioco > soldiPosseduti)
        {
            compraPotere[6].interactable = false;
        }
        //altrimenti si
        else
        {
            compraPotere[5].interactable = true;
        }

        //aggiorno il potere dell aumento della porbabilita di trovare il terreno d' oro
        //altrimenti se non ho soldi per comprare il potere il bottone non sara cliccabile
        if (costoAumentoTerrenoOro[PlayerPrefs.GetInt("indiceProbTerrOro", 0)] > soldiPosseduti)
        {
            testoBottoneAPO.text = "compra liv " + contoInLettere[PlayerPrefs.GetInt("indiceProbTerrOro", 0)];
            testoSoldi[1].text = "" + costoAumentoTerrenoOro[PlayerPrefs.GetInt("indiceProbTerrOro", 0)];
            compraPotere[1].interactable = false;
        }
        //altrimenti si
        else
        {
            testoBottoneAPO.text = "compra liv " + contoInLettere[PlayerPrefs.GetInt("indiceProbTerrOro", 0)];
            testoSoldi[1].text = "" + costoAumentoTerrenoOro[PlayerPrefs.GetInt("indiceProbTerrOro", 0)];
            compraPotere[1].interactable = true;
        }

        //aggiorno il potere della resurrezione
        //altrimenti se non ho soldi per comprare il potere il bottone non sara cliccabile
        if (costoResurrezione > soldiPosseduti)
        {
            compraPotere[2].interactable = false;
        }
        //altrimenti si
        else
        {
            compraPotere[2].interactable = true;
        }

        //aggiorno il potere dell aumento dei metri "percorsi" in partenza
        //altrimenti se non ho soldi per comprare il potere il bottone non sara cliccabile
        if (costoAumentaDistanza[PlayerPrefs.GetInt("indiceAumentoDist", 0)] > soldiPosseduti)
        {
            testoBottoneAM.text = "compra liv " + contoInLettere[PlayerPrefs.GetInt("indiceAumentoDist", 0)];
            testoSoldi[3].text = "" + costoAumentaDistanza[PlayerPrefs.GetInt("indiceAumentoDist", 0)];
            compraPotere[3].interactable = false;
        }
        //altrimenti si
        else
        {
            testoBottoneAM.text = "compra liv " + contoInLettere[PlayerPrefs.GetInt("indiceAumentoDist", 0)];
            testoSoldi[3].text = "" + costoAumentaDistanza[PlayerPrefs.GetInt("indiceAumentoDist", 0)];
            compraPotere[3].interactable = true;
        }
    }

    //se ho comprato il potere allora o lo aggiorno al costo successivo oppure mostro LIVELLO MAX
    private void AggiornaUI2()
    {
        //doppi salto
        if (PlayerPrefs.GetInt("doppioSalto", 0) == 1)
        {
            compraPotere[4].gameObject.SetActive(false);
            testoLivMax[4].gameObject.SetActive(true);
        }
        // doppi soldi
        if (PlayerPrefs.GetInt("doppiSoldi", 0) == 1)
        {
            compraPotere[0].gameObject.SetActive(false);
            testoLivMax[0].gameObject.SetActive(true);
        }
        //aumenta velocita
        if(PlayerPrefs.GetInt("indiceVelocita1", 0) == 10)
        {
            compraPotere[5].gameObject.SetActive(false);
            testoLivMax[5].gameObject.SetActive(true);
        }
        //diminuisci velocita
        if(PlayerPrefs.GetInt("diminuisciVelocita",0) == 1)
        {
            compraPotere[6].gameObject.SetActive(false);
            testoLivMax[6].gameObject.SetActive(true);
        }
        //aumento probabilita
        if (PlayerPrefs.GetInt("indiceProbTerrOro1", 0) == 10)
        {
            compraPotere[1].gameObject.SetActive(false);
            testoLivMax[1].gameObject.SetActive(true);
        }
        //resurrezione
        if(PlayerPrefs.GetInt("boolResurrezione", 0) == 1)
        {
            compraPotere[2].gameObject.SetActive(false);
            testoLivMax[2].gameObject.SetActive(true);
        }
        //aumento distanza
        if (PlayerPrefs.GetInt("indiceAumentoDist1", 0) == 10)
        {
            compraPotere[3].gameObject.SetActive(false);
            testoLivMax[3].gameObject.SetActive(true);
        }
    }
}
