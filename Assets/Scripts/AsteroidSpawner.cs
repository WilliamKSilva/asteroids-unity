using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        if (!GameState.gameState.started)
        {
            return;
        }

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
        Asteroid randomAsteroid = (Asteroid)mediumAsteroidPrefabs[0];

        Position randomPosition = positions[Random.Range(0, positions.Count)];
        Asteroid asteroid = Instantiate(randomAsteroid, randomPosition.position, transform.rotation);
        asteroid.name = "Asteroid";
        asteroid.movement.direction = Utils.Movement.GetDirection(randomPosition.positionName);
        asteroid.movement.diagonal = Utils.Movement.GetDiagonal(Camera.main.WorldToScreenPoint(randomPosition.position));
        asteroid.destroyedEvent = asteroidDestroyedEvent;
        asteroid.type = Asteroid.AsteroidType.BIG;

        asteroids.Add(asteroid);
    }

    public static void BuildChildAsteroid(Asteroid.AsteroidType type, Utils.Movement.Direction direction, Asteroid fatherAsteroid, Projectile projectile)
    {
        Asteroid randomAsteroid = null;
        if (type == Asteroid.AsteroidType.MEDIUM)
        {
            Object[] mediumAsteroidPrefabs = Resources.LoadAll("Prefabs/Asteroids/Medium", typeof(Asteroid));
            randomAsteroid = (Asteroid)mediumAsteroidPrefabs[0];
            randomAsteroid.type = Asteroid.AsteroidType.MEDIUM;
        }

        if (type == Asteroid.AsteroidType.SMALL)
        {
            Object[] smallAsteroidPrefabs = Resources.LoadAll("Prefabs/Asteroids/Small", typeof(Asteroid));
            randomAsteroid = (Asteroid)smallAsteroidPrefabs[0];
            randomAsteroid.type = Asteroid.AsteroidType.SMALL;
        }

        Asteroid asteroid = Instantiate(randomAsteroid, Asteroid.GetChildAsteroidPosition(projectile.rb.transform.up, fatherAsteroid.rb.position), Quaternion.identity);

        asteroid.name = "Asteroid";
        asteroid.movement.direction = direction;
        asteroid.movement.diagonal = true;
        asteroid.childAsteroid = true;
        asteroid.destroyedEvent = asteroidDestroyedEvent;
        Asteroid.RotateChildAsteroid(asteroid, projectile.rb.rotation);

        asteroids.Add(asteroid);
    }

    private void BuildPositions()
    {
        positions.Clear();

        // Left with random Y
        float spawnY = Random.Range
               (0, Screen.height);
        Vector2 leftWithRandomYSpawn = Camera.main.ScreenToWorldPoint(new Vector2(-5, spawnY));


        positions.Add(new Position(leftWithRandomYSpawn, Utils.PositionNames.LEFT_WITH_RANDOM_Y));

        // Top with random X
        float spawnX = Random.Range
                (0, Screen.width);
        Vector2 topWithRandomXSpawn = Camera.main.ScreenToWorldPoint(new Vector2(spawnX, Screen.height + 5));

        positions.Add(new Position(topWithRandomXSpawn, Utils.PositionNames.TOP_WITH_RANDOM_X));

        // Right with random Y
        spawnY = Random.Range
                (0, Screen.height);
        Vector2 rightWithRandomY = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width + 5, spawnY));

        positions.Add(new Position(rightWithRandomY, Utils.PositionNames.RIGHT_WITH_RANDOM_Y));

        // Bottom with random X
        spawnX = Random.Range
               (0, Screen.width);
        Vector2 bottomWithRandomX = Camera.main.ScreenToWorldPoint(new Vector2(spawnX, -5));

        positions.Add(new Position(bottomWithRandomX, Utils.PositionNames.BOTTOM_WITH_RANDOM_X));
    }

    void AsteroidDestroyed(Asteroid asteroid)
    {
        int asteroidIndex = asteroids.FindIndex(a => a.GetInstanceID() == asteroid.GetInstanceID());
        if (asteroidIndex > -1)
        {
            asteroids.RemoveAt(asteroidIndex);
        }

        Destroy(asteroid.rb.gameObject);
    }

    private class Position
    {
        public Position(Vector2 position, Utils.PositionNames positionName)
        {
            this.position = position;
            this.positionName = positionName;
        }

        public Vector2 position;
        public Utils.PositionNames positionName;
    }
}
