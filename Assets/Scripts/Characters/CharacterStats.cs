using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] protected float maxHealth = 100f;
    protected float currentHealth;

    [Header("Stamina")]
    [SerializeField] protected float maxStamina = 100f;
    protected float currentStamina;
    [SerializeField] protected float staminaRegenRate = 20f;

    [Header("Combat")]
    [SerializeField] protected float baseDamage = 10f;
    [SerializeField] protected float armor = 0f;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float sprintSpeed = 8f;
    [SerializeField] protected float jumpForce = 5f;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    protected virtual void Update()
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + staminaRegenRate * Time.deltaTime);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage * (1f - armor / 100f);
        currentHealth = Mathf.Max(0, currentHealth);
    }

    public virtual bool UseStamina(float amount)
    {
        if (currentStamina >= amount) { currentStamina -= amount; return true; }
        return false;
    }

    public float GetHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public float GetStamina() => currentStamina;
    public float GetMaxStamina() => maxStamina;
    public float GetDamage() => baseDamage;
}