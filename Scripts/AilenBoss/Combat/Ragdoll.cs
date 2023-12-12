using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    private void Start()
    {
        colliders = GetComponentsInChildren<Collider>(true);
        rigidbodies = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdolll(false);
    }

    public void ToggleRagdolll(bool isRagdollOn)
    {
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdollOn;
            }
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdollOn;
                rigidbody.useGravity = isRagdollOn;
            }
        }

        controller.enabled = !isRagdollOn;
        animator.enabled = !isRagdollOn;
    }
}
