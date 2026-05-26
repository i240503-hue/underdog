using UnityEngine;

public class EnemyController : CharacterStats
{
    private Rigidbody rb;
    private AIBrain aiBrain;
    private PlayerController player;
    private Vector3 moveDirection;
    private bool isGrounded;
    private float lastAttackTime;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        aiBrain = GetComponent<AIBrain>();
        player = FindObjectOfType<PlayerController>();
        if (rb) rb.freezeRotation = true;
    }

    protected override void Update()
    {
        base.Update();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        
        if (currentHealth <= 0) return;
        if (player && aiBrain) aiBrain.UpdateAI(player, this);
        
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager) uiManager.UpdateEnemyHealth(currentHealth, maxHealth);
    }

    private void FixedUpdate()
    {
        if (rb == null || currentHealth <= 0) return;
        rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        
        if (moveDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    public void SetMoveDirection(Vector3 dir) => moveDirection = dir;

    public void PerformAttack(float staminaCost)
    {
        if (!isGrounded || !UseStamina(staminaCost) || Time.time - lastAttackTime < 0.5f) return;
        
        float damage = baseDamage * (staminaCost / 10f);
        Debug.Log($"Enemy Attack: {damage:F1} DMG");
        lastAttackTime = Time.time;
    }

    public Vector3 GetPosition() => transform.position;
}