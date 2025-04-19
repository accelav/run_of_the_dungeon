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
            if (SistemaDeVidas.instance != null)
            {
                SistemaDeVidas.instance.ActivarVida();
            }
            else
            {
                Debug.LogError("La instancia de SistemaVida es null.");
            }

            // Destruimos el objeto (el power-up)
            Destroy(gameObject);
        }
    }

}
