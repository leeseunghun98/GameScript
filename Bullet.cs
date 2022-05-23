using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    public float bulletSpeed;
    
    private void Awake() {
        Destroy(gameObject, 2.5f);
    }
}
