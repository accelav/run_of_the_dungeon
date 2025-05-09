
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    [SerializeField] float forwardSpeed = 5.0f;
    [SerializeField] float laneChangeSmooth = 10.0f;
    [SerializeField] float[] positions; // Las mismas que usa el jugador

    int currentLane = 1;
    float targetX;
    public bool isGrounded = true;

    Animator animator;
    Rigidbody rb;
    Vector3 newPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        targetX = positions[currentLane];
        transform.Rotate(0,180,0);
    }

    void FixedUpdate()
    {
        MoveForward();
        
        animator.SetBool("Jump", false);
    }

    void MoveForward()
    {
        newPosition = new Vector3(
            Mathf.Lerp(rb.position.x, targetX, laneChangeSmooth * Time.fixedDeltaTime),
            rb.position.y,
            rb.position.z + forwardSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(newPosition);
        animator.SetFloat("Moving", 1f);
    }

    public void TryJump()
    {

         animator.SetBool("Jump", true);

    }

    public void TryChangeLane()
    {
        float checkDistance = Mathf.Abs(positions[1] - positions[0]);

        // Intentar izquierda
        if (currentLane > 0)
        {
                ChangeLane(-1);    
        }
        // Intentar derecha
        else if (currentLane < positions.Length - 1)
        {
                ChangeLane(1);
        }
    }

    void ChangeLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, 0, positions.Length - 1);
        targetX = positions[currentLane];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            animator.SetBool("Jump", false);
        }

        if (collision.gameObject.CompareTag("Player"))
            {
            Destroy(gameObject);
        }
    }

}
