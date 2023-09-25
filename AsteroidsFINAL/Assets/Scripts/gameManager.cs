using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{

    public GameObject pauseButton;

    void Start()
    {
        // Pausa inicialmente desactivada
        pauseButton.SetActive(false);

    }

    // Metodo para pausar el juego
    public void Pause()
    {
        pauseButton.SetActive(true);
        // Se pone la velocidad inicialmente a 0, por lo que el juego se detiene
        Time.timeScale = 0f;
    }

    // Metodo para reanudar el juego
    public void Resume()
    {
        pauseButton.SetActive(false);
        // Se pone la velocidad a uno, de manera que el juego se reanuda
        Time.timeScale = 1.0f;

    }

    public void Update()
    {
        // Se comprueba si se ha pulsado la tecla de pausa del juego
        if (Input.GetKey(KeyCode.P))
        {
            Pause();
        }

        // Se comprueba si se ha pulsado la tecla de reanudacion del juego
        if (Input.GetKey(KeyCode.R))
        {
            Resume();
        }


        if (Input.GetKey(KeyCode.Escape))
        {
            // Salir del juego
            Application.Quit();
        }
    }

}
