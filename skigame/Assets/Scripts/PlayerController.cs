using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode leftInput, rightInput;
    [SerializeField] private float acceleration = 100, turnspeed = 100, miniSpeed = 0, maxSpeed = 500, minAcceleration = 100, maxAcceleration = 200;
    private float speed = 0;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
       {
           float angle = Mathf.Abs (transform.eulerAngles.y -180);
           acceleration = Remap(0, 90, maxAcceleration, minAcceleration, angle);
            speed += acceleration * Time.fixedDeltaTime;
            speed = Mathf.Clamp(speed, miniSpeed, maxSpeed);
            animator.SetFloat("playerSpeed", speed);
            Vector3 velocity = transform.forward * speed*Time.fixedDeltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
       }

    private float Remap(float oldMin, float oldMax, float newMin, float newMax, float oldValue)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newValue = (((oldValue - oldMin) / oldRange) * newRange + newMin);
        return newValue;
    }
    void Update()
    {
        bool isGrounded = Physics.Linecast(transform.position, groundTransform.position, groundLayers);
        if (isGrounded)
        {
            if (Input.GetKey(leftInput) && transform.eulerAngles.x < 269)
            {
                transform.Rotate(new Vector3(0, turnspeed * Time.deltaTime, 0), Space.Self);
            }

            if (Input.GetKey(rightInput)&& transform.eulerAngles.x > 91)
            {
                transform.Rotate(new Vector3(0, -turnspeed * Time.deltaTime, 0), Space.Self);
            }
        }
    }
}
