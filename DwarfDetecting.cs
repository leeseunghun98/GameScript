using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfDetecting : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            Dwarf enemy = gameObject.GetComponentInParent<Dwarf>();
            enemy.isChase = true;
        }
    }
}
