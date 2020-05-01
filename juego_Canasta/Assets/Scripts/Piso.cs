using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piso : MonoBehaviour
{
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onCollisionEnter2D (Collision2D col){

        if (col.gameObject.CompareTag("Bueno")){

            Destroy(col.gameObject);

        }
    }
}
