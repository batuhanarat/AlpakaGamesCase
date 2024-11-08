using Unity.Mathematics;

public static class SpatialHashing
{
    // Calculates the grid position based on world position and cell size, ignoring the y-axis
    public static int2 GridPosition(float3 position, float cellSize)
    {
        return new int2(
            (int)math.floor(position.x / cellSize),
            (int)math.floor(position.z / cellSize)
        );
    }

    // Generates a unique hash code for a given grid position (2D)
    public static int Hash(int2 gridPos)
    {
        unchecked
        {
            int hash = gridPos.x;
            hash = 73856093 * hash ^ gridPos.y;
            hash = 19349663 * hash;
            return hash;
        }
    }
}