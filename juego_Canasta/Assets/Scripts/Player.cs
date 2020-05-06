using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float velocidadMov;

    Vector2 movimiento;

    public Rigidbody2D rb;

    public int puntos;

    private bool mirandoDerecha = true;

    public Animator playerAnim;

   // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        movimiento.x = 0;
        if(GameManager.Instance.GetCurrentGameState() == GameManager.GameState.playing)
        {
            movimiento.x = Input.GetAxisRaw("Horizontal");
        }


        playerAnim.SetFloat("Velocidad", Mathf.Abs(movimiento.x));

        if ( movimiento.x >0 && !mirandoDerecha){

            Flip();
        }
        else if( movimiento.x <0 &&mirandoDerecha){

            Flip();
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

            playerAnim.SetTrigger("Lastimado");           

        }

        if (trigger.gameObject.tag.Equals("Bueno"))
        {
            Destroy(trigger.gameObject);
            puntos++;
        }
    }

       

    void Flip(){
        mirandoDerecha = !mirandoDerecha;

        Vector3 escala = transform.localScale;
        escala.x = escala.x* -1;
        transform.localScale = escala;

    }
}
