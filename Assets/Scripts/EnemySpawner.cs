using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<Position> positions = new List<Position>();
    float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        BuildPositions();
        BuildEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 4.0f)
        {
            BuildEnemy();
            timer = 0.0f;
        }
    }

    void BuildEnemy()
    {
        Object[] enemyPrefabs = Resources.LoadAll("Prefabs/Enemies", typeof(Enemy));
        Enemy randomEnemy = (Enemy)enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Position randomPosition = positions[Random.Range(0, positions.Count)];

        Enemy enemy = Instantiate(randomEnemy, randomPosition.position, transform.rotation);
        enemy.movement.direction = Utils.Movement.GetDirection(randomPosition.positionName);
        enemy.RotateBasedOnMovement();
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
