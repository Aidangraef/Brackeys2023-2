using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Vector3 movement;
    private bool facingRight;
    private Animator animator;

    public int speed;
    public bool inConversation;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = false;
        inConversation = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");

        // flip sprite
        if(!DialogueManager.IsConversationActive && 
            (movement.x > 0 && !facingRight || movement.x < 0 && facingRight))
        {
            Flip();
        }

        // walking animation
        if(!DialogueManager.IsConversationActive && 
                (Input.GetAxisRaw("Horizontal") >= 0.01 || Input.GetAxisRaw("Horizontal") <= -0.01))
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

    public void StartEndConversation()
    {
        //inConversation = !inConversation;
    }
}
