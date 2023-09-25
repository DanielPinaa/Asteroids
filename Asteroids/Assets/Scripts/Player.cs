using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    
    // Declaracion e inicializacion de las variables relacionadas con el movimiento de la nave
    public float rotationSpeed = 120f;
    public float movementSpeed = 5f;
    // Declaracion e inicializacion del marcador de puntos
    public static int SCORE = 0;
    // Declaracion de los GO necesarios para el disparo de las balas
    public GameObject bulletPrefab;
    public GameObject bulletSpawner;

    public bool juegoPausado = false;

    // Inicializacion de los bordes del mapa
    public float xBorderLimit, yBorderLimit;


    // Rigidbody para las fuerzas
    private Rigidbody2D _rigid; //La barrabaja se pone por convención en C# al ser una variable oculta

    // Start is called before the first frame update  (Solo se ejecuta 1 vez y es lo que se ejecuta)
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Espacio infinito
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit;
        transform.position = newPos;

        // Usamos Time.deltaTime para que la velocidad se adapte al tiempo entre dos frames de cada ordenador
        float thrust = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.forward, -rotation);
        
        Vector2 movementDirection = (Vector2)transform.right; // Dirección hacia la derecha

        _rigid.AddForce( thrust * movementDirection);

        // Script para que aparezca una bala de la pool de balas al pulsar la tecla "espacio" y se dispare
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
            if (bullet != null)
            {
                bullet.transform.position = bulletSpawner.transform.position;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true); // Se activa la bala para que aparezca e interactue con el mundo del juego
            }
        }
    }

    // Manejo de colisiones con meteoritos
    // "Enemy" corresponde a los meteoritos que no han colisionado ninguna vez con una bala
    // "Hitted" se corresponde con un meteorito creado tras la colision de un "Enemy" con una bala
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Hitted")
        {
            // Destruccion tanto la nave como el meteorito
            Destroy (gameObject);
            Destroy(other.gameObject);

            GameObject go = GameObject.FindGameObjectWithTag("UI");
            go.GetComponent<Text>().text = "Score: 0";
            // Reinicio del contador de puntos a 0
            SCORE = 0;
            // Cambio de escena
            SceneManager.LoadScene("Game Over");



        }
    }
}
