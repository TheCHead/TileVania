using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D myRigidBody;
    BoxCollider2D searchGroundCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        searchGroundCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndFlipSprite();
    }


    private void MoveAndFlipSprite()
    {
        if (!searchGroundCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
            moveSpeed *= -1;
        }
        myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }
}
