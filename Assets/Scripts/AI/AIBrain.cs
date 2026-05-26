using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private float detectionRange = 20f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float blockChance = 0.3f;

    private EnemyController enemyController;
    private float stateTimer;

    private enum AIState { Idle, Chase, Attack }
    private AIState currentState;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void UpdateAI(PlayerController player, EnemyController controller)
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        stateTimer -= Time.deltaTime;

        if (distance > detectionRange)
        {
            currentState = AIState.Idle;
        }
        else if (distance < attackRange)
        {
            currentState = stateTimer > 0 ? currentState : (Random.value < blockChance ? AIState.Idle : AIState.Attack);
            if (stateTimer <= 0) stateTimer = 2f;
        }
        else
        {
            currentState = AIState.Chase;
        }

        ExecuteState(player, distance);
    }

    private void ExecuteState(PlayerController player, float distance)
    {
        switch (currentState)
        {
            case AIState.Idle:
                enemyController.SetMoveDirection(Vector3.zero);
                break;
            case AIState.Chase:
                Vector3 direction = (player.transform.position - transform.position).normalized;
                enemyController.SetMoveDirection(direction);
                break;
            case AIState.Attack:
                enemyController.PerformAttack(Random.value > 0.6f ? 25f : 10f);
                break;
        }
    }
}