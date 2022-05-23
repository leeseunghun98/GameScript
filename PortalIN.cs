using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIN : MonoBehaviour
{
    public Transform TranslatePosition;
    // public Transform[] PortalPositions;
    bool gDown;
    
    void Update() {
        gDown = Input.GetButtonDown("Interaction");
    }
    // G키를 눌러 포탈 활성화
    void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player" && gDown) {

            // 포탈 계속 탈 경우 저절로 회전하는 버그로 인해 주석처리 후 아래 코드로 변경

                // Transform ParentTransform = other.transform;
                // while(true) {
                //     if(ParentTransform.parent == null)
                //         break;
                //     else
                //         ParentTransform = ParentTransform.parent;
                // }

                // ParentTransform.position = TranslatePosition.position;
                // ParentTransform.rotation = other.transform.rotation;
                other.transform.position = TranslatePosition.position;


        }
    }
}
