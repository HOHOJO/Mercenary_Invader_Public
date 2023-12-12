using UnityEngine;

public class WeaponEquipScript : MonoBehaviour
{
    public GameObject character; 
    public GameObject weaponObject; 
    public Renderer weaponRenderer;

    void Start()
    {
        Transform handBone = FindRightHandBone(character);

        if (handBone != null)
        {
            weaponObject.transform.parent = handBone;

            weaponObject.transform.localPosition = Vector3.zero;
            weaponObject.transform.localRotation = Quaternion.identity;

            Vector3 weaponScale = weaponObject.transform.localScale;
            if (weaponScale.x < 0) { weaponScale.x *= -1; }
            if (weaponScale.y < 0) { weaponScale.y *= -1; }
            if (weaponScale.z < 0) { weaponScale.z *= -1; }
            weaponObject.transform.localScale = weaponScale;

            weaponRenderer.sortingOrder = GetFingerSortingOrder(handBone.gameObject) - 1;
        }
        else
        {
            Debug.LogError("오른손 본을 찾을 수 없습니다!");
        }
    }

    Transform FindRightHandBone(GameObject characterObject)
    {
        return characterObject.transform.Find("RightHand");
    }

    int GetFingerSortingOrder(GameObject boneObject)
    {
        return boneObject.GetComponent<Renderer>().sortingOrder;
    }
}
