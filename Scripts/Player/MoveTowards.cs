using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveTowards : MonoBehaviour
{
    // 추적할 대상
    public Transform target;

    // 카메라와의 거리
    public float dist = 6f;

    // 카메라 초기 위치
    private float x = 0.0f;
    private float y = 0.0f;

    // y값 제한 (위 아래 제한)
    public float yMinLimit = 0f;
    public float yMaxLimit = 0f;

    // 카메라 회전 속도 (슬라이더 조절을 위해 public으로 변경)
    public float xSpeed = 0.0f; // 초기값은 0
    public float ySpeed = 0.0f; // 초기값은 0

    // 최대 스피드
    public float maxSpeed = 300.0f;

    // 앵글의 최소,최대 제한
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {

        // 카메라 회전속도 계산
        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

        // 앵글값 정하기(y값 제한)
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // 카메라 위치 변화 계산
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0, 0.9f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);

        transform.rotation = rotation;
        target.rotation = Quaternion.Euler(0, x, 0);
        transform.position = position;
    }
}
