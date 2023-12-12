using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private float hitEffectDuration = 0.2f;

    [SerializeField] private AudioClip hitSound;

    private int damage = 10;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            health.DealDamage(damage);
            ShowHitEffect(gameObject.transform.position);
            if (hitSound != null)
            {
                SoundManager.Instance.SFXPlay("HitShot", hitSound);
            }
        }
    }

    public void SetAttack(int damage)
    {
        this.damage = damage;
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
