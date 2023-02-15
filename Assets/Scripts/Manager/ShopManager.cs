using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //informazioni personaggi
    public GameObject[] personaggi;  //lista dei personaggi del gioco
    public Animator[] animazioniPersonaggi; // ogni personaggio ha la propria animazione
    private Animator animazione;
    public Personaggio[] informazioniPersonaggi;
    private int personaggioCorrente;

    //informazioni UI
    public TextMeshProUGUI testoSoldiTotali;
    public TextMeshProUGUI testoCostoPersonaggio;

    //informazioni circa la possibilita di comprare un personaggio o meno
    public Button compraPersonaggio;
    public Button usaPersonaggio;
    public TextMeshProUGUI personaggioCorrenteInGioco;

    void Start()
    {
        testoSoldiTotali.text = " " + PlayerPrefs.GetInt("Soldi", 0);
        

        // verifico che i personaggi siano bloccati o meno
        foreach(Personaggio p in informazioniPersonaggi)
        {
            if(p.prezzo == 0)
            {
                p.isUnlocked = true;
            }
            else
            {
                p.isUnlocked = PlayerPrefs.GetInt(p.nome, 0) == 0 ? false: true;
            }
        }

        //se non si comprano personaggi allora il personaggio di default e' TIMMY
        personaggioCorrente = PlayerPrefs.GetInt("Personaggio", 0);
        foreach(GameObject personaggio in personaggi)
        {
            personaggio.SetActive(false);
        }
        personaggi[personaggioCorrente].SetActive(true);
        animazione = animazioniPersonaggi[personaggioCorrente];
        animazione.SetBool("isGameStarted", false);
    }

    // Update is called once per frame
    void Update()
    {
        AggiornaUI();
        testoSoldiTotali.text = " " + PlayerPrefs.GetInt("Soldi", 0);
        //PlayerController.animazioni[personaggioCorrente].SetBool("isGameStarted", false);
    }

    public void CambiaPersonaggioSuccessivo()
    {
        personaggi[personaggioCorrente].SetActive(false);
        personaggioCorrente++;

        if (personaggioCorrente == personaggi.Length)
            personaggioCorrente = 0;

        personaggi[personaggioCorrente].SetActive(true);

        Personaggio p = informazioniPersonaggi[personaggioCorrente];
        AggiornaUI();
        if (!p.isUnlocked)
            return;
    }

    public void CambiaPersonaggioPrecedente()
    {
        personaggi[personaggioCorrente].SetActive(false);
        personaggioCorrente--;

        if (personaggioCorrente == -1)
            personaggioCorrente = personaggi.Length - 1;

        personaggi[personaggioCorrente].SetActive(true);

        Personaggio p = informazioniPersonaggi[personaggioCorrente];
        AggiornaUI();
        if (!p.isUnlocked)
            return;
    }

    public void SbloccaPersonaggio()
    {
        Personaggio p = informazioniPersonaggi[personaggioCorrente];
        PlayerPrefs.SetInt(p.nome, 1);
        PlayerPrefs.SetInt("Personaggio", personaggioCorrente);
        p.isUnlocked = true;
        PlayerPrefs.SetInt("Soldi", PlayerPrefs.GetInt("Soldi", 0) - p.prezzo);
    }

    public void UsaPersonaggio()
    {
        usaPersonaggio.gameObject.SetActive(false);
        PlayerPrefs.SetInt("Personaggio", personaggioCorrente);
        personaggioCorrenteInGioco.gameObject.SetActive(true);
    }

    public void NegozioPrincipale()
    {
        SceneManager.LoadScene("Negozio");
    }


    public void AggiornaUI()
    {
        Personaggio p = informazioniPersonaggi[personaggioCorrente];
        // mi chiedo se il personaggio sia stato sbloccato
        if(p.isUnlocked)
        {
            compraPersonaggio.gameObject.SetActive(false);
            //disattivo il testo in uso se il personaggio corrente e' lo stesso di quello scelto
            if(personaggioCorrente == PlayerPrefs.GetInt("Personaggio", 0))
            {
                testoCostoPersonaggio.text = " ";
                usaPersonaggio.gameObject.SetActive(false);
                personaggioCorrenteInGioco.gameObject.SetActive(true);
            }
            //altrimenti il bottone in uso sa quello USA
            else
            {
                testoCostoPersonaggio.text = " ";
                personaggioCorrenteInGioco.gameObject.SetActive(false);
                usaPersonaggio.gameObject.SetActive(true);
            }
        }
        //se il personaggio ancora non e' stato sbloccato
        else
        {
            //bottone compra con il relativo prezzo
            compraPersonaggio.gameObject.SetActive(true);
            testoCostoPersonaggio.text = " " + p.prezzo;

            // non ho soldi a sufficienza
            if ( p.prezzo >= PlayerPrefs.GetInt("Soldi", 0))
            {
                compraPersonaggio.interactable = false;
                usaPersonaggio.gameObject.SetActive(false);
                personaggioCorrenteInGioco.gameObject.SetActive(false);
            }
            //altrimenti posso comprare il personaggio
            else
            {
                compraPersonaggio.interactable = true;
                usaPersonaggio.gameObject.SetActive(false);
                personaggioCorrenteInGioco.gameObject.SetActive(false);
            }
        }
    }
}
