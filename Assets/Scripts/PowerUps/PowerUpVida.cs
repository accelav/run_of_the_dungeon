using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpVida : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        // Comprobamos si el objeto con el que colisionamos tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            SoundsBehaviour.instance.PlayClip(9);
            if (SistemaDeVidas.instance != null)
            {
                SistemaDeVidas.instance.RecuperarCorazon();
            }
           
            // Destruimos el objeto (el power-up)
            Destroy(gameObject);
        }
    }

}
