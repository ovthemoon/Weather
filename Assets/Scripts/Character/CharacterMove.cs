using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class CharacterMove : MonoBehaviour
{
    //<<<<<<< yunsohee


    
    public float groundDetectDistance = 0.1f;
    
    public float walkSpeed = 5;
    public AudioSource[] walkSound;

    private float jumpForce = 10;


    private float moveDirX;
    private float moveDirZ;

    private bool isGround = true; //캐릭터가 땅에 있는지 확인
    private bool isMoving = false;
    

    // 땅 착지 여부
    private CapsuleCollider capsuleCollider;

    //민감도
    public float lookSensitivity = 3;

    //카메라 한계
    public float cameraRotationLimit = 60;
    public float currentCameraRotationX = 0;
    public bool canMove=true;
    //필요한 컴포넌트
    [SerializeField]
    private CinemachineVirtualCamera theCamera;

    private Rigidbody myRigid;
    private AudioSource currentWalkSound;

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        myRigid = GetComponent<Rigidbody>();
        currentWalkSound= GetComponent<AudioSource>();
        if (SceneManager.GetActiveScene().name == "Map2_Desert")
        {
            currentWalkSound = walkSound[1];
        }
        else if (SceneManager.GetActiveScene().name == "Map2_Pole")
        {
            currentWalkSound = walkSound[2];
        }
        else
        {
            currentWalkSound = walkSound[0];
        }
    }

    private void FixedUpdate()
    {
        IsGround();
        if (canMove)
        {
            TryJump();

            Move();
            if (theCamera != null)
            {
                CameraRotation();

            }
            CharacterRotation();
        } 
    }
    
    // 지면 체크.
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + groundDetectDistance);
        isGround = true;
    }
    

    // 점프 시도
    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            isGround = false;
            Jump();
        }
    }

    // 점프
    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
        
    }
    


    private void Move()
    {
        

        moveDirX = Input.GetAxisRaw("Horizontal");
        moveDirZ = Input.GetAxisRaw("Vertical");


        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * walkSpeed;
        if (velocity.magnitude > Mathf.Abs(0.01f))
            isMoving = true;
        else
            isMoving = false;
        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
        if(isMoving&&isGround)
        {
            if (!currentWalkSound.isPlaying)
            {
                currentWalkSound.Play();
            }
            
        }
        else
        {
            currentWalkSound.Stop();
        }
    }

    //애니메이션 스크립트에서 상태를 받아오기 위한 get함수
    public bool GetMovingState()
    {
        return isMoving;
    }
    public bool GetGroundState()
    {
        return isGround;
    }
    public Vector3 GetDirectionVector()
    {
        return new Vector3(moveDirX,0,moveDirZ);
    }

    //좌우 캐릭터 회전
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationY));
        

    }

    //상하 카메라 회전
    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
