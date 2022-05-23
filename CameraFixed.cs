using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixed : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // 카메라의 Player와의 상대적 위치
    void Update()
    {
        transform.position = target.position + offset;
    }
}
