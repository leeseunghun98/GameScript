using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfDetectingExit : MonoBehaviour
{
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            Dwarf enemy = gameObject.GetComponentInParent<Dwarf>();
            enemy.isChase = false;
            enemy.rigid.velocity = Vector3.zero;
            enemy.rigid.angularVelocity = Vector3.zero;
        }
    }
}
