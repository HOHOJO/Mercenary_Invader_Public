using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public SkinnedMeshRenderer rend;
    [SerializeField] Collider[] collider;
    [SerializeField] Rigidbody rb;
    [SerializeField] Monster monster;
    [SerializeField] Monster2 monster2;
    [SerializeField] public int health=1;
    public bool IsDead => health == 0;
    public event Action OnDie;
    private float DamageTime = 0.5f;
    private static float deleyTime = 0f;
    private bool isBorder=true;
    private Collider targatCol;

    private List<Collider> alreadyColliderWith = new List<Collider>();

    void Start()
    {
        if (monster != null)
        {
            health = monster.Data.HP;
            monster.health = health;
        }
        else
        {
            health = monster2.Data.HP;
            monster2.health = health;
        }
    }

    // Update is called once per frame
    void Update()
    {
        deleyTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        WallCheck();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Monster")
        {
            return;
        }
        if (alreadyColliderWith.Contains(targatCol))
        {
            return;
        }
        if (col.collider.tag == "Melee")
        {
            Debug.Log("데미지2");
            tagetColl(col.collider);
            DamageCheck();
        }
    }

    public void DamageCheck()
    {
        if (targatCol.tag == "Melee")
        {
            if(deleyTime>DamageTime)
            {
                alreadyColliderWith.Add(targatCol);

                if (targatCol.TryGetComponent(out Weapon curValue)) // 대상 콜라이더의 객체 가져오기
                {
                    if(monster!=null)
                    {
                        monster.soundManager.SFXPlay("Hit",monster.monsterSound.audioClips[6]);
                    }
                    else
                    {
                        monster2.soundManager.SFXPlay("Hit", monster2.monsterSound.audioClips[0]);
                    }
                    TakeDamage(curValue.damage);
                    alreadyColliderWith.Remove(targatCol);
                }
                deleyTime = 0f;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (health == 0) return;
        health = Mathf.Max(health - damage, 0);

        if (health <= 0)
            OnDie?.Invoke();

        if (monster != null)
        {
            monster.health = health;
        }
        else
        {
            monster2.health = health;
        }
    }

    private void WallCheck()
    {
        isBorder = Physics.Raycast(transform.position + new Vector3(0, 1.0f, 0), transform.forward, 0.6f, LayerMask.GetMask("Weapon"));
    }

    public void tagetColl(Collider other)
    {
        targatCol = other;
    }
}
