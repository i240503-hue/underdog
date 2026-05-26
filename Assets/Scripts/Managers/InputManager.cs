using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public Vector3 GetMovementInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        return new Vector3(x, 0, z).normalized;
    }

    public bool IsJumping() => Input.GetKeyDown(KeyCode.Space);
    public bool IsSprinting() => Input.GetKey(KeyCode.LeftShift);
    public bool IsLightAttacking() => Input.GetMouseButtonDown(0);
    public bool IsHeavyAttacking() => Input.GetMouseButtonDown(1);
    public bool IsBlocking() => Input.GetKey(KeyCode.Q);
    public bool IsSpecialAttacking() => Input.GetKeyDown(KeyCode.E);
}