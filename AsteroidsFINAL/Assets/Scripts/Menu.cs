using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambioDeEscena : MonoBehaviour
{
    // Declaracion de variable que guarda un sonido de audio
    public AudioSource audioSource;
    // Variable que guarda la escena a cargar despues del menu
    public string nombreDeLaEscenaACargar = "Juego";
    // Declaracion e inicializacion de la variable que guarda el numero de monedas insertadas 
    public int coins = 0;

    private void Start()
    {
        coins = 0;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            GameObject go = GameObject.FindGameObjectWithTag("Coins");
            GameObject ayuda = GameObject.FindGameObjectWithTag("Player");
            // Al pulsar la tecla "C" se incrementa el numero de monedas insertadas
            if (Input.GetKeyDown(KeyCode.C))
            {
                coins++;
                // Se actualiza el texto que tiene la informacion de las monedas insertadas
                go.GetComponent<Text>().text = "MONEDAS: " + coins;
                // Se activa el sonido de insertar moneda
                audioSource.Play();
            }

            // Si el numero de monedas es 0, todavia no se puede jugar, solo salir o insertar moneda
            if (coins == 0)
            {
                
                ayuda.GetComponent<Text>().text = "Pulsa [ESC] para salir";


                go.GetComponent<Text>().text = "INSERTA MONEDA [C]";



            }
            // Si monedas mayor que 0, ya se puede jugar
            if (coins > 0)
            {
                ayuda.GetComponent<Text>().text = "Pulsa [ESPACIO] para iniciar.\t\t\t\t\tPulsa [ESC] para salir.";
                // Verifica si se presiona la tecla Espacio
                if (Input.GetKeyDown(KeyCode.Space) && coins > 0)
                {
                    coins--;
                    // Carga la escena con el nombre especificado
                    SceneManager.LoadScene(nombreDeLaEscenaACargar);
                }
            }
        }
        else // En este punto nos encontramos en la escena llamada "Game Over"
        {
            // Verifica si se presiona la tecla Espacio
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Carga la escena con el nombre especificado
                SceneManager.LoadScene(nombreDeLaEscenaACargar);
            }
        }

   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Salir del juego
            Application.Quit();
        }

    }
}



