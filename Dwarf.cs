using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dwarf : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyCurHealth;
    public int Dwarf_Attack_Damage;
    public float walkSpeed;
    public bool isRun;
    public bool isChase;
    bool isDead;
    bool isThinking = false;
    public SphereCollider[] attackArea;
    bool isAttack;
    public Rigidbody rigid;
    NavMeshAgent nav;
    Animator anim;
    SkinnedMeshRenderer[] meshes;
    public Transform target;
    public GameObject blood;
    Quaternion nowRotation;
    Vector3 moveVec;
    private void Awake() {
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update() {
        Move();
        Finding();    
    }

    void FixedUpdate() {
    }

    void Move()
    {
        if (!isChase && !isDead)
        {
            if (!isThinking)
            {
                StartCoroutine(WhereTogo());
            }
            nav.speed = 0.15f;
            // transform.position += moveVec * walkSpeed * Time.deltaTime;
            nav.SetDestination(transform.position + moveVec);
            // Stop
            anim.SetBool("isRun", false);
        }
    }
    // Moving while not chasing
    IEnumerator WhereTogo(){
        isThinking = true;
        moveVec = new Vector3(Random.Range(-10,11), 0 ,Random.Range(-10,11));
        yield return new WaitForSeconds(3f);
        isThinking = false;
    }
    // Attack Enemy
    IEnumerator Attacking()
    {
        anim.SetTrigger("doAttack");
        isAttack = true;
        // Right hand attack
        attackArea[0].enabled = true;
        yield return new WaitForSeconds(0.4f);
        attackArea[0].enabled = false;
        attackArea[1].enabled = true;
        yield return new WaitForSeconds(0.4f);
        attackArea[1].enabled = false;
        yield return new WaitForSeconds(0.2f);
        isAttack = false;
    }
    // Find Enemy
    void Finding(){
        // Run , Attack
        if (isChase == true && !isDead)
        {
            float targetRadius = 1f;
            float targetRange = 1f;
            anim.SetBool("isRun", true);
            nav.speed = 3.5f;
            nav.SetDestination(target.position);

            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, targetRadius, transform.forward, targetRange, LayerMask.GetMask("Player"));

            if (rayHits.Length > 0 && !isAttack)
            {
                StartCoroutine(Attacking());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Damaged by bullet
        if (other.gameObject.tag == "Bullet" && !isDead){
            Debug.Log("Hit");
            Bullet bullet = other.gameObject.GetComponentInParent<Bullet>();
            enemyCurHealth -= bullet.bulletDamage;
            Vector3 reactVec = other.transform.position - transform.position;
            GameObject bloodparticle = Instantiate(blood, transform.position, transform.rotation);
            Destroy(bloodparticle, 2.5f);
            Destroy(other.gameObject);
            if (enemyCurHealth <= 0)
            {
                isDead = true;
                gameObject.layer = 10;
                
                anim.SetTrigger("doDie");
                
                rigid.mass = 20;
                rigid.freezeRotation = true;
                Destroy(gameObject, 2.5f);
            }
            rigid.AddForce(reactVec * 4f, ForceMode.Impulse);
        }
    }
}
