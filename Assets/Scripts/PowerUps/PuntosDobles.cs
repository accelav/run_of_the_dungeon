using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosDobles : MonoBehaviour
{
    public float duracion = 10f; // Duración del power-up (10 segundos)

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que tocó el PowerUp tiene el tag "Jugador"
        if (other.CompareTag("Naves"))
        {
            // Intentar obtener el componente GameController del jugador
            // falta script enemigos
            ///////////////// GameObjecEnemigos gameobjecenemigos = other.GetComponent<GameObjecEnemigos>();

            // Verificar si se obtuvo el GameController correctamente
            /*   if (gameobjecenemigos != null)
              {
                  // Activar puntos dobles en el GameController
                ////////////////////  gameobjecenemigos.ActivarPuntosDobles(duracion);

                  // Destruir el power-up después de ser recogido
                  Destroy(gameObject);
              }*/
        }
        else
            {
                // Si no se encuentra el GameController, mostrar un mensaje de error
                Debug.LogError("El jugador no tiene el componente GameController.");
            }
        
    }
}



