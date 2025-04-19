using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaDeVidas : MonoBehaviour
{
    public static SistemaDeVidas instance { get; private set; }



    void Awake()
    {
        // Asegurarse de que la instancia no sea null
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); // Evita que se duplique la instancia
    }





    public int vidas = 3;
    public int RecuperarVida = 1;


    void Update()
    {

        if (vidas <= 0)
        {

         //Canvas PerderPartida

        }
    }
    public void ActivarVida()
    {

        vidas = vidas + RecuperarVida;


    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            // vidas--;
            vidas--; // Resta una vida

            if (vidas <= 0)
            {

                //Canvas PerderPartida

            }

        }

    }

}
