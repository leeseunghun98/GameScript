using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // HP
    public int curHealth;
    public int maxHealth;
    public TPSCharacterController tPS;

    bool isDamaging;
    Rigidbody rigid;

    // Audio
    AudioSource audioSource;
    public AudioClip jumpClip;
    void Awake() {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    
    // Hurt by zombie
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Zombie_Attack_Area" && !isDamaging)
        {
            StartCoroutine(onHit(other.GetComponentInParent<Enemy>().zombie_Attack_Damage));
        }
    }

    // Jump Landing
    private void OnCollisionEnter(Collision other) {
        if (tPS.isJump == true & (other.gameObject.tag == "MapComponent" | other.gameObject.tag == "Ground"))
        {
            tPS.animator.SetBool("isJump", false);
            tPS.isJump = false;
        } 
    }

    // onHit, onDamage, isDamaging, isDamaged
    IEnumerator onHit(int zombie_Damage){
        isDamaging = true;
        curHealth -= zombie_Damage;
        Debug.Log("onHit!!!");
        yield return new WaitForSeconds(0.25f);
        isDamaging = false;
    }
}
