using UnityEngine;

public static class Extensions
{

    public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) {
        return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    }
    public static Vector3 RandomPointInAnnulus(this Vector3 origin, float minRadius, float maxRadius ) {
        float angle = Random.value * Mathf.PI *2f;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        float minRadius2 = minRadius * minRadius;
        float maxRadius2 = maxRadius * maxRadius;
        float distance = Mathf.Sqrt(Random.value *(maxRadius2 - minRadius2) +minRadius2);

        Vector3 position = new Vector3(direction.x, 0, direction.y) * distance;
        return origin + position;

    }
}