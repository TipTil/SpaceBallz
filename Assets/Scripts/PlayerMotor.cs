using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float xCameraRotation = 0f;
    private float xCurrentCamRotation = 0f;
    private Vector3 thForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
		
    //get the movement vector
	public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //get the rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //get the rotation vector for camera
    public void RotateCamera(float _xCameraRotation)
    {
        xCameraRotation = _xCameraRotation;
    }

    //get the force vector for th
        public void ApplyThForce(Vector3 _thThForce)
    {
        thForce = _thThForce;
    }

    //runs every frame
    void FixedUpdate()
    {
        MakeMovement();
        MakeRotation();
    }


    //make mouvement based on velocity
    void MakeMovement()
    {
        if(velocity != Vector3.zero)
        {
            rBody.MovePosition(rBody.position + velocity * Time.fixedDeltaTime);
        }

        if (thForce != Vector3.zero)
        {
            rBody.AddForce(thForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    //make rotation
    void MakeRotation()
    {
        rBody.MoveRotation(rBody.rotation * Quaternion.Euler(rotation));

        if (cam != null)        //if camera exist
        {
            //set rotation
            xCurrentCamRotation -= xCameraRotation;
            xCurrentCamRotation = Mathf.Clamp(xCurrentCamRotation, -cameraRotationLimit, cameraRotationLimit); //limits the camera mouvement/rot

            //applying rot to camera transform
            cam.transform.localEulerAngles = new Vector3(xCurrentCamRotation, 0f, 0f);
        }
    }

}
