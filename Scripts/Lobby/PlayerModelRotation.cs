using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerModelRotation : MonoBehaviour, IDragHandler
{
    [SerializeField] GameObject characterModel;
    private float speed = 5f;
    private Vector3 rotation;
    private void Start()
    {
        rotation = characterModel.transform.localEulerAngles;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rotation.y += Input.GetAxis("Mouse X") * speed;
        characterModel.transform.localEulerAngles = -rotation;
    }
}
