using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AsteroidSpawner;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    List<Position> positions = new List<Position>();
    // Start is called before the first frame update
    void Start()
    {
        BuildPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: finish Enemy spawner
    void BuildEnemy()
    {
        Object[] enemyPrefabs = Resources.LoadAll("Prefabs/Enemies", typeof(Enemy));
        Enemy randomEnemy = (Enemy)enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Position position = positions[Random.Range(0, positions.Count)];
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
