using System.Collections.Generic;
using UnityEngine;

public class AsteroidPoolManager : MonoBehaviour
{
    [Header("Asteroid Types")]
    [SerializeField] private Sprite smallAsteroidSprite;
    [SerializeField] private Sprite mediumAsteroidSprite;
    [SerializeField] private Sprite largeAsteroidSprite;

    [Header("Asteroid Sizes")]
    [SerializeField] private Vector3 smallAsteroidScale = new Vector3(0.5f, 0.5f, 1f);
    [SerializeField] private Vector3 mediumAsteroidScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private Vector3 largeAsteroidScale = new Vector3(1.5f, 1.5f, 1f);
    [SerializeField] private bool enableScaleVariation = true;
    [SerializeField][Range(0f, 0.5f)] private float scaleVariation = 0.2f;

    [Header("Spawn Probabilities (%)")]
    [SerializeField][Range(0, 100)] private float smallAsteroidWeight = 60f;
    [SerializeField][Range(0, 100)] private float mediumAsteroidWeight = 30f;
    [SerializeField][Range(0, 100)] private float largeAsteroidWeight = 10f;

    [Header("Pool Settings")]
    [SerializeField] private int poolSize = 30;

    [Header("Spawn Settings")]
    [SerializeField] private int columnCount = 6;
    [SerializeField] private float columnWidth = 2f;
    [SerializeField] private float spawnHeight = 10f;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float asteroidSpeed = 3f;
    [SerializeField] private float despawnHeight = -10f;

    [Header("Difficulty Settings")]
    [SerializeField] private int minAsteroidsPerLine = 1;
    [SerializeField] private int maxAsteroidsPerLine = 3;

    [Header("Variation Settings")]
    [SerializeField] private float minSpawnInterval = 1.5f;
    [SerializeField] private float maxSpawnInterval = 3f;
    [SerializeField] private bool enableRotation = true;
    [SerializeField] private float minRotationSpeed = -100f;
    [SerializeField] private float maxRotationSpeed = 100f;

    private Queue<GameObject> pool;
    private List<GameObject> activeAsteroids;
    private Dictionary<GameObject, float> asteroidRotationSpeeds;
    private float nextSpawnTime;
    private float leftBound;

    void Start()
    {
        InitializePool();
        CalculateBounds();
        nextSpawnTime = Time.time + spawnInterval;
    }

