using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_1_Damage : MonoBehaviour
{
    public Monster monster;
    public bool attack_ok;
    public Collider myCollider;
    private float moreDamage;
    private static float deleyTime = 0f;
    private float DamageTime = 3f;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();

        attack_ok= false;
        myCollider.enabled = false;
        moreDamage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        deleyTime += Time.deltaTime;
    }

    public void attackStart(float n)
    {
        moreDamage = n;
        attack_ok = true;
        myCollider.enabled = true;
    }

    public void attackExit()
    {
        moreDamage = 0;
        attack_ok = false;
        myCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (attack_ok)
        {
            if (other == null) return;
            if (other == myCollider) return;
            if (other.gameObject.tag == "Monster")
            {
                return;
            }
            if (other.gameObject.tag == "Melee")
            {
                return;
            }
            if (other.gameObject.tag == "Player")
            {
                if(deleyTime > DamageTime)
                {
                    if (other.gameObject.TryGetComponent(out Player curValue)) // 대상 콜라이더의 객체 가져오기
                    {
                        curValue.DM(monster.Data.damage + moreDamage);
                        deleyTime = 0f;
                        if(effect!=null)
                        {
                            Instantiate(effect, other.transform.position, other.transform.rotation);

                        }
                    }
                }
            }
        }
    }
}
