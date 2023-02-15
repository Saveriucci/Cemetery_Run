using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }


    void LateUpdate()
    {
        Vector3 nuovaPosizioneDellaCamera = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, nuovaPosizioneDellaCamera, 10 * Time.deltaTime);
    }
}
