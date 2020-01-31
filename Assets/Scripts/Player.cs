using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float climbSpeed = 3f;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    float gravityAtStart;
    bool isAlive = true;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        gravityAtStart = myRigidBody.gravityScale;
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {

            Run();
            FlipSprite();
            Jump();
            ClimbLadders();
            Die();
        }
        else
        {
            myRigidBody.velocity *= 0.99f;
        }
    }


    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 and +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpHeight);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadders()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidBody.gravityScale = gravityAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return;
        }
        else
        {
            myRigidBody.gravityScale = 0f;
            float controlThrowV = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrowV * climbSpeed);
            myRigidBody.velocity = climbVelocity;

            bool playerIsClimbing = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("IsClimbing", playerIsClimbing);
        }
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            StartCoroutine(DeathSequence()); 
        }
    }

    IEnumerator DeathSequence()
    {
        myAnimator.SetTrigger("Die");
        GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-20f, 20f), Random.Range(20f, 30f));
        isAlive = false;
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        gameSession.ProcessPlayerDeath();
    }
}
