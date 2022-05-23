using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    public Transform characterBody;
    public Transform cameraArm;
    public float rotateSensitivity;
    public float characterSpeed;

    Rigidbody rigid;
    public Animator animator;
    AudioSource audioSource;
    Player player;

    bool jDown;
    public bool isJump = false;
    bool isRun;

    void Awake()
    {
        player = GetComponentInChildren<Player>();
        audioSource = GetComponentInChildren<AudioSource>();
        rigid = characterBody.GetComponent<Rigidbody>();
        animator = characterBody.GetComponent<Animator>();
    }

    void FixedUpdate() {
        Jump();
    }

    void Update()
    {
        LookAround();
        Move();
        GetInput();
    }
    // 점프 키 받기
    void GetInput()
    {
        jDown = Input.GetButtonDown("Jump");
    }
    // 점프
    void Jump()
    {
        if(jDown && !isJump) {
            audioSource.clip = player.jumpClip;
            audioSource.Play();
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            animator.SetTrigger("doJump");
            isJump = true;
            animator.SetBool("isJump", true);
            Invoke("OnAir", 0.1f);
        }
    }
    // while Jump
    void OnAir(){
        animator.SetTrigger("isOnAir");
    }
    // wasd move
    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        isRun = moveInput.magnitude != 0;
        animator.SetBool("isRun", isRun);
        if(isRun)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * characterSpeed;
        }
    }
    // Heading Mouse position
    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y * rotateSensitivity;

        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x * rotateSensitivity, camAngle.z);
    }
}
