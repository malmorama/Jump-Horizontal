using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controlls the player animation based if lands on a platform or at the top of the air on his way down
public class PlayerAnimation : MonoBehaviour
{
    public LayerMask platformLayerMask;
    private BoxCollider2D boxCollider2D;
    public Animator animator;
    private Rigidbody2D rb2d;
    //public AudioSource playerJumpSound;
    //public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(PlayerAnimationLoop());
         
    }

    // Update is called once per frame
    void Update()
    {
        //PlayPlayerAnimation();
    }

    private IEnumerator PlayerAnimationLoop()
    {
        while (true)
        {
            PlayPlayerAnimation();
            yield return new WaitForSeconds(0.01f); ;
        }
    }


    //check if player hit a layer mask
    private bool IsGrounded()
    {
        float extraHeight = 0.05f;
        //Vector2 centerPlayBox = new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y - 3f);
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;

        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + extraHeight), rayColor);
        //print(raycastHit.collider);
        return raycastHit.collider != null;
    }


    //play animation when player hits a platform and when at top pos in air and on way down
    private void PlayPlayerAnimation()
    {
       
        if (IsGrounded())
        {
            if (rb2d.velocity.y <= 0)
            {
                animator.SetBool("TouchGround", true);
                animator.SetBool("TopPosInAir", false);
                //playerJumpSound.Play();
                 
            }
        }
        if (!IsGrounded() && rb2d.velocity.y < 0)
        {
            animator.SetBool("TopPosInAir", true);
            animator.SetBool("TouchGround", false);
        }
    }
}
