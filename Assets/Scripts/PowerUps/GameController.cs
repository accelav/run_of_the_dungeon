using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance; // Singleton para acceder a la instancia global
    public TextMeshProUGUI textoPuntos; // El texto de la UI donde se mostrar� el puntaje
    public TextMeshProUGUI textoPuntos2; // El texto de la UI donde se mostrar� el puntaje

    public TextMeshProUGUI textoMayorPuntuacion;// El texto de la UI donde se mostrar� el mayor puntaje
    private int puntosTotales = 0; // Puntaje total del jugador
    private int mayorPuntuacion = 0; // Mayor puntuaci�n registrada
                                     //  private int puntosPerdidos = 2; 
    private const string MayorPuntuacion = "MayorPuntuacion"; // Clave para almacenar la mayor puntuaci�n en PlayerPrefs
    private const string PuntosGuardado = "Puntos"; // Clave para almacenar los puntos en PlayerPrefs
    private bool puntosDoblesActivos = false; // Si los puntos dobles est�n activos
    private float tiempoRestante = 0f; // Tiempo restante de los puntos dobles


    private void Awake()
    {
        // Asegurarse de que solo haya una instancia del controlador de juego
        if (instance == null)
        {
            instance = this;

            // DontDestroyOnLoad(gameObject);  // Si quieres que el objeto no se destruya al cambiar de escena

        }
        else
        {
            Destroy(gameObject);

        }
        //ResetearPuntos();
        // Cargar el puntaje guardado al iniciar el juego
        CargarPuntos();

    }

    void Start()
    {
        //puntosTotales = 0;
        ResetearPuntos();
        // Aseg�rate de que los textos de puntuaci�n se actualicen al inicio
        // ActualizarPuntos();
        ActualizarMayorPuntuacion();
    }

    private void Update()
    {

        // Si los puntos dobles est�n activos, reducir el tiempo restante
        if (puntosDoblesActivos)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                // Desactivar puntos dobles cuando se acabe el tiempo
                puntosDoblesActivos = false;
            }
        }

    }



    // M�todo para agregar puntos al puntaje total
    public void AgregarPuntos(int puntos)
    {
        puntosTotales += puntos;
        ActualizarPuntos();
        GuardarPuntos(); // Guardar el puntaje despu�s de cada cambio

        // Verificar si el puntaje actual es mayor que la mayor puntuaci�n
        if (puntosTotales > mayorPuntuacion)
        {
            mayorPuntuacion = puntosTotales;
            ActualizarMayorPuntuacion(); // Actualizar la mayor puntuaci�n mostrada
            GuardarMayorPuntuacion(); // Guardar la nueva mayor puntuaci�n
        }



    }

    // Actualiza el texto que muestra los puntos actuales
    private void ActualizarPuntos()
    {
        //  Debug.Log("llamado a actuguardarpuntos");
        textoPuntos.text = "Puntos: " + puntosTotales;
        textoPuntos2.text = "Puntos: " + puntosTotales;

    }

    // Actualiza el texto que muestra la mayor puntuaci�n
    private void ActualizarMayorPuntuacion()
    {
        textoMayorPuntuacion.text = "Mayor Puntuaci�n: " + mayorPuntuacion;
    }

    // Guardar los puntos en PlayerPrefs
    private void GuardarPuntos()
    {
        //  Debug.Log("llamado a guardarpuntos");
        PlayerPrefs.SetInt(PuntosGuardado, puntosTotales); // Guardar el puntaje con la clave
        PlayerPrefs.Save(); // Guardar de manera permanente

    }

    // Guardar la mayor puntuaci�n en PlayerPrefs
    private void GuardarMayorPuntuacion()
    {
        PlayerPrefs.SetInt(MayorPuntuacion, mayorPuntuacion); // Guardar la mayor puntuaci�n con la clave
        PlayerPrefs.Save(); // Guardar de manera permanente
    }

    // Cargar los puntos desde PlayerPrefs
    private void CargarPuntos()
    {
        if (PlayerPrefs.HasKey(PuntosGuardado))
        {
            puntosTotales = PlayerPrefs.GetInt(PuntosGuardado); // Recuperar el puntaje guardado
        }
        else
        {
            puntosTotales = 0; // Si no hay puntos guardados, poner el puntaje en 0
        }

        if (PlayerPrefs.HasKey(MayorPuntuacion))
        {
            mayorPuntuacion = PlayerPrefs.GetInt(MayorPuntuacion); // Recuperar la mayor puntuaci�n guardada
        }
        else
        {
            mayorPuntuacion = 0; // Si no hay mayor puntuaci�n guardada, ponerla en 0
        }
    }

    // Nuevo m�todo para descontar puntos cuando la bala falla
    public void DescontarPuntos(int puntosPerdidos)
    {
        puntosTotales -= puntosPerdidos;
        // if (puntosTotales < 0) puntosTotales = 0; // Evita valores negativos
        ActualizarPuntos();
        GuardarPuntos();
    }



    // Opcional: Resetear los puntos y la mayor puntuaci�n (por ejemplo, al comenzar una nueva partida)
    public void ResetearPuntos()
    {
        Debug.Log("llamado a rsetear");
        puntosTotales = 0;
        //mayorPuntuacion = 0;
        // GuardarPuntos(); // Guardar el puntaje reseteado
        GuardarMayorPuntuacion(); // Guardar la mayor puntuaci�n reseteada
                                  // ActualizarPuntos();
        ActualizarMayorPuntuacion();

    }


    public void NextLevel()
    {
        // Debug.Log("llamado a nextlevel");
        // puntosTotales = 0;
        //mayorPuntuacion = 0;
        GuardarPuntos(); // Guardar el puntaje reseteado
        GuardarMayorPuntuacion(); // Guardar la mayor puntuaci�n reseteada
        ActualizarPuntos();
        ActualizarMayorPuntuacion();
    }




    /*
    public void NuevaPartida()
    {
        GameController.instance.ReiniciarPuntos(); // Reiniciar puntos al comenzar nueva partida
    }
    void Start()
{
    GameController.instance.ReiniciarPuntos(); // Reiniciar puntos al iniciar la partida
}
    */
}


