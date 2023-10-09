using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed = 5f;
    public bool isPlayerCanRun;
    public bool isPlayerRunning;
    public float runSpeed = 10f;
    public float currentStamina;
    public float staminaDecreaseRate = 5f;
    public float staminaRechargeRate = 5f;

    [Header("Debug")]
    [SerializeField] Vector2 moveInput;
    [SerializeField] Vector2 lastInput;
    Vector2 move;
    Rigidbody2D rb;
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = PlayerStats.instance.maxStamina;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PlayerStats.instance.isPlayerCanMove)
        {
            moveInput = InputManager.inputSystem.Player.Move.ReadValue<Vector2>();

            LastState();
        }
    }

    // Menggunakan FixedUpdate untuk pergerakan yang melibatkan fisika
    void FixedUpdate()
    {
        if (PlayerStats.instance.isPlayerCanMove)
        {
            float run_input_value = InputManager.inputSystem.Player.Run.ReadValue<float>();
            if (run_input_value > 0 && isPlayerCanRun)
            {
                currentStamina -= staminaDecreaseRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0f, PlayerStats.instance.maxStamina);
                if (currentStamina <= 0f)
                {
                    isPlayerCanRun = false;
                }

                move = moveInput * runSpeed * Time.fixedDeltaTime;
                SetMoveAnimationParameters(moveInput.x, moveInput.y, moveInput.sqrMagnitude, 0.2f * walkSpeed);
            }
            else
            {
                currentStamina += staminaRechargeRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0f, PlayerStats.instance.maxStamina);
                if (currentStamina >= PlayerStats.instance.maxStamina)
                {
                    isPlayerCanRun = true;
                }

                move = moveInput * walkSpeed * Time.fixedDeltaTime;

                SetMoveAnimationParameters(moveInput.x, moveInput.y, moveInput.sqrMagnitude, 0.2f * walkSpeed);
            }

            rb.MovePosition(rb.position + move);

        }
        else if (!PlayerStats.instance.isPlayerCanMove)
        {
            SetMoveAnimationParameters(0, 0, 0, 0);
        }
    }

    void LastState()
    {
        if (moveInput.magnitude > 0)
        {
            lastInput = moveInput;
        }
        SetIdleAnimationParameters(lastInput.x, lastInput.y);
    }

    private void SetMoveAnimationParameters(float horizontal, float vertical, float speed, float animSpeed)
    {
        animator.SetFloat("MoveHorizontal", horizontal);
        animator.SetFloat("MoveVertical", vertical);
        animator.SetFloat("Speed", speed);
        animator.SetFloat("AnimSpeed", animSpeed);
    }

    private void SetIdleAnimationParameters(float horizontal, float vertical)
    {
        animator.SetFloat("IdleHorizontal", horizontal);
        animator.SetFloat("IdleVertical", vertical);
    }
}