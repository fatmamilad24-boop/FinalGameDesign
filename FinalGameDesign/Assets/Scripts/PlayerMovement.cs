using UnityEngine;

public class XControl : MonoBehaviour
{
    public float x = 0f;
    public float jumpForce = 5f;
    public float jumpCooldown = 1f; // Time in seconds between jumps

    private Rigidbody rb;
    private Animator anim;
    private float nextJumpTime = 0f; // Time when the player can jump again

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Move on X
        if (Input.GetKeyDown(KeyCode.A))
            x -= 5f;

        if (Input.GetKeyDown(KeyCode.D))
            x += 5f;

        // Clamp x between -5 and 5
        x = Mathf.Clamp(x, -5f, 5f);

        // Apply position
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        // Jump with cooldown
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextJumpTime)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (anim != null)
                anim.SetTrigger("Jump");

            // Set next allowed jump time
            nextJumpTime = Time.time + jumpCooldown;
        }
    }
}
