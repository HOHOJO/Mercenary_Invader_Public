using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipManager : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    private Player controller;
    private Player conditions;

    // singleton
    public static EquipManager instance;

    private void Awake()
    {
        instance = this;
        controller = GetComponent<Player>();
        conditions = GetComponent<Player>();
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        //if (context.phase == InputActionPhase.Performed && curEquip != null && controller.canLook)
        {
            curEquip.OnAttackInput();
        }
    }

    public void EquipNew(ItemData item)
    {
        UnEquip();
        //curEquip = Instantiate(item.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip()
    {
        if (curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}