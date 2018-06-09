using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]    //show up in Inspector
    private float speed = 5f;
    [SerializeField]    //show up in Inspector
    private float sensitivity = 3f;
    [SerializeField]    //show up in Inspector
    private float thForce = 1000f;

    [Header("Spring settings:")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;



    private PlayerMotor playerMotor;
    private ConfigurableJoint joint;

    void Start()
    {
        playerMotor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);

    }

    void Update()
    {
        //mouvement velocity as 3d vector
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        Vector3 horizontalMovement = transform.right * xMovement; //(-1/1, 0, 0)
        Vector3 forwardMovement = transform.forward * zMovement;  //(0, 0, -1/1)

        //movement vector
        Vector3 velocityVector = (horizontalMovement + forwardMovement).normalized * speed;     //allways get a vector with lenght of 1(normalized): uses hor+for just as direction

        //applying vector
        playerMotor.Move(velocityVector);

        //rotation as 3d vector(just turning)          //only for x axis (y for camera)
        float yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRotation, 0f) * sensitivity;

        //applying rotation
        playerMotor.Rotate(rotation);

        //camera rotation as 3d vector
        float xRotation = Input.GetAxisRaw("Mouse Y");

        float xCameraRotation = xRotation * sensitivity;

        //applying camera rotation
        playerMotor.RotateCamera(xCameraRotation);

        //thruster force
        Vector3 th_Force = Vector3.zero;    //(0f, 0f, 0f)
        if (Input.GetButton("Jump"))
        {
            th_Force = Vector3.up * thForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        //applying thruster force
        playerMotor.ApplyThForce(th_Force);


    }


    private void SetJointSettings(float _jointPosition)
    {
        joint.yDrive = new JointDrive { mode = jointMode, positionSpring = _jointPosition, maximumForce =jointMaxForce };

    }




}
