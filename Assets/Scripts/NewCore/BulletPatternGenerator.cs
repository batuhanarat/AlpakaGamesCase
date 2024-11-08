using UnityEngine;

public interface IBulletPattern {
    BulletProjectile[] GeneratePattern(Vector3 origin, Vector3 forward, int bulletCount, float speed);
}

public class BulletPatternGenerator {
    IBulletPattern bulletPattern;

    public BulletPatternGenerator(IBulletPattern pattern) {
        bulletPattern = pattern;
    }

    public BulletProjectile[] GeneratePattern(Vector3 origin, int bulletCount, float speed) {
        return bulletPattern.GeneratePattern(origin, Vector3.forward, bulletCount, speed);
    }

    public void SetPattern(IBulletPattern pattern) => bulletPattern = pattern;
}

public class RadialPattern : IBulletPattern {
    public BulletProjectile[] GeneratePattern(Vector3 origin, Vector3 forward, int bulletCount, float speed) {
        BulletProjectile[] bullets = new BulletProjectile[bulletCount];
        float angleStep = 360f / bulletCount;  // Divide the circle into equal angles

        for (int i = 0; i < bulletCount; i++) {
            float angle = i * angleStep;

            // Create a local direction in the x/z plane
            Vector3 localDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            // Rotate the local direction to match the forward direction of the object
            Vector3 direction = Quaternion.LookRotation(forward) * localDirection;

            // Initialize the bullet with the calculated position and direction
            bullets[i] = new BulletProjectile(origin, direction, speed);
        }

        return bullets;
    }
}

public class WavePattern : IBulletPattern {
    public BulletProjectile[] GeneratePattern(Vector3 origin, Vector3 forward, int bulletCount, float speed) {
        BulletProjectile[] bullets = new BulletProjectile[bulletCount];
        float waveFrequency = 0.1f;  // Frequency of the wave oscillation
        float waveAmplitude = 1f;    // Amplitude of wave motion

        for (int i = 0; i < bulletCount; i++) {
            float waveOffset = Mathf.Sin(i * waveFrequency) * waveAmplitude;

            // Calculate direction in x/z plane, with wave affecting the x-axis
            Vector3 localDirection = new Vector3(waveOffset, 0, 1).normalized;

            // Rotate the local direction to align with the forward direction
            Vector3 direction = Quaternion.LookRotation(forward) * localDirection;

            bullets[i] = new BulletProjectile(origin, direction, speed);
        }

        return bullets;
    }
}

public class SpiralPattern : IBulletPattern {
    public BulletProjectile[] GeneratePattern(Vector3 origin, Vector3 forward, int bulletCount, float speed) {
        BulletProjectile[] bullets = new BulletProjectile[bulletCount];
        float angleStep = 10f;  // Angle between bullets
        float radiusStep = 0.1f; // Radius increases per bullet

        for (int i = 0; i < bulletCount; i++) {
            float angle = i * angleStep;
            float radius = i * radiusStep;  // Spiral outwards

            // Calculate direction in x/z plane
            Vector3 localDirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

            // Rotate local direction to match the forward direction
            Vector3 direction = Quaternion.LookRotation(forward) * localDirection;
            Vector3 offset = direction * radius;

            bullets[i] = new BulletProjectile(origin + offset, direction, speed);
        }

        return bullets;
    }
}