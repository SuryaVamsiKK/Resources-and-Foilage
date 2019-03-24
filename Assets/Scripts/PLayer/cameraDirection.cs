using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDirection : MonoBehaviour
{
    public Transform tracker;
    public Transform player;

    public void Start()
    {
        player = GameObject.FindObjectOfType<Player>().transform;
        tracker = player.GetComponent<Player>().tracker;
    }

    public void Update()
    {
        Vector3 playerLocalPos = player.InverseTransformPoint(transform.position);
        tracker.transform.position = player.TransformPoint(-playerLocalPos.x, 0, -playerLocalPos.z);
        track();
    }

    private void OnDrawGizmos()
    {
    }

    void track()
    {
        //Vector3 playerLocalPos = player.InverseTransformPoint(transform.position);
        //tracker.transform.position = player.TransformPoint(-playerLocalPos.x, 0, -playerLocalPos.z);
        //tracker.position = player.position;

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(player.TransformPoint(0, 0, 0), tracker.transform.position);
        tracker.forward = tracker.transform.position - player.TransformPoint(0, 0, 0);
        //Quaternion test = Quaternion.LookRotation(tracker.transform.position - player.TransformPoint(0, 0, 0), player.up);
        //tracker.rotation = test;
        //tracker.localEulerAngles = new Vector3(tracker.localEulerAngles.x, tracker.localEulerAngles.y, 0f);
        //tracker.up = Vector3.Cross(player.up , transform.up);

        //Vector3 upProjected = Vector3.ProjectOnPlane(tracker.transform.position - player.TransformPoint(0, 0, 0), player.up).normalized; // makes sure the vectors are perpendicular. You can skip this if you are already sure they are.
        tracker.rotation = Quaternion.LookRotation(tracker.transform.position - player.TransformPoint(0, 0, 0), player.up); // sets the forward vector to desiredForwardVector and the up vector to rightProjected. Now we need to rotate it so the right vector is at rightProjected
        //tracker.rotation *= Quaternion.AngleAxis(90f/*this might need to be positive*/, tracker.forward);
    }
}
