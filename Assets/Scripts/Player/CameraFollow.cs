using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundPlayer))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 10;
    public Vector3 offset;
    public Vector3 velocity;

    public static CameraFollow Instance;
    public SoundPlayer globalSound;

    private void Awake()
    {
        Instance = this;
        globalSound = GetComponent<SoundPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            print("Target not found to follow!");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, desiredPos, ref velocity, smoothSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }
}
