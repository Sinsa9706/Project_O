using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 5;
    public bool menuOpen = false;
    public Animator animator;
    private Camera _camera;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        Move();
        StartAnimation();
    }

    private void OnMove(InputValue value)
    {
        if(menuOpen)
        {

        }
        else
        {
            inputVec = value.Get<Vector2>();
        }
        
    }

    private void Move()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec);
    }

    private void StartAnimation()
    {
        if(inputVec.x == 0 && inputVec.y == 0)
        {
            animator.SetBool("IsMove", false);
        }
        else
        {
            animator.SetBool("IsMove", true);
        }

        animator.SetFloat("inputX", inputVec.x);
        animator.SetFloat("inputY", inputVec.y);
    }
}
