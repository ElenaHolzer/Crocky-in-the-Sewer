using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string WALK_ANIMATION = "Walk";

    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    private string ENEMY_TAG = "Enemy";

    GameObject button;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Buttons");
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {
        if (movementX < 0)
        {
            sr.flipX = true;
        }
        else if (movementX > 0)
        {
            sr.flipX = false;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }

        // walk to right side
        if (movementX > 0 && isGrounded == true)
        {
            anim.SetBool(WALK_ANIMATION, true);
        }
        // walk to left side
        else if (movementX < 0 && isGrounded == true)
        {
            anim.SetBool(WALK_ANIMATION, true);
        }
        // idle (movementX = 0)
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
        // jump animation ASCENDING
        if (isGrounded == false && myBody.velocity.y > 0)
        {
            anim.SetBool("Ascending", true);
        }
        else
        {
            anim.SetBool("Ascending", false);
        }
        // jump animation DESCENDING
        if (isGrounded == false && myBody.velocity.y < 0)
        {
            anim.SetBool("Descending", true);
        }
        else
        {
            anim.SetBool("Descending", false);
        }
    }

    void PlayerJump ()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if(collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
            button.SetActive(true);
        }
    }

    











} //class


















