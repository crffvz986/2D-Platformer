using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //CONFIG
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumping = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] Vector2 death = new Vector2(25f, 25f);

    //STATE
    bool isAlive = true;
    //private bool m_FacingRight = true;

    //COMPONENT REFERENCES
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D bodyColider;
    BoxCollider2D feetColider;
    float gravityAtStart;


    //METHODS/FUNCTIONS
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        bodyColider = GetComponent<CapsuleCollider2D>();
        gravityAtStart = myRigidbody.gravityScale;
        feetColider = GetComponent<BoxCollider2D>();
    }

    void Update ()
    {
        if (!isAlive) { return; }

        Run();
        ClimbLadder();
        Jump();
        FlipSprite();
        Die();
        
	}

    void Run ()
    {
        float playerMovement = CrossPlatformInputManager.GetAxis("Horizontal"); // vrijednost je između -1 i +1 

        Vector2 playerVelocity = new Vector2(playerMovement * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHorizontalSpeed);
    }

    private void ClimbLadder()
    {
        if(!feetColider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody.gravityScale = gravityAtStart;
            return;
        }

        float playerClimb = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, playerClimb * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerVerticalSpeed);
    }

    private void Jump()
    {
        if(!feetColider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumping);
            myRigidbody.velocity += jumpVelocity;
        }
    }

    private void Die ()
    {
        if(bodyColider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = death;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    
    private void FlipSprite()
    {
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

       // m_FacingRight = !m_FacingRight;

		//transform.Rotate(0f, 180f, 0f);
    }
}
