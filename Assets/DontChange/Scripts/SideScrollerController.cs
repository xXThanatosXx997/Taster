using UnityEngine;
using UnityEngine.SceneManagement;

public class SideScrollerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 5f;       // Movement speed
    public float jumpForce = 10f;     // Jumping force
    public int maxHealth = 3;         // Maximum health
    public LayerMask groundLayer;     // Layer mask for the ground

    private int currentHealth;        // Current health
    private Rigidbody2D rb;           // Player's Rigidbody2D
    private Animator animator;        // Player's Animator
    private bool isGrounded;          // Check if player is on the ground

    [Header("Ground Check")]
    public Transform groundCheck;     // Position to check for ground
    public float groundCheckRadius = 0.2f; // Radius of ground check circle
    public int coins;

    void Start()
    {
        // Initialize components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    //
    void Update()
    {
        // Handle movement
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // Handle facing direction
        if (move > 0)
            transform.localScale = new Vector3(2, 2, 2);
        else if (move < 0)
            transform.localScale = new Vector3(-2, 2, 2);

        // Check for ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        Debug.Log(isGrounded);

        // Handle jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(move)); // Speed for walk/idle
        animator.SetBool("IsJumping", !isGrounded); // IsJumping for jump
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for damage tag
        if (collision.gameObject.CompareTag("Damage"))
        {
            TakeDamage(1);
        }

        // Check for death tag
        if (collision.gameObject.CompareTag("Death"))
        {
            RestartScene();
        }

        // Check for victory tag
        if (collision.gameObject.CompareTag("Victory"))
        {
            GoToVictoryScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
        }
    }
    
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Health: " + currentHealth);

        // Check if health reaches zero
        if (currentHealth <= 0)
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GoToVictoryScene()
    {
        // Load the first scene (index 0)
        SceneManager.LoadScene(0);
    }

    private void OnDrawGizmos()
    {
        // Visualize the ground check radius in the editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