    void InitializePool()
    {
        pool = new Queue<GameObject>();
        activeAsteroids = new List<GameObject>();
        asteroidRotationSpeeds = new Dictionary<GameObject, float>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = CreateAsteroidObject();
            pool.Enqueue(obj);
        }
    }

    void CalculateBounds()
    {
        float totalWidth = columnCount * columnWidth;
        leftBound = -totalWidth / 2f + columnWidth / 2f;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnAsteroidLine();
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            nextSpawnTime = Time.time + interval;
        }

        MoveAsteroids();
        CheckDespawn();
    }

    void SpawnAsteroidLine()
    {
        int asteroidsToSpawn = Random.Range(minAsteroidsPerLine, maxAsteroidsPerLine + 1);
        List<int> availableColumns = new List<int>();
        for (int i = 0; i < columnCount; i++)
            availableColumns.Add(i);

        for (int i = 0; i < asteroidsToSpawn && availableColumns.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, availableColumns.Count);
            int selectedColumn = availableColumns[randomIndex];
            availableColumns.RemoveAt(randomIndex);

            GameObject asteroid = GetFromPool();
            if (asteroid != null)
            {
                float xPos = leftBound + (selectedColumn * columnWidth);
                asteroid.transform.position = new Vector3(xPos, spawnHeight, 0f);

                if (enableRotation)
                {
                    asteroid.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                    float rotSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                    asteroidRotationSpeeds[asteroid] = rotSpeed;
                }
                else
                {
                    asteroid.transform.rotation = Quaternion.identity;
                }

                asteroid.SetActive(true);
                asteroid.GetComponent<Collider2D>().isTrigger = true;
                asteroid.transform.localScale = Vector3.one;
                activeAsteroids.Add(asteroid);
            }
        }
    }

    GameObject CreateAsteroidObject()
    {
        GameObject obj = new GameObject("Asteroid");
        obj.AddComponent<SpriteRenderer>();

        // Ajoute un collider circulaire
        CircleCollider2D collider = obj.AddComponent<CircleCollider2D>();
        collider.isTrigger = true; // On le met en trigger pour OnTriggerEnter2D

        // Ajoute le script de gestion des collisions
        obj.AddComponent<AsteroidCollision>();

        obj.SetActive(false);
        obj.transform.parent = transform;
        return obj;
    }

    GameObject GetFromPool()
    {
        GameObject obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = CreateAsteroidObject(); // On utilise la même fonction
        }

        SetSpecificsOnAsteroidSize(obj);
        return obj;
    }

    void SetSpecificsOnAsteroidSize(GameObject asteroid)
    {
        SpriteRenderer spriteRenderer = asteroid.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;

        CircleCollider2D circleCollider = asteroid.GetComponent<CircleCollider2D>();
        if (circleCollider == null) return;

        float totalWeight = smallAsteroidWeight + mediumAsteroidWeight + largeAsteroidWeight;
        float randomValue = Random.Range(0f, totalWeight);

        Vector3 baseScale;

        if (randomValue < smallAsteroidWeight)
        {
            spriteRenderer.sprite = smallAsteroidSprite;
            baseScale = smallAsteroidScale;
            circleCollider.radius = 0.5f;
        }
        else if (randomValue < smallAsteroidWeight + mediumAsteroidWeight)
        {
            spriteRenderer.sprite = mediumAsteroidSprite;
            baseScale = mediumAsteroidScale;
            circleCollider.radius = 1f;
        }
        else
        {
            spriteRenderer.sprite = largeAsteroidSprite;
            baseScale = largeAsteroidScale;
            circleCollider.radius = 1.5f;
        }

        // Ajout : variation aléatoire de la taille (optionnelle)
        if (enableScaleVariation)
        {
            float variation = Random.Range(1f - scaleVariation, 1f + scaleVariation);
            asteroid.transform.localScale = baseScale * variation;
        }
        else
        {
            asteroid.transform.localScale = baseScale;
        }
    }

    void MoveAsteroids()
    {
        foreach (GameObject asteroid in activeAsteroids)
        {
            if (asteroid.activeSelf)
            {
                asteroid.transform.position += Vector3.down * asteroidSpeed * Time.deltaTime;

                if (enableRotation && asteroidRotationSpeeds.ContainsKey(asteroid))
                {
                    float rotSpeed = asteroidRotationSpeeds[asteroid];
                    asteroid.transform.Rotate(0f, 0f, rotSpeed * Time.deltaTime);
                }
            }
        }
    }

    void CheckDespawn()
    {
        for (int i = activeAsteroids.Count - 1; i >= 0; i--)
        {
            GameObject asteroid = activeAsteroids[i];

            if (asteroid.transform.position.y < despawnHeight)
            {
                asteroid.SetActive(false);
                activeAsteroids.RemoveAt(i);

                if (asteroidRotationSpeeds.ContainsKey(asteroid))
                    asteroidRotationSpeeds.Remove(asteroid);

                pool.Enqueue(asteroid);
            }
        }
    }

    public void ReturnToPool(GameObject asteroid)
    {
        if (activeAsteroids.Contains(asteroid))
        {
            asteroid.SetActive(false);
            activeAsteroids.Remove(asteroid);

            if (asteroidRotationSpeeds.ContainsKey(asteroid))
                asteroidRotationSpeeds.Remove(asteroid);

            pool.Enqueue(asteroid);
        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            CalculateBounds();

        Gizmos.color = Color.yellow;
        for (int i = 0; i < columnCount; i++)
        {
            float xPos = leftBound + (i * columnWidth);
            Gizmos.DrawLine(
                new Vector3(xPos, spawnHeight, 0f),
                new Vector3(xPos, despawnHeight, 0f)
            );
        }

        Gizmos.color = Color.green;
        Gizmos.DrawLine(
            new Vector3(leftBound - columnWidth / 2, spawnHeight, 0f),
            new Vector3(leftBound + (columnCount - 0.5f) * columnWidth, spawnHeight, 0f)
        );

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector3(leftBound - columnWidth / 2, despawnHeight, 0f),
            new Vector3(leftBound + (columnCount - 0.5f) * columnWidth, despawnHeight, 0f)
        );
    }

    // --- GETTERS / SETTERS pour le système de difficulté ---
    public float AsteroidSpeed
    {
        get => asteroidSpeed;
        set => asteroidSpeed = value;
    }

    public float MinSpawnInterval
    {
        get => minSpawnInterval;
        set => minSpawnInterval = value;
    }

    public float MaxSpawnInterval
    {
        get => maxSpawnInterval;
        set => maxSpawnInterval = value;
    }

}
