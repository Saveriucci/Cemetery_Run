using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direzioni;
    public float velocita = 5f;
    public float velocitaMassima = 25f;

    private int corsiaDesiderata = 1; // 0 a sinistra, 1 al centro, 2 a destra
    private float distanzaTraCorsie = 2.5f;

    private float forzaSalto = 8f;
    private float gravita = -20f;

    public Animator[] animazioni;
    private Animator animator;
    private int personaggioCorrente;

    private bool isSliding = false;
    private bool playSuonoCorsa;

    // cominciano i poteri
    private int doppioSalto;

    public Button diminuisciVelocita;

    public GameObject menuResurrezione;
    public GameObject personaggio;
    Vector3 nuovaPosizione; 
    private int resurrezionePossibile;


    void Start()
    {
        playSuonoCorsa = false;
        controller = GetComponent<CharacterController>();
        personaggioCorrente = PlayerPrefs.GetInt("Personaggio", 0);
        animator = animazioni[personaggioCorrente];

        //aggiorno la velocita del giocatore se ho comprato il relativo potere 
        if(PlayerPrefs.GetInt("boolVelocita", 0) == 1)
        {
            velocita += PlayerPrefs.GetInt("indiceVelocita1", 0) * PowerManager.aumentaVelocitaInPartenza;
        }

        //aggiorno il potere di diminuire la velocita
        if(PlayerPrefs.GetInt("diminuisciVelocita",0) == 1)
        {
            diminuisciVelocita.gameObject.SetActive(true);
        }

        resurrezionePossibile = 1;
        nuovaPosizione = new Vector3(0,0,0);
    }

    void Update()
    {
        // e' cominciato il gioco?
        if (!PlayerManager.isGameStarted)
        {
            FindObjectOfType<AudioManager>().StopPlaying("Corsa");
            return;
        }

        if (!playSuonoCorsa)
        {
            FindObjectOfType<AudioManager>().PlayEffect("Corsa");
            playSuonoCorsa = true;
        }
        else if(PlayerManager.gameOver)
        {
            FindObjectOfType<AudioManager>().StopPlaying("Corsa");
        }


        animator.SetBool("isGameStarted", true);

        if ( velocita < velocitaMassima)
            velocita += (0.002f * Time.deltaTime);
        direzioni.z = velocita;

        if (controller.isGrounded)
        {
            doppioSalto = 0;
            //gestisco il primo salto del giocatore anche se non ho sbloccato il potere del doppio salto
            if (!PowerManager.isUnlocked[4] || doppioSalto == 0)
            {
                direzioni.y = -3;
                if (SwipeManager.swipeUp)
                {
                    doppioSalto++;
                    animator.SetBool("isGrounded", false);
                    Jump();
                }
            }
        }
        else
        {
            //se mi trovo in aria e ho sbloccato il potere del doppio salto allora posso gestire il secondo salto
            if (SwipeManager.swipeUp && doppioSalto <= 1 && PlayerPrefs.GetInt("doppioSalto",0) == 1)
            {
                doppioSalto++;
                animator.SetBool("doppioSalto", true);
                SecondoSalto();
                animator.SetBool("doppioSalto", false);
            }
            animator.SetBool("isGrounded", true);
            direzioni.y += gravita * Time.deltaTime;
        }

        controller.Move(direzioni * Time.deltaTime);

        // input
        if (SwipeManager.swipeRight)
        {
            FindObjectOfType<AudioManager>().PlayEffect("Corsia");
            corsiaDesiderata++;
            if(corsiaDesiderata == 3)
            {
                corsiaDesiderata = 2;
            }
        }

        if (SwipeManager.swipeLeft)
        {
            FindObjectOfType<AudioManager>().PlayEffect("Corsia");
            corsiaDesiderata--;
            if (corsiaDesiderata == -1)
            {
                corsiaDesiderata = 0;
            }
        }

        if(SwipeManager.swipeDown && !isSliding)
        {
            FindObjectOfType<AudioManager>().StopPlaying("Corsa");
            FindObjectOfType<AudioManager>().PlayEffect("SlideDown");
            StartCoroutine(Slide());
            FindObjectOfType<AudioManager>().PlayEffect("Corsa");
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (corsiaDesiderata == 0)
        {
            targetPosition += Vector3.left * distanzaTraCorsie;
        }
        else if( corsiaDesiderata == 2)
        {
            targetPosition += Vector3.right * distanzaTraCorsie;
        }

        if(transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }


    private void Jump()
    {
        direzioni.y = forzaSalto;
        FindObjectOfType<AudioManager>().StopPlaying("Corsa");
        FindObjectOfType<AudioManager>().PlayEffect("Salto");
        FindObjectOfType<AudioManager>().PlayEffect("Corsa");
    }

    private void SecondoSalto()
    {
        direzioni.y = forzaSalto;
        FindObjectOfType<AudioManager>().PlayEffect("Salto");

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //se ho comprato il potere della resurrezione posso sopravvivere
        if (hit.transform.tag == "Obstacle" && (PlayerPrefs.GetInt("boolResurrezione", 0) == 1) && resurrezionePossibile == 1)
        {
            Time.timeScale = 0f;
            menuResurrezione.SetActive(true);
            FindObjectOfType<AudioManager>().StopPlaying("Corsa");
            //se muoio rinasco piu avanti
            nuovaPosizione.z = personaggio.transform.position.z +1;
            nuovaPosizione.x = personaggio.transform.position.x;
            nuovaPosizione.y = personaggio.transform.position.y;
        }
        //altrimenti muoio
        else if (hit.transform.tag == "Obstacle")
        {
            {
                FindObjectOfType<AudioManager>().PlayEffect("GameOver");
                PlayerManager.gameOver = true;
            }
        }
    }

    private void Resurrezione()
    {
        resurrezionePossibile = 0;
        controller.enabled = false;
        //do la nuova posizione al personaggio corrente
        controller.transform.position = nuovaPosizione;
        controller.enabled = true;
        velocita = 4f;
        menuResurrezione.SetActive(false);
        FindObjectOfType<AudioManager>().PlayEffect("Corsa");
        Time.timeScale = 1f;
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        // cambio le statistiche del controller per poter scivolare

        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 0f;
        yield return new WaitForSeconds(1f); // durata dell animazione
        controller.center = new Vector3(0, 0, 0);

        //rimetto le statistiche del controller come erano prima
        controller.height = 1;
        animator.SetBool("isSliding", false);

        isSliding = false;
    }

    private void DiminuisciVelocita()
    {
        velocita += PowerManager.diminuisciVelocitaInGioco;
        diminuisciVelocita.gameObject.SetActive(false);
    }
}
