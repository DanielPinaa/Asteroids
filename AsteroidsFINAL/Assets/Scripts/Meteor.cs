using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // Destruccion del meteorito tras 10 segundos de su instancacion
        Destroy(gameObject, 10f);

    }
}
