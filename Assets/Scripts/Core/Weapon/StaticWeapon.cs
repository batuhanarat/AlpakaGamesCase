using UnityEngine;

public class StaticWeapon : Weapon
{
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        SetCircleSize(weaponData.attackRange);
    }
    public void SetCircleSize(float radius)
    {
        if (spriteRenderer != null)
        {
            float originalSize = spriteRenderer.sprite.bounds.size.x;
            float scale = radius * 2f / originalSize;
            transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}