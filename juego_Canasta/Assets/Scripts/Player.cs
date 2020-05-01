using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velovidadMov;

    Vector2 movimiento;

    public Rigidbody2D rb;

    public float puntos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       movimiento.x = Input.GetAxisRaw("Horizontal"); 
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movimiento * velovidadMov * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D trigger){

         if (trigger.gameObject.tag.Equals("Malo")){

             puntos --;
         }

         if(trigger.gameObject.tag.Equals("Bueno")){

             puntos ++;
         }
     }  
}
