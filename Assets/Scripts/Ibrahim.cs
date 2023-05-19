using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ibrahim : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer sr;
    public float horizontalInput;
    public bool left;
    public bool right;
    public bool walk;
    public bool jump = false;
    public bool unleashed = false;
    private float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && !jump && !unleashed)
        {
            jump = true;
            rb.AddForce(Vector2.up, ForceMode2D.Impulse);
        }

        if (!unleashed)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed * horizontalInput);
        }

        if(horizontalInput != 0)
        {
            walk = true;
            if(horizontalInput > 0)
            {
                right = true;
                left = false;
            }
            else
            {
                right = false;
                left = true;
            }
        }
        else
        {
            walk = false;
            right = true;
            left = false;
        }

        animator.SetBool("walk", walk);
        animator.SetBool("jump", jump);
        animator.SetBool("unleashed", unleashed);

        if (!unleashed && Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(Unleash());
        }

        if(left)
        {
            sr.flipX = true;
        }
        else if (right)
        {
            sr.flipX = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            jump = false;
        }
    }
    
    public IEnumerator Unleash()
    {
        unleashed = true;
        yield return new WaitForSeconds(1.75f);
        unleashed = false;
    }
}
