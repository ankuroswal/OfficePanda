using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PawMovement : MonoBehaviour {

    public float sensitivity = 1;
    public Rigidbody rb;
    public Vector3 movement;
    private Transform myTransform;

    public Vector3 limitLow;
    public Vector3 limitHigh;

    public float upDownMovementSpeed = 2f;
    public float wristMovementSpeed;
    public float armRotateSpeed;
    public float armRotationDamping;

    public float armRotateLeft;
    public float armRotateRight;
    public float armRotationError = 5.0f;

    public float mouseSpeedLimitLow = 30f;
    public float mouseSpeedLimitHigh = 100f;

    private float mouseSpeed;
    private float targetMouseSpeed;
    private float armRotation;

    private Dictionary<ActionEvent, Rigidbody> previousObjects;

    public Collider grabCollider;
    public Transform paw;
    public List<ActionEvent> grabbedItems = new List<ActionEvent>();

    
    // Use this for initialization
    void Awake ()
    {
        previousObjects = new Dictionary<ActionEvent, Rigidbody>();
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
        if (Input.GetMouseButton(0))
        {
            grabCollider.enabled = true;
            
            for (int i = 0; i < grabbedItems.Count; i++)
            {
                grabbedItems[i].transform.SetParent(transform);
                Destroy(grabbedItems[i].GetComponent<Rigidbody>());
                //Rigidbody itemRB = GetRigidbody(grabbedItems[i]);
                //itemRB.isKinematic = true;
                //itemRB.useGravity = false;

                //grabbedItems[i].GetComponent<Rigidbody>().velocity = rb.velocity;
                //grabbedItems[i].GetComponent<Transform>().Rotate(0, 0, armRotation);
                

            }
        }
        else
        {
            grabCollider.enabled = false;
            for (int i = 0; i < grabbedItems.Count; i++)
            {
                grabbedItems[i].transform.SetParent(null);
                grabbedItems[i].gameObject.AddComponent<Rigidbody>();
                //Rigidbody itemRB2 = GetRigidbody(grabbedItems[i]);
                //itemRB2.useGravity = true;
                //itemRB2.isKinematic = false;

                //grabbedItems[i].GetComponent<Rigidbody>().isKinematic = false;

            }
            grabbedItems.Clear();
        }

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

        float d = Input.GetAxis("Mouse ScrollWheel");
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

        if (armRotation + myTransform.eulerAngles.z < armRotateLeft
            || armRotation + myTransform.eulerAngles.z > armRotateRight)
            myTransform.Rotate(0, 0, armRotation);
        else // we are either trying to go out of bounds, or are currently out of bounds
        {
            float distance = Mathf.Abs(myTransform.eulerAngles.z - armRotateLeft);
            if (distance <= armRotationError)
                myTransform.Rotate(0, 0, distance);
            distance = Mathf.Abs(armRotateRight - myTransform.eulerAngles.z);
            if (distance <= armRotationError)
                myTransform.Rotate(0, 0, -1 * distance);
        }

        Vector3 pos = myTransform.position;
        if (pos.x < limitLow.x) pos.x = limitLow.x;
        if (pos.y < limitLow.y) pos.y = limitLow.y;
        if (pos.z < limitLow.z) pos.z = limitLow.z;
        if (pos.x > limitHigh.x) pos.x = limitHigh.x;
        if (pos.y > limitHigh.y) pos.y = limitHigh.y;
        if (pos.z > limitHigh.z) pos.z = limitHigh.z;

        myTransform.position = pos;
        paw = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        ActionEvent interactable = other.gameObject.GetComponent<ActionEvent>();
        if (interactable != null)
        {
            if (!grabbedItems.Contains(interactable))
                grabbedItems.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ActionEvent interactable = other.gameObject.GetComponent<ActionEvent>();
        if (interactable != null)
        {
            grabbedItems.Remove(interactable);
            interactable.transform.SetParent(null);
            interactable.gameObject.AddComponent<Rigidbody>();
            //Rigidbody rb = GetRigidbody(interactable);
            //rb.useGravity = true;
            //rb.isKinematic = false;
        }
    }

    private Rigidbody GetRigidbody(ActionEvent ae)
    {
        Rigidbody r;
        if (previousObjects.TryGetValue(ae, out r))
            return r;
        else
        {
            r = ae.GetComponent<Rigidbody>();
            if (r == null)
                Debug.LogError("This object '" + ae.gameObject.name + "' doesn't have a Rigidbody!");
            previousObjects.Add(ae, r);
            return r;
        }
    }
}
