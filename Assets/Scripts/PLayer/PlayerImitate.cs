using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerImitate : MonoBehaviour
{
    public Transform player;
    public LayerMask lyrMsk;



    void Update()
    {
        this.transform.position = player.position;
        GetComponent<CinemachineFreeLook>().m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
        //RaycastHit hit;
        //Ray ray = new Ray(transform.position, -transform.up);

        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, lyrMsk))
        //{
        //    transform.up = hit.normal;
        //}

        //transform.rotation = Quaternion.LookRotation(player.up, player.right);
        //transform.rotation = Quaternion.AngleAxis(-90f, transform.forward);

        this.transform.up = player.up;
        //transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.deltaTime * 10f);
        //transform.localEulerAngles = new Vector3(player.transform.localEulerAngles.x, 0f, player.transform.localEulerAngles.z);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x)
    }
}
