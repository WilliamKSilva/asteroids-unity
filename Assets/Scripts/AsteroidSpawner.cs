using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroid;
    UnityEvent<Asteroid> asteroidDestroyedEvent;
    private Sprite[] sprites;
    private List<Position> positions = new List<Position>();
    private List<Asteroid> asteroids = new List<Asteroid>();
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Asteroids");
        asteroidDestroyedEvent = new UnityEvent<Asteroid>();
        asteroidDestroyedEvent.AddListener(AsteroidDestroyed);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(asteroids.Count);
        timer += Time.deltaTime;

        if (timer >= 2)
        {
            BuildAsteroid();

            timer = 0f;
        }
    }

    void BuildAsteroid()
    {
        BuildPositions();

        Sprite randomSprite = sprites[Random.Range(0, sprites.Length)];
        Asteroid randomAsteroid = GameObject.Instantiate(asteroid, transform.position, transform.rotation);
        SpriteRenderer spriteRenderer = randomAsteroid.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = randomSprite;
        randomAsteroid.type = Asteroid.AsteroidType.BIG;

        Position randomPosition = positions[Random.Range(0, positions.Count)];
        randomAsteroid.movement.direction = Asteroid.Movement.GetDirection(randomPosition.positionName);
        randomAsteroid.movement.diagonal = Asteroid.Movement.GetRandomDiagonal();
        randomAsteroid.transform.position = randomPosition.position;
        randomAsteroid.destroyedEvent = asteroidDestroyedEvent;

        asteroids.Add(randomAsteroid);
    }

    void BuildPositions()
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
        int asteroidIndex = asteroids.FindIndex(a => a.GetInstanceID() == asteroid.GetInstanceID());
        if (asteroidIndex > -1)
        {
            asteroids.RemoveAt(asteroidIndex);
        }
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
