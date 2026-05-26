using UnityEngine;

public class ProgressionSystem : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int experience = 0;
    [SerializeField] private int expPerLevel = 100;

    private CharacterStats stats;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void GainExperience(int amount)
    {
        experience += amount;
        while (experience >= expPerLevel * currentLevel)
        {
            currentLevel++;
            if (stats) stats.GetComponent<CharacterStats>().GetMaxHealth();
            Debug.Log($"Level Up! Level {currentLevel}");
        }
    }

    public int GetLevel() => currentLevel;
}