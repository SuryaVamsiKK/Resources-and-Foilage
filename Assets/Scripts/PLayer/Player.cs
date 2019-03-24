using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool lookat;
    
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        var locVel = transform.InverseTransformDirection(playerBody.velocity);
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //playerBody.velocity = transform.TransformDirection(inputVector);
        transform.Translate(inputVector);
        
        locVel.x = 0;
        locVel.z = 0;
        playerBody.velocity = transform.TransformDirection(locVel);        
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
