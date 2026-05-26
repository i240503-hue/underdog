using UnityEngine;

public class PlayerController : CharacterStats
{
    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;
    private int comboCounter;
    private float lastAttackTime;

    private const float GROUND_CHECK_DIST = 0.1f;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        if (rb) rb.freezeRotation = true;
    }

    protected override void Update()
    {
        base.Update();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, GROUND_CHECK_DIST);
        
        if (GameManager.Instance?.GetGameState() != GameManager.GameState.Playing) return;

        Vector3 input = InputManager.Instance.GetMovementInput();
        moveDirection = input;

        if (InputManager.Instance.IsJumping() && isGrounded) Jump();
        if (InputManager.Instance.IsLightAttacking()) Attack(10f);
        if (InputManager.Instance.IsHeavyAttacking()) Attack(25f);

        UIManager ui = FindObjectOfType<UIManager>();
        if (ui)
        {
            ui.UpdatePlayerHealth(currentHealth, maxHealth);
            ui.UpdateStamina(currentStamina, maxStamina);
        }
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        
        if (moveDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Attack(float staminaCost)
    {
        if (!isGrounded || !UseStamina(staminaCost)) return;
        
        if (Time.time - lastAttackTime > 1.5f) comboCounter = 0;
        comboCounter++;
        comboCounter = Mathf.Min(3, comboCounter);
        lastAttackTime = Time.time;

        float damage = baseDamage * (staminaCost / 10f) * (1f + (comboCounter - 1) * 0.25f);
        Debug.Log($"Player Attack: {damage:F1} DMG (Combo x{comboCounter})");
    }
}