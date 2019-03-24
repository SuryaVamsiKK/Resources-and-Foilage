using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    private Rigidbody playerBody;
    private Vector3 inputVector;

    [HideInInspector] public bool enableValues = false;
    [ConditionalHide("enableValues")] public float retroThrustYaw = 1;
    [ConditionalHide("enableValues")] public float retroThrustPitch = 1;
    [ConditionalHide("enableValues")] public float retroThrustRoll = 1;
    [ConditionalHide("enableValues")] public float updateValue = 0.1f;
    public float maxRetroThrust = 3f;
    public float hoverHeight = 5f;
    public Transform hoverStart;
    Ray ray;

    public bool lookat;
    public Transform tracker;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var locVel = transform.InverseTransformDirection(playerBody.velocity);
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //playerBody.velocity = transform.TransformDirection(inputVector);
        transform.Translate(inputVector);
        
        locVel.x = 0;
        locVel.z = 0;
        playerBody.velocity = transform.TransformDirection(locVel);

        //retroThrusterBasedMovement(-Input.GetAxis("Mouse X"), 0f, 0f, 1f);
        //transform.localEulerAngles += new Vector3(0f, Input.GetAxis("Mouse X") * Time.deltaTime * 50f, 0f);
        rotation();
    }
    
    private void OnDrawGizmos()
    {
        ray = new Ray(hoverStart.position, -this.transform.up);
        Gizmos.color = Color.red;
        //Gizmos.DrawRay(ray.origin,ray.direction * hoverHeight);
    }

    public void rotation()
    {
        //tracker.position = transform.position;
        //tracker.position = GameObject.FindObjectOfType<Camera>().transform.position;
        //tracker.position = transform.TransformPoint(new Vector3(tracker.localPosition.x, transform.localPosition.y, tracker.localPosition.z));

        //Vector3.Cross(GameObject.FindObjectOfType<Camera>().transform.right, transform.up)
        //targ = transform.localRotation;
        //Vector3 targRot = new Vector3(transform.localEulerAngles.x, tet.m_XAxis.Value, transform.localEulerAngles.z);
        //Quaternion targQ = Quaternion.Euler(targRot);
        if (lookat)
        {
            //    var dir = (tracker.position - transform.position).normalized;
            //    float step = 10f * Time.deltaTime;
            //    Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            //    newDir.x = 0f;
            //    newDir.x = 0f;
            //    //transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Time.deltaTime * 100f, 0), Space.Self);
            transform.rotation = tracker.rotation;
            //float angle = Vector3.Angle(transform.forward, tracker.forward);
            //transform.rotation = Quaternion.Lerp(transform.rotation, tracker.rotation, Time.deltaTime * 10f);
            //    transform.rotation = Quaternion.LookRotation(newDir);

            //transform.rotat (tracker.position, transform.up, 10f);
            //Quaternion _lookRot = Quaternion.LookRotation(dir);
            //Debug.Log(transform.InverseTransformPoint(_lookRot.eulerAngles));

            //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRot, Time.deltaTime * 10f);
            //transform. (tracker);
        }
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, targQ, Time.deltaTime * 10f);
        //transform.LookAt(GameObject.FindObjectOfType<Camera>().transform);
        //transform.forward = Vector3.Cross(GameObject.FindObjectOfType<Camera>().transform.right, transform.up);
    }
    


    public void retroThrusterBasedMovement(float inputRetroThrustYaw, float inputRetroThrustPitch, float inputRetroThrustRoll, float implementation)
    {
        retroThrustYaw += inputRetroThrustYaw;
        retroThrustPitch += inputRetroThrustPitch;
        retroThrustRoll += inputRetroThrustRoll;

        #region Retro Thruster Yaw Clamp
        if (retroThrustYaw > maxRetroThrust)
        {
            retroThrustYaw = maxRetroThrust;
        }
        if (retroThrustYaw < -maxRetroThrust)
        {
            retroThrustYaw = -maxRetroThrust;
        }
        #endregion

        #region Retro Thruster Roll Clamp
        if (retroThrustRoll > maxRetroThrust)
        {
            retroThrustRoll = maxRetroThrust;
        }
        if (retroThrustRoll < -maxRetroThrust)
        {
            retroThrustRoll = -maxRetroThrust;
        }
        #endregion

        #region Retro Thruster Pitch Clamp
        if (retroThrustPitch > maxRetroThrust)
        {
            retroThrustPitch = maxRetroThrust;
        }
        if (retroThrustPitch < -maxRetroThrust)
        {
            retroThrustPitch = -maxRetroThrust;
        }
        #endregion

        float yaw = retroThrustYaw * updateValue * implementation;
        float pitch = retroThrustPitch * updateValue * implementation;
        float roll = retroThrustRoll * updateValue * implementation;

        //transform.Rotate(pitch, yaw, roll);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + yaw, transform.localEulerAngles.z);
    }
}
