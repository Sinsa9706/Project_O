using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 5;
    public float boostedSpeed = 10; // Shift Ű�� ������ ���� ���� �ӵ�
    public bool menuOpen = false;
    public Animator animator;
    private Camera _camera;

    Rigidbody2D rb;
    bool isBoosting = false; // Shift Ű�� ������ �ִ��� ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
    }
    private void Update()
    {
        // Shift Ű�� ������ �ִ��� ���� ����
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

    private void Move()
    {
        // Shift Ű�� ������ ������ boostedSpeed��, �ƴϸ� �⺻ speed�� ���
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
