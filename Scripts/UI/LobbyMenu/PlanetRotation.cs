using DG.Tweening;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float rotationDuration = 2.0f;
    public Vector3 targetRotation = new Vector3(0, 45, 0);

    void Start()
    {
        transform.DORotate(targetRotation, rotationDuration, RotateMode.FastBeyond360)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}