using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public static int soldiCollezionati;

    public GameObject gameOverPanel;
    public GameObject testoDiInizio;
    public PlayerController giocatore;
    public TextMeshProUGUI testoSoldi;
    public TextMeshProUGUI testoMetri;

    private int metri;
    private bool inMovimento;
    private float secondiInBaseAllaVelocita;

    //per la scelta del personaggio
    public GameObject[] personaggi;
    private int personaggioCorrente;



    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        Time.timeScale = 1;
        Debug.Log(PlayerPrefs.GetInt("indiceAumentoDist1", 0));
        testoMetri.text = "" + PowerManager.aumentaMetri * PlayerPrefs.GetInt("indiceAumentoDist1", 0);
        metri += PowerManager.aumentaMetri * PlayerPrefs.GetInt("indiceAumentoDist1", 0);
        inMovimento = false;
        soldiCollezionati = PlayerPrefs.GetInt("Soldi", 0);

        //Scelgo il personaggio da mostrare
        personaggioCorrente = PlayerPrefs.GetInt("Personaggio", 0);
        foreach (GameObject personaggio in personaggi)
        {
            personaggio.SetActive(false);
        }
        personaggi[personaggioCorrente].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        testoSoldi.text = soldiCollezionati.ToString();

        if (isGameStarted)
        {
            if (!inMovimento)
            {
                inMovimento = true;
                secondiInBaseAllaVelocita = SecondiVelocita();
                StartCoroutine(AggiungiDistanza());
            }
        }


        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

        }

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(testoDiInizio);
        }
    }

    private IEnumerator AggiungiDistanza()
    {
        metri += 1;
        testoMetri.text = "" + metri;
        PlayerPrefs.SetInt("Metri", metri);
        yield return new WaitForSeconds(secondiInBaseAllaVelocita);
        inMovimento = false;

    }

    private float SecondiVelocita()
    {
        float velocita = giocatore.velocita;
        if (velocita >= 0 && velocita <= 10)
        {
            secondiInBaseAllaVelocita = 0.35f;
        }
        else if (velocita > 10 && velocita <= 15)
        {
            secondiInBaseAllaVelocita = 0.3f;
        }
        else if (velocita > 15 && velocita <= 20)
        {
            secondiInBaseAllaVelocita = 0.2f;
        }
        else if (velocita > 20 && velocita <= 25)
        {
            secondiInBaseAllaVelocita = 0.1f;
        }
        return secondiInBaseAllaVelocita;

    }

}
