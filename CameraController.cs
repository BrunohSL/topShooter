using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform trackingTarget;

    public float xOffset;
    public float yOffset;
    public float followSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xTarget = trackingTarget.position.x + xOffset;
        float yTarget = trackingTarget.position.y + yOffset;

        float newX = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
        float newY = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * followSpeed);

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
