
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance; // Singleton para acceder a la instancia global
    public TextMeshProUGUI textoPuntos; // El texto de la UI donde se mostrará el puntaje
    public TextMeshProUGUI textoPuntos2; // El texto de la UI donde se mostrará el puntaje

    public TextMeshProUGUI tiempodeUI; // El texto de la UI donde se mostrará el Tiempo

    private float tiempoDePartida = 0.0f; // Que empieze de 0 el tiempo
    private float tiempoEspera = 6f; // Tiempo de espera ya que empieza el juego al segundo 6
    private float temporizador = 0f; //Hasta donde quiero que se ponga el tiempo de espera de 5 a 0 para que se empiece el tiempodepartida



    public TextMeshProUGUI textoMayorPuntuacion;// El texto de la UI donde se mostrará el mayor puntaje
    private int puntosTotales = 0; // Puntaje total del jugador
    private int mayorPuntuacion = 0; // Mayor puntuación registrada
                                     //  private int puntosPerdidos = 2; 
    private const string MayorPuntuacion = "MayorPuntuacion"; // Clave para almacenar la mayor puntuación en PlayerPrefs
    private const string PuntosGuardado = "Puntos"; // Clave para almacenar los puntos en PlayerPrefs
    private bool puntosDoblesActivos = false; // Si los puntos dobles están activos
    private float tiempoRestante = 0f; // Tiempo restante de los puntos dobles
    public bool levelStatus;

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
        // Asegúrate de que los textos de puntuación se actualicen al inicio
        // ActualizarPuntos();
        ActualizarMayorPuntuacion();
    }

    private void Update()
    {
        // Suma el tiempo desde que empezó el juego
        temporizador += Time.deltaTime;

        // Comienza a contar tiempoDePartida solo después de 6 segundos
        if (temporizador >= tiempoEspera)
        {
            tiempoDePartida += Time.deltaTime;
        }
        // Si los puntos dobles están activos, reducir el tiempo restante
        if (puntosDoblesActivos)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                // Desactivar puntos dobles cuando se acabe el tiempo
                puntosDoblesActivos = false;
            }
        }

        float minutos = Mathf.FloorToInt(tiempoDePartida / 60F);
        float segundos = Mathf.FloorToInt(tiempoDePartida % 60F);
        tiempodeUI.text = string.Format("{0:00}:{1:00}", minutos, segundos);
       




    }



    // Método para agregar puntos al puntaje total
    public void AgregarPuntos(int puntos)
    {
        puntosTotales += puntos;
        ActualizarPuntos();
        GuardarPuntos(); // Guardar el puntaje después de cada cambio

        // Verificar si el puntaje actual es mayor que la mayor puntuación
        if (puntosTotales > mayorPuntuacion)
        {
            mayorPuntuacion = puntosTotales;
            ActualizarMayorPuntuacion(); // Actualizar la mayor puntuación mostrada
            GuardarMayorPuntuacion(); // Guardar la nueva mayor puntuación
        }



    }

    // Actualiza el texto que muestra los puntos actuales
    private void ActualizarPuntos()
    {
        //  Debug.Log("llamado a actuguardarpuntos");
        textoPuntos.text = "Puntos: " + puntosTotales;
        textoPuntos2.text = "Puntos: " + puntosTotales;


    }

    public void LevelStatus(bool switchStatus)
    {
        levelStatus = switchStatus;
    }


    // Actualiza el texto que muestra la mayor puntuación
    private void ActualizarMayorPuntuacion()
    {
        textoMayorPuntuacion.text = "Mayor Puntuación: " + mayorPuntuacion;
    }

    // Guardar los puntos en PlayerPrefs
    private void GuardarPuntos()
    {
        //  Debug.Log("llamado a guardarpuntos");
        PlayerPrefs.SetInt(PuntosGuardado, puntosTotales); // Guardar el puntaje con la clave
        PlayerPrefs.Save(); // Guardar de manera permanente

    }

    // Guardar la mayor puntuación en PlayerPrefs
    private void GuardarMayorPuntuacion()
    {
        PlayerPrefs.SetInt(MayorPuntuacion, mayorPuntuacion); // Guardar la mayor puntuación con la clave
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
            mayorPuntuacion = PlayerPrefs.GetInt(MayorPuntuacion); // Recuperar la mayor puntuación guardada
        }
        else
        {
            mayorPuntuacion = 0; // Si no hay mayor puntuación guardada, ponerla en 0
        }
    }

    // Nuevo método para descontar puntos cuando la bala falla
    public void DescontarPuntos(int puntosPerdidos)
    {
        puntosTotales -= puntosPerdidos;
        // if (puntosTotales < 0) puntosTotales = 0; // Evita valores negativos
        ActualizarPuntos();
        GuardarPuntos();
    }



    // Opcional: Resetear los puntos y la mayor puntuación (por ejemplo, al comenzar una nueva partida)
    public void ResetearPuntos()
    {
        Debug.Log("llamado a rsetear");
        puntosTotales = 0;
        //mayorPuntuacion = 0;
        // GuardarPuntos(); // Guardar el puntaje reseteado
        GuardarMayorPuntuacion(); // Guardar la mayor puntuación reseteada
                                  // ActualizarPuntos();
        ActualizarMayorPuntuacion();

    }


    public void NextLevel()
    {
        SceneManager.LoadScene("Nivel2");
        // Debug.Log("llamado a nextlevel");
        // puntosTotales = 0;
        //mayorPuntuacion = 0;
        GuardarPuntos(); // Guardar el puntaje reseteado
        GuardarMayorPuntuacion(); // Guardar la mayor puntuación reseteada
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


