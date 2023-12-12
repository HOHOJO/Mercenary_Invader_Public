using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoveTowards : MonoBehaviour
{
    // ������ ���
    public Transform target;

    // ī�޶���� �Ÿ�
    public float dist = 6f;

    // ī�޶� �ʱ� ��ġ
    private float x = 0.0f;
    private float y = 0.0f;

    // y�� ���� (�� �Ʒ� ����)
    public float yMinLimit = 0f;
    public float yMaxLimit = 0f;

    // ī�޶� ȸ�� �ӵ� (�����̴� ������ ���� public���� ����)
    public float xSpeed = 0.0f; // �ʱⰪ�� 0
    public float ySpeed = 0.0f; // �ʱⰪ�� 0

    // �ִ� ���ǵ�
    public float maxSpeed = 300.0f;

    // �ޱ��� �ּ�,�ִ� ����
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

        // ī�޶� ȸ���ӵ� ���
        x += Input.GetAxis("Mouse X") * xSpeed * 0.015f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.015f;

        // �ޱ۰� ���ϱ�(y�� ����)
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // ī�޶� ��ġ ��ȭ ���
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0, 0.9f, -dist) + target.position + new Vector3(0.0f, 0, 0.0f);

        transform.rotation = rotation;
        target.rotation = Quaternion.Euler(0, x, 0);
        transform.position = position;
    }
}
