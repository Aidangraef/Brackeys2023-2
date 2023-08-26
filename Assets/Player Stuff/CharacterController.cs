using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Vector3 movement;
    private bool facingRight;
    private Animator animator;
    bool isDiving = false;

    public int speed;

    public bool FacingRight { get => facingRight; set => facingRight = value; }
    public bool IsDiving { get => isDiving; set => isDiving = value; }

    void Awake()
    {
        facingRight = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Ignores update if into diving transition
        if (IsDiving) {
            return;
        }

        movement.x = PlayerHoldingBothDirections() ? 0f : Input.GetAxis("Horizontal");

        // flip sprite
        if(!DialogueManager.IsConversationActive && 
            (movement.x > 0 && !facingRight || movement.x < 0 && facingRight))
        {
            Flip();
        }

        // walking animation
        if(!DialogueManager.IsConversationActive && 
            (Input.GetAxisRaw("Horizontal") >= Mathf.Epsilon || Input.GetAxisRaw("Horizontal") <= -Mathf.Epsilon))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if(DialogueManager.IsConversationActive)
        {
            return;
        }
        transform.position += movement * Time.fixedDeltaTime * speed;
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x = theScale.x * -1;
        transform.localScale = theScale;
    }

    bool PlayerHoldingBothDirections() {
        return (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow));
    }
}
