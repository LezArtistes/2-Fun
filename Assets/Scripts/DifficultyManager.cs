using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("R�f�rences")]
    [SerializeField] private AsteroidPoolManager asteroidPoolManager;

    [Header("Param�tres de difficult�")]
    [SerializeField] private float difficultyIncreaseInterval = 10f; // Temps entre deux augmentations
    [SerializeField] private float asteroidSpeedIncrease = 0.5f; // +0.5 de vitesse toutes les 10s
    [SerializeField] private float minSpawnIntervalDecrease = 0.1f; // R�duit le d�lai min de spawn
    [SerializeField] private float maxSpawnIntervalDecrease = 0.1f; // R�duit le d�lai max de spawn
    [SerializeField] private float minSpawnIntervalLimit = 0.5f;
    [SerializeField] private float maxSpawnIntervalLimit = 1.0f;

    private float nextDifficultyTime;

    void Start()
    {
        // Nouvelle m�thode Unity (plus performante)
        if (asteroidPoolManager == null)
        {
            asteroidPoolManager = FindFirstObjectByType<AsteroidPoolManager>();
        }

        nextDifficultyTime = Time.time + difficultyIncreaseInterval;
    }

    void Update()
    {
        if (Time.time >= nextDifficultyTime)
        {
            IncreaseDifficulty();
            nextDifficultyTime = Time.time + difficultyIncreaseInterval;
        }
    }

    void IncreaseDifficulty()
    {
        // Augmente la vitesse
        asteroidPoolManager.AsteroidSpeed += asteroidSpeedIncrease;

        // R�duit les d�lais de spawn progressivement
        asteroidPoolManager.MinSpawnInterval = Mathf.Max(
            asteroidPoolManager.MinSpawnInterval - minSpawnIntervalDecrease,
            minSpawnIntervalLimit
        );

        asteroidPoolManager.MaxSpawnInterval = Mathf.Max(
            asteroidPoolManager.MaxSpawnInterval - maxSpawnIntervalDecrease,
            maxSpawnIntervalLimit
        );

    }
}
