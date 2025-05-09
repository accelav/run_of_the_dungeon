using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;  
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float forwardSpeed = 5.0f; //ESTA ES LA VELOCIDAD DEL JUGADOR
    [SerializeField] float laneChangeSmooth = 10.0f;
    [SerializeField] float[] positions;
    [SerializeField] float crashTime; //TIEMPO QUE VA HACIA ATRAS DESPUES DE CHOCAR
    public static PlayerController instance;

    int currentLane = 1;
    float targetX;
    

    bool saltando = false;
    bool chocando = false;

    Animator animator;
    Rigidbody rb;

    Vector3 newPosition;
    float time;



    // PowerUp Iman

    [SerializeField] private float radioIman = 5f;
    [SerializeField] private float velocidadAtraccion = 10f;
    private bool imanActivo = false;

    // Referencia a la imagen del Iman en la UI
    public Image ImanIcono;

    // Puntos Dobles

    private bool puntosDoblesActivos = false;
    public bool PuntosDoblesActivos => puntosDoblesActivos;

    // Referencia a la imagen del PuntosDobles en la UI
    public Image PuntosDoblesIcono;

    //Escudo

    private bool escudoActivo = false;
    public bool EscudoActivo => escudoActivo; // Para que otros scripts lo lean

    // Referencia a la imagen del escudo en la UI
    public Image escudoIcono;

    void Start()
    {     
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        targetX = positions[currentLane];
    }

    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        if (input.x < 0)
        {
            ChangeLane(-1);
        }
        else if (input.x > 0)
        {
            ChangeLane(1);
        }
    }
    public void OnJump(InputValue value)
    {
        saltando = value.isPressed;
        
    }
    void ChangeLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, 0, positions.Length - 1);

        targetX = positions[currentLane];
    }

    void FixedUpdate()
    {
        float newX = Mathf.Lerp(rb.position.x, targetX, laneChangeSmooth * Time.fixedDeltaTime);
        

        

        if (chocando)
        {
            time += Time.deltaTime;
            newPosition = new Vector3(newX, rb.position.y, rb.position.z - forwardSpeed/2 * Time.fixedDeltaTime);
            if (time >= crashTime)
            {
                chocando = false;
                time = 0f;
            }
        }
        else if (!chocando)
        {
            newPosition = new Vector3(newX, rb.position.y, rb.position.z + forwardSpeed * Time.fixedDeltaTime);
        }

        rb.MovePosition(newPosition);

        animator.SetFloat("Moving", 1f);
        animator.SetBool("Jump", saltando);
        saltando = false;
    }


    public void ActivarIman(float duracion)
    {
        StartCoroutine(ImanTemporal(duracion));
    }

    private IEnumerator ImanTemporal(float duracion)
    {
        imanActivo = true;
        Debug.Log("Imán activado");

        float tiempo = 0f;

        if (ImanIcono != null)
        {
            ImanIcono.gameObject.SetActive(true);
        }


        // Aquí, cada vez que se repite el bucle, incrementamos el tiempo
        while (tiempo < duracion)
        {
            AtraerMonedas();
            tiempo += Time.deltaTime;

            // Esto puede ser un efecto visual que se actualiza constantemente
            yield return null;
        }

        imanActivo = false;
        Debug.Log("Imán desactivado");

        if (ImanIcono != null)
        {
            ImanIcono.gameObject.SetActive(false);
        }
    }

    private void AtraerMonedas()
    {
        Collider[] monedas = Physics.OverlapSphere(transform.position, radioIman);
        foreach (var moneda in monedas)
        {
            if (moneda.CompareTag("Star"))
            {
                Transform monedaTransform = moneda.transform;
                monedaTransform.position = Vector3.MoveTowards(monedaTransform.position, transform.position, velocidadAtraccion * Time.deltaTime);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioIman);
    }

    public void ActivarPuntosDobles(float duracion)
    {
        StartCoroutine(PuntosDoblesTemporal(duracion));
    }

    private IEnumerator PuntosDoblesTemporal(float duracion)
    {
        puntosDoblesActivos = true;
        Debug.Log("Puntos dobles ACTIVADOS");

        if (PuntosDoblesIcono != null)
        {
            PuntosDoblesIcono.gameObject.SetActive(true);
        }


        // Esperar durante el tiempo especificado (por ejemplo, 5 segundos)
        yield return new WaitForSeconds(duracion);

        puntosDoblesActivos = false;
        Debug.Log("Puntos dobles DESACTIVADOS");

        if (PuntosDoblesIcono != null)
        {
            PuntosDoblesIcono.gameObject.SetActive(false);
        }
    }

    public void ActivarEscudo(float duracion)
    {
        StartCoroutine(EscudoTemporal(duracion));
    }

    private IEnumerator EscudoTemporal(float duracion)
    {
        escudoActivo = true;
        Debug.Log("ESCUDO ACTIVADO");
        // Activar el ícono del escudo en la UI
        if (escudoIcono != null)
        {
            escudoIcono.gameObject.SetActive(true);
        }

        // Esperar durante el tiempo especificado (por ejemplo, 5 segundos)
        yield return new WaitForSeconds(duracion);

        escudoActivo = false;
        Debug.Log("ESCUDO DESACTIVADO");
        // Desactivar el ícono del escudo en la UI
        if (escudoIcono != null)
        {
            escudoIcono.gameObject.SetActive(false);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Jumpable") || collision.gameObject.CompareTag("Enemy"))
        {
            SoundsBehaviour.instance.PlayClip(3);
            chocando = true;

            if (escudoActivo)
            {
                Debug.Log("¡Golpe bloqueado por el escudo!");
                return; // Si el escudo está activo, no se hace nada más
            }

            SistemaDeVidas.instance.PerdidaVida();  // Si el escudo no está activo, se pierde vida
        }
        else if (collision.gameObject.CompareTag("Finish"))
        {
            PauseWinLoseBehaviour.instance.Winner();  // Si choca con el Finish, se llama al ganador
        }
    }
}
