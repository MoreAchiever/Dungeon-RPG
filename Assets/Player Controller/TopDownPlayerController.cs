using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    public Transform Map2EnteryPos; // Reference to the teleport position game object
    public Transform Map3EnteryPos;
    public Transform Map4EnteryPos;
    public Transform Map1EnteryPos;


    [SerializeField] float speed = 5f;
    [SerializeField] float runMultiplier = 1.5f;

    Rigidbody2D rb;
    Collider2D col;
    Animator anim;

    Vector2 moveVector;
    bool isGod = false;
    bool isSprinting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate() => rb.velocity = speed * moveVector;

    private void Update()
    {
        GetInput();
        SetAnimations();
    }

    private void GetInput()
    {
        if (Input.GetButtonDown("Debug Previous"))
        {
            isGod = !isGod;
            col.enabled = !isGod;            
        }

        isSprinting = Input.GetButton("Fire3");
      
        moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (isGod) moveVector *= 5f;
        if (isSprinting) moveVector *= runMultiplier;

    }

    private void SetAnimations()
    { 
        // If the player is moving
        if (moveVector != Vector2.zero)
        {
            // Trigger transition to moving state
            anim.SetBool("IsMoving", true);

            // Set X and Y values for Blend Tree
            anim.SetFloat("MoveX", moveVector.x);
            anim.SetFloat("MoveY", moveVector.y);
        }
        else
            anim.SetBool("IsMoving", false);
    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map2Entery")) // Replace "Teleport" with the tag of the specific position
        {
            // Teleport the player to the specified position
            transform.position = Map2EnteryPos.position;
        }

        else if (collision.gameObject.CompareTag("Map3Entery"))
        {
            transform.position = Map3EnteryPos.position;
        }

        else if (collision.gameObject.CompareTag("Map4Entery"))
        {
            transform.position = Map4EnteryPos.position;
        }

        else if (collision.gameObject.CompareTag("Map1Entery"))
        {
            transform.position = Map1EnteryPos.position;
        }

    }
}