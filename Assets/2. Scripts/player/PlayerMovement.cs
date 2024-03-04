using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 5;
    public float boostedSpeed = 10; // Shift 키를 누르고 있을 때의 속도
    public bool menuOpen = false;
    public Animator animator;
    private Camera _camera;

    Rigidbody2D rb;
    bool isBoosting = false; // Shift 키를 누르고 있는지 여부

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
    }
    private void Update()
    {
        // Shift 키를 누르고 있는지 여부 감지
        if (Keyboard.current.shiftKey.isPressed)
        {
            isBoosting = true;
        }
        else
        {
            isBoosting = false;
        }
    }

    private void FixedUpdate()
    {
        Move();
        StartAnimation();
    }

    private void OnMove(InputValue value)
    {
        if (!menuOpen)
        {
            inputVec = value.Get<Vector2>();
        }
    }

    private void OnClick()
    {
        MainSoundManager.instance.PlaySFX(8);
    }

    private void Move()
    {
        // Shift 키를 누르고 있으면 boostedSpeed를, 아니면 기본 speed를 사용
        float currentSpeed = isBoosting ? boostedSpeed : speed; 
        Vector2 nextVec = inputVec.normalized * currentSpeed * Time.fixedDeltaTime;
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
