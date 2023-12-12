using UnityEngine;

public class MinimapPos : MonoBehaviour
{
    [SerializeField] private bool x, y, z;
    [SerializeField] private Transform target;

    private void Update()
    {
        if (!target) return;
        transform.position = new Vector3(
            (x ? target.position.x : target.position.x),
            (y ? target.position.y : transform.position.y),
            (z ? target.position.z : transform.position.z));
    }
}
