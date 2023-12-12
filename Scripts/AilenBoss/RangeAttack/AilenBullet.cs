using UnityEngine;

public class AlienBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float hitEffectDuration = 0.2f;
    [SerializeField] private AudioClip bulletHitSound;

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            health.DealDamage(damage);
            SoundManager.Instance.SFXPlay("BulletHitShot", bulletHitSound);
            ShowHitEffect(gameObject.transform.position);
            DeactivateBullet();
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

    private void DeactivateBullet()
    {
        gameObject.SetActive(false);
    }
}

