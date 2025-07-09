using UnityEngine;

public class ProjectileGrenadeBullet : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private float destroyDelay = 0.05f;
    [SerializeField] private LayerMask ignoredCollisionLayers; // 👈 Layers to ignore for collision

    private bool hasExploded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasExploded) return;

        int collidedLayer = collision.gameObject.layer;

        // Check if the collided object is in the ignored layers
        if (((1 << collidedLayer) & ignoredCollisionLayers) != 0)
        {
            Debug.Log("Ignored collision with: " + collision.gameObject.name);
            return;
        }

        hasExploded = true;
        Explode();
    }

    private void Explode()
    {
        if (explosionEffect)
        {
            var exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(exp, destroyDelay+0.5f);
        }

        // Optional: still filter damage to Damageable layer if needed
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var hit in hitColliders)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }

        
        
        Destroy(gameObject, destroyDelay);
    }
}