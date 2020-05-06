using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMov;

    Vector2 movimiento;

    public Rigidbody2D rb;

    public float puntos;

    public bool corriendo;

    private bool parado;

    private bool idle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal");

        if (velocidadMov == 0 ){

            corriendo = false;

            parado = true;
        }

        else{

            corriendo = true;

            parado = false;
        }
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + movimiento * velocidadMov * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {

        if (trigger.gameObject.tag.Equals("Malo"))
        {
            Destroy(trigger.gameObject);
            puntos--;
        }

        if (trigger.gameObject.tag.Equals("Bueno"))
        {
            Destroy(trigger.gameObject);
            puntos++;
        }
    }
}
