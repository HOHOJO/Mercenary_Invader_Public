using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float lookSensitivity;
    [SerializeField]
    private float cameraRoatationLimit;
    private float currentCameraRotationX = 0f;
    [SerializeField]
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public float health;
    public float maxHealth;
    float hAxis;
    float vAxis;
    bool iDown;
    bool wDown;
    bool jDown;
    bool dDown;
    bool isJump;
    bool isDodge;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool isSwap;
    bool fDown;
    bool isFireReady;
    bool isDamage;
    Vector3 moveVec;
    Rigidbody rigid;
    Animator anim;
    MeshRenderer[] meshs;
    GameObject nearObject;
    Weapon equipWeapon;
    SpriteRenderer mesh;
    Color halfA = new Color(1, 1, 1, 0);
    Color fullA = new Color(1, 1, 1, 1);
    int equipWeaponIndex =-1;
    float fireDelay;

    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.OnTakeDamage += OnTakeDamage;
            healthSystem.OnDie += OnDie;
        }

        rigid = GetComponent<Rigidbody>();
    }
   
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponents<MeshRenderer>();
        mesh = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Swap();
        Interation();
        Attack();
        CameraRotaion();
        CharacterRotation();
        Dodge();
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        dDown = Input.GetButtonDown("Dodge");
        iDown = Input.GetButton("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
        fDown = Input.GetButtonDown("Fire1");
    }
    void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHoriznotal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;
        Vector3 _velocity = (_moveHoriznotal + _moveVertical).normalized;
        rigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        if (isSwap)
            _velocity  = Vector3.zero;
        transform.position += _velocity * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        anim.SetBool("isRun", _velocity  != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }
    void Turn()
    { transform.LookAt(transform.position + moveVec); 
    }
    void Jump()
    {
        if (jDown && !isJump && !isDodge && !isSwap)
        {
            rigid.AddForce(Vector3.up * 65, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }
    void Dodge()
    {
        if (dDown && !isDodge && !isSwap)
        {
            gameObject.tag = "Ghost";
            anim.SetTrigger("doDodge");
            isDodge = true;
            Invoke("DodgeOut", 2f);
        }
    }
    void DodgeOut()
    {
        gameObject.tag = "Player";
        isDodge = false;
    }
    void Attack()
    {
        if (equipWeapon == null)
            return;
        fireDelay += Time.deltaTime;
        isFireReady = equipWeapon.rate < fireDelay;
        if(fDown && isFireReady && !isSwap)
        {
            equipWeapon.Use();
            anim.SetTrigger("doSwing");
            fireDelay = 0;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor") 
        {
            anim.SetBool("isJump", false);
            isJump = false; }
    }
    void Swap()
        {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;
        if (sDown3) weaponIndex = 2;

        if((sDown1 || sDown2 || sDown3) && !isJump)
        {
            if(equipWeapon != null)
            equipWeapon.gameObject.SetActive(false);
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Weapon>();
            equipWeapon.gameObject.SetActive(true);
            anim.SetTrigger("doSwap");
            isSwap = true;
            Invoke("SwapOut", 0.4f);
        }
    }
    void SwapOut()
    { isSwap = false; }
    void Interation()
    {
        if (iDown && nearObject != null && !isJump)
        {
            if (nearObject.tag == "weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int waponIndex = item.value;
                hasWeapons[waponIndex] = true;

                Destroy(nearObject);
            }
        }
    }
   void OnTriggerStay(Collider other)
    {
        if (other.tag == "weapon")
            nearObject = other.gameObject;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "weapon")
            nearObject = null;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "MonsterHit")
    //        if (!isDamage)
    //        {
    //            {
    //                Hit monsterHit = other.GetComponent<Hit>();
    //                health -= monsterHit.damage;
    //                StartCoroutine(Ondamage());
    //            }
    //        }
    //}
    public void DM (float damage)
    {
        if(healthSystem != null)
            healthSystem.DealDamage((int)damage);

        health -= damage;
        anim.SetTrigger("doDamage");
        StartCoroutine(Ondamage());
        StartCoroutine(alphablink());

        if (health <= 0)
        {
            GameManager.Instance.GameOver(false);
            anim.SetTrigger("doDie");
            gameObject.layer = 9;
            gameObject.tag = "Ghost";
        }
    }
    public void OnTakeDamage()
    {
        anim.SetTrigger("doDamage");
        StartCoroutine(Ondamage());
        StartCoroutine(alphablink());
    }

    public void OnDie()
    {
        GameManager.Instance.GameOver(false);
        anim.SetTrigger("doDie");
        gameObject.layer = 9;
        gameObject.tag = "Ghost";
    }

    IEnumerator Ondamage()
    {
        yield return new WaitForSeconds(7f);
        isDamage = false;
    }
    IEnumerator alphablink()
    {
        while(isDamage)
        {
            yield return new WaitForSeconds(0.1f);
            mesh.color = halfA;
            yield return new WaitForSeconds(0.1f);
            mesh.color = fullA;
        }
    }
    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(_characterRotationY));
    }
    private void CameraRotaion()
    {
        float _xRotaion = Input.GetAxisRaw("Mouse Y");
        float _CameraRotationX = _xRotaion * lookSensitivity;
        currentCameraRotationX += _CameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRoatationLimit, cameraRoatationLimit);
    }

}
