using UnityEngine;
using System.Collections;

public class PawMovement : MonoBehaviour {


    public float sensitivity = 1;
    private Rigidbody rb;
    public Vector3 movement;
    private Transform myTransform;

    public Vector3 limitLow;
    public Vector3 limitHigh;

    public float upDownMovementSpeed = 2f;
    public float wristMovementSpeed;
    public float armRotateSpeed;
    public float armRotationDamping;

    public float armRotateMin;
    public float armRotateMax;

    public float mouseSpeedLimitLow = 30f;
    public float mouseSpeedLimitHigh = 100f;

    private float mouseSpeed;
    private float targetMouseSpeed;
    private float armRotation;
    private float totalArmRotation;

    
    // Use this for initialization
    void Awake ()
    {
        rb = GetComponent<Rigidbody>();
	}
    private void Start()
    {
        targetMouseSpeed = mouseSpeed;
        myTransform = transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement);

        //if (movement != Vector3.zero)
        //    rb.AddForce(movement);
        //else
        //    rb.velocity = Vector3.zero;
        armRotation *= armRotationDamping;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            movement = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

            armRotation -= Input.GetAxis("Mouse X") * armRotateSpeed * Time.deltaTime;

        }
        else
        { 
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            movement = new Vector3(Input.GetAxis("Mouse X") * mouseSpeed, 0,
                                               Input.GetAxis("Mouse Y") * mouseSpeed);
        }



        var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            // scroll up
            movement.y = upDownMovementSpeed;
        }
        else if (d < 0f)
        {
            // scroll down
            movement.y = -upDownMovementSpeed;

        }

        targetMouseSpeed = Mathf.Clamp(targetMouseSpeed, mouseSpeedLimitLow, mouseSpeedLimitHigh);
        mouseSpeed = Mathf.Lerp(mouseSpeed, targetMouseSpeed, Time.deltaTime * 5f);
        rb.drag = mouseSpeed / 10f;
        //loweringSpeed = mouseSpeed / 2f;

        //if (myTransform.eulerAngles.z > armRotateMax)
        //{
        //    myTransform.Rotate(0, 0, armRotation);
        //}
        //else if (myTransform.eulerAngles.z < armRotateMin)
        //{
        //    myTransform.Rotate(0, 0, armRotation);
        //}

        myTransform.Rotate(0, 0, armRotation);

        Vector3 pos = myTransform.position;
        if (pos.x < limitLow.x) pos.x = limitLow.x;
        if (pos.y < limitLow.y) pos.y = limitLow.y;
        if (pos.z < limitLow.z) pos.z = limitLow.z;
        if (pos.x > limitHigh.x) pos.x = limitHigh.x;
        if (pos.y > limitHigh.y) pos.y = limitHigh.y;
        if (pos.z > limitHigh.z) pos.z = limitHigh.z;

        myTransform.position = pos;
    }
}
