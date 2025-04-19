using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed = 5.0f; //ESTA ES LA VELOCIDAD DEL JUGADOR
    [SerializeField] float laneChangeSmooth = 10.0f;
    [SerializeField] float[] positions;
    [SerializeField] float crashTime; //TIEMPO QUE VA HACIA ATRAS DESPUES DE CHOCAR

    int currentLane = 1;
    float targetX;
    

    bool saltando = false;
    bool chocando = false;

    Animator animator;
    Rigidbody rb;

    Vector3 newPosition;
    float time;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            chocando = true;
            Debug.Log("Ha chocado, pierde una vida");
        }
    }
}
