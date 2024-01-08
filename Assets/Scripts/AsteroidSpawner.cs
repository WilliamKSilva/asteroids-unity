using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AsteroidSpawner : MonoBehaviour
{
    private static UnityEvent<Asteroid> asteroidDestroyedEvent;
    private List<Position> positions = new List<Position>();
    private static List<Asteroid> asteroids = new List<Asteroid>();
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        asteroidDestroyedEvent = new UnityEvent<Asteroid>();
        asteroidDestroyedEvent.AddListener(AsteroidDestroyed);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2)
        {
            BuildBigAsteroid();

            timer = 0f;
        }
    }

    public void BuildBigAsteroid()
    {
        BuildPositions();

        Object[] mediumAsteroidPrefabs = Resources.LoadAll("Prefabs/Asteroids/Big", typeof(Asteroid));
        Asteroid randomAsteroid = (Asteroid)mediumAsteroidPrefabs[Random.Range(0, mediumAsteroidPrefabs.Length)];

        Position randomPosition = positions[Random.Range(0, positions.Count)];
        Asteroid asteroid = Instantiate(randomAsteroid, randomPosition.position, transform.rotation);
        asteroid.name = "Asteroid";
        asteroid.movement.direction = Asteroid.Movement.GetDirection(randomPosition.positionName);
        asteroid.movement.diagonal = Asteroid.Movement.GetDiagonal(Camera.main.WorldToScreenPoint(randomPosition.position));
        asteroid.destroyedEvent = asteroidDestroyedEvent;
        asteroid.type = Asteroid.AsteroidType.BIG;

        asteroids.Add(asteroid);
    }

    public static void BuildChildAsteroid(Asteroid.AsteroidType type, Asteroid.Movement.Direction direction, Asteroid fatherAsteroid)
    {
        Asteroid randomAsteroid = null;
        if (type == Asteroid.AsteroidType.MEDIUM)
        {
            Object[] mediumAsteroidPrefabs = Resources.LoadAll("Prefabs/Asteroids/Medium", typeof(Asteroid));
            randomAsteroid = (Asteroid)mediumAsteroidPrefabs[Random.Range(0, mediumAsteroidPrefabs.Length)];
            randomAsteroid.type = Asteroid.AsteroidType.MEDIUM;
        }

        if (type == Asteroid.AsteroidType.SMALL)
        {
            Object[] smallAsteroidPrefabs = Resources.LoadAll("Prefabs/Asteroids/Small", typeof(Asteroid));
            randomAsteroid = (Asteroid)smallAsteroidPrefabs[Random.Range(0, smallAsteroidPrefabs.Length)];
            randomAsteroid.type = Asteroid.AsteroidType.MEDIUM;
        }

        Asteroid asteroid = Instantiate(randomAsteroid, Asteroid.GetChildAsteroidPosition(fatherAsteroid.rb.position, direction), Quaternion.identity);

        asteroid.name = "Asteroid";
        asteroid.movement.direction = direction;
        asteroid.movement.diagonal = true;
        asteroid.destroyedEvent = asteroidDestroyedEvent;

        asteroids.Add(asteroid);
    }

    private void BuildPositions()
    {
        positions.Clear();

        // Left with random Y
        float spawnY = Random.Range
               (0, Screen.height);
        Vector2 leftWithRandomYSpawn = Camera.main.ScreenToWorldPoint(new Vector2(-5, spawnY));


        positions.Add(new Position(leftWithRandomYSpawn, PositionNames.LEFT_WITH_RANDOM_Y));

        // Top with random X
        float spawnX = Random.Range
                (0, Screen.width);
        Vector2 topWithRandomXSpawn = Camera.main.ScreenToWorldPoint(new Vector2(spawnX, Screen.height + 5));

        positions.Add(new Position(topWithRandomXSpawn, PositionNames.TOP_WITH_RANDOM_X));

        // Right with random Y
        spawnY = Random.Range
                (0, Screen.height);
        Vector2 rightWithRandomY = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + 5, spawnY));

        positions.Add(new Position(rightWithRandomY, PositionNames.RIGHT_WITH_RANDOM_Y));

        // Bottom with random X
        spawnX = Random.Range
               (0, Screen.width);
        Vector2 bottomWithRandomX = Camera.main.ScreenToWorldPoint(new Vector2(spawnX, -5));

        positions.Add(new Position(bottomWithRandomX, PositionNames.BOTTOM_WITH_RANDOM_X));
    }

    void AsteroidDestroyed(Asteroid asteroid)
    {
        Debug.Log("Asteroid Destroyed");
        int asteroidIndex = asteroids.FindIndex(a => a.GetInstanceID() == asteroid.GetInstanceID());
        if (asteroidIndex > -1)
        {
            asteroids.RemoveAt(asteroidIndex);
        }

        Destroy(asteroid.rb.gameObject);
    }

    private class Position
    {
        public Position(Vector2 position, PositionNames positionName)
        {
            this.position = position;
            this.positionName = positionName;
        }

        public Vector2 position;
        public PositionNames positionName;
    }

    public enum PositionNames
    {
        LEFT_WITH_RANDOM_Y,
        RIGHT_WITH_RANDOM_Y,
        TOP_WITH_RANDOM_X,
        BOTTOM_WITH_RANDOM_X
    }
}
