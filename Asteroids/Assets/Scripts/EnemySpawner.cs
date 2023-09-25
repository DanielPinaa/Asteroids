using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRatePerMinute = 30;
    public float spawnRateIncrement = 1;
    public float xBorderLimit, yBorderLimit;
    private float spawnNext = 0;
    void Update()
    {
        // Instanciamos enemigos s�lo si ha pasado tiempo suficiente desde el �ltimo.
        if (Time.time > spawnNext)
        {
            // Indicamos cu�ndo podremos volver a instanciar otro enemigo
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            // Con cada spawn hay mas asteroides por minuto para incrementar la dificultad
            spawnRatePerMinute += spawnRateIncrement;
            // Guardamos un punto aleatorio entre las esquinas superiores de la pantalla
            var rand = Random.Range(-xBorderLimit, xBorderLimit);
            var spawnPosition = new Vector3(rand, yBorderLimit,5f);

            // Instanciamos el asteroide en el punto y con el �ngulo aleatorios
            Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
