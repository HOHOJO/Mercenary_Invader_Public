using UnityEngine;

public class MeteorAttack : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float hitEffectDuration = 0.2f;
    [SerializeField] private AudioClip bulletHitSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            health.DealDamage(damage);
            SoundManager.Instance.SFXPlay("BulletHitShot", bulletHitSound);
            ShowHitEffect(gameObject.transform.position);
        }
    }

    private void ShowHitEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, position, Quaternion.identity);
            Destroy(hitEffect, hitEffectDuration);
        }
    }
}

