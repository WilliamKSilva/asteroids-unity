using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AsteroidSpawner;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Movement movement;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Movement
    {
        public Direction direction;
        public bool diagonal;

        public static Direction GetDirection(EnemySpawner.PositionNames position)
        {
            if (position == EnemySpawner.PositionNames.LEFT_WITH_RANDOM_Y)
            {
                return Direction.RIGHT;
            }

            if (position == EnemySpawner.PositionNames.RIGHT_WITH_RANDOM_Y)
            {
                return Direction.LEFT;
            }

            if (position == EnemySpawner.PositionNames.TOP_WITH_RANDOM_X)
            {
                return Direction.DOWN;
            }

            // Bottom with random X
            return Direction.UP;
        }

        public enum Direction
        {
            UP,
            DOWN,
            RIGHT,
            LEFT,
        }
    }
}
