using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDetectingExit : MonoBehaviour
{
    // when zombie far enough from player
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            Enemy enemy = gameObject.GetComponentInParent<Enemy>();
            enemy.isChase = false;
            enemy.rigid.velocity = Vector3.zero;
            enemy.rigid.angularVelocity = Vector3.zero;
        }
    }
}
