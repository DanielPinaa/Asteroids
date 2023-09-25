using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public Transform bulletSpawner;
    public Vector3 targetVector;
    public GameObject meteor1;
    public GameObject meteor2;
    public float xBorderLimit, yBorderLimit;


    // Start is called before the first frame update
    void OnEnable()
    {   
        // Tras 4 segundos la bala se desactivará (y podra volver a ser usada al estar disponible en la pool)
        Invoke("DesactivarGameObject", 1f);
        
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

        // Movimiento de la bala
        transform.Translate(targetVector * speed * Time.deltaTime);
        
        
    }
    // Manejo de colisiones con meteoritos
    // "Enemy" corresponde a los meteoritos que no han colisionado ninguna vez con una bala
    // "Hitted" se corresponde con un meteorito creado tras la colision de un "Enemy" con una bala (al siguiente impacto desaparece)
    private void OnTriggerEnter2D(Collider2D other){

        
        if (other.gameObject.tag == "Enemy") // Si es un meteorito al que todavia no le ha dado ninguna bala
        {
            // Llamada al metodo privado Duplicate y destruccion del GO
            Duplicate(other.gameObject);
            Destroy(other.gameObject);

            // Como desactivamos la bala en este metodo, cancelamos la involacion de "DesactivarGameObject"
            CancelInvoke("DesactivarGameObject");

            this.gameObject.SetActive(false); // Se desactiva la bala para que vuelva a estar disponible en la pool
            
            IncreaseScore();

        }
        else if (other.gameObject.tag == "Hitted") // En este caso se trata de un meteorito ya golpeado, por lo que no hay que duplicar
        {
            Destroy(other.gameObject);
            // Como desactivamos la bala en este metodo, cancelamos la involacion de "DesactivarGameObject"
            CancelInvoke("DesactivarGameObject");
            this.gameObject.SetActive(false);

            IncreaseScore();
        }
    }


    // Metodo para duplicar un meteorito golpeado
    private void Duplicate(GameObject gameObject)
    {
        // Se calcula la posicion actual del asteroide
        Vector3 newPosition = gameObject.transform.position;
        // Modificamos ligeramente el eje x para que no se instancien de manera superpuesta
        newPosition.x = gameObject.transform.position.x + 0.2f;
        newPosition.y = gameObject.transform.position.y;
        newPosition.z = gameObject.transform.position.z;

        // Se instancian los dos nuevos meteoritos (son de etiqueta "Hitted")
        Instantiate(meteor1, gameObject.transform.position, transform.rotation);
        Instantiate(meteor2, newPosition, transform.rotation);
    }

    private void IncreaseScore()
    {
        // Cuando un asteroide es destruido, se llama a esta función para dar puntos.
        Player.SCORE++;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        // Se llama a esta función cada vez que se ganan puntos para actualizar el marcador
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Score: " + Player.SCORE;
    }

    // Metodo privado para desactivar la bala
    private void DesactivarGameObject()
    {
        this.gameObject.SetActive(false);
    }
}
