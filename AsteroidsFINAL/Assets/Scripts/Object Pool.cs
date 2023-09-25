using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    // Variable que guardará las distintas balas de la pool
    public List<GameObject> pooledObjects;
    // Variable para indicar dentro de Unity que el objeto en la pool es de tipo Bullet
    public GameObject objectToPool;
    // Variable que indica la cantidad de balas que habrá en la pool
    public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        // Inicializacion de la lista de objetos de la pool
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        // Creacion de tantas balas para la pool como diga la variable amountToPool
        for (int i = 0; i < amountToPool; i++)
        {
            // Se guarda en tmp una bala y se desactiva
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            // Se anade la bala a pool
            pooledObjects.Add(tmp);
        }
    }

    // Metodo que devuelve una bala de la pool disponible para usarse
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            // Se comprueba si el objeto esta activo en la jerarquia
            if (!pooledObjects[i].activeInHierarchy)
            {
                // Si no lo esta, se devuelve esa bala
                return pooledObjects[i];
            }
        }
        return null;
    }
}
