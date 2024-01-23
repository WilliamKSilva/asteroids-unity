using UnityEngine;

public static class Utils
{
    private static readonly float diagonalPositionPossibility = 300;

    public static bool ObjectOutOfBounds(Movement.Direction direction, Vector2 objectPosition)
    {
        if (direction == Movement.Direction.UP)
        {
            return GetScreenPosition(objectPosition).y >= Screen.height || GetScreenPosition(objectPosition).x >= Screen.width;
        }

        if (direction == Movement.Direction.DOWN)
        {
            return GetScreenPosition(objectPosition).y <= 0 || GetScreenPosition(objectPosition).x >= Screen.width;
        }

        if (direction == Movement.Direction.LEFT)
        {
            return GetScreenPosition(objectPosition).x <= 0 || GetScreenPosition(objectPosition).y >= Screen.height;
        }

        if (direction == Movement.Direction.RIGHT)
        {
            return GetScreenPosition(objectPosition).x >= Screen.width || GetScreenPosition(objectPosition).y >= Screen.height;
        }

        return false;
    }

    static Vector2 GetScreenPosition(Vector2 position)
    {
        return Camera.main.WorldToScreenPoint(position);
    }

    public class Movement
    {
        public Direction direction;
        public bool diagonal;

        public static Direction GetDirection(Utils.PositionNames position)
        {
            if (position == Utils.PositionNames.LEFT_WITH_RANDOM_Y)
            {
                return Direction.RIGHT;
            }

            if (position == Utils.PositionNames.RIGHT_WITH_RANDOM_Y)
            {
                return Direction.LEFT;
            }

            if (position == Utils.PositionNames.TOP_WITH_RANDOM_X)
            {
                return Direction.DOWN;
            }

            // Bottom with random X
            return Direction.UP;
        }

        public static bool GetDiagonal(Vector2 screenPosition)
        {
            bool diagonalPossible = false;
            if (screenPosition.x >= diagonalPositionPossibility && screenPosition.x <= Screen.width - diagonalPositionPossibility)
            {
                diagonalPossible = true;
            }

            if (screenPosition.y >= diagonalPositionPossibility && screenPosition.y <= Screen.height - diagonalPositionPossibility)
            {
                diagonalPossible = true;
            }

            if (diagonalPossible)
            {
                int random = Random.Range(0, 2);

                return random == 1;
            }

            return false;
        }

        public enum Direction
        {
            UP,
            DOWN,
            RIGHT,
            LEFT,
        }
    }

    public enum PositionNames
    {
        LEFT_WITH_RANDOM_Y,
        RIGHT_WITH_RANDOM_Y,
        TOP_WITH_RANDOM_X,
        BOTTOM_WITH_RANDOM_X
    }
}
