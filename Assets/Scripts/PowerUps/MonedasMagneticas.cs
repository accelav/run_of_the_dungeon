using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasMagneticas : MonoBehaviour
{
    [SerializeField] float pingPongSpeed;
    [SerializeField] float maxScale;
    [SerializeField] float minScale;
    [SerializeField] float rotSpeed;
    [SerializeField] int puntos;

    private void Update()
    {
        //ScalePingPong();
        IdleRotation();
    }

    void IdleRotation()
    {
        rotSpeed++;
        transform.rotation = Quaternion.Euler(0, rotSpeed, 0);
    }
    void ScalePingPong()
    {
        float pingPong = Mathf.PingPong(Time.time * pingPongSpeed, maxScale - minScale) + minScale;
        transform.localScale = new Vector3(pingPong, pingPong, pingPong);
    }
    void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("Player"))
        {
            SoundsBehaviour.instance.PlayClip(7);

            // Se obtiene el componente PlayerController del objeto raíz del objeto 'other'
            PlayerController player = other.transform.root.GetComponent<PlayerController>();
            int puntos = 1;

            if (player != null && player.PuntosDoblesActivos)
            {
                puntos *= 2; // Duplica los puntos
            }

            GameController.instance.AgregarPuntos(puntos);
            Destroy(gameObject);
        }
    }
}
