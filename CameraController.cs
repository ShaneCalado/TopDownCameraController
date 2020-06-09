using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int targetFramesPerSecond = 30;

    //Follow player
    private GameObject player;
    public float smoothSpeed = 0.125F;
    private float lookInfluence = 5.0f;
    private Vector3 offset = new Vector3(0, 0, -10);

    //Lock on
    public GameObject target;
    public bool lockedOn = false;

    // Start is called before the first frame update
    void Awake()
    {
        // Find player object
        player = GameObject.FindGameObjectWithTag("Player");

        // Set target fps
        Application.targetFrameRate = targetFramesPerSecond;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!lockedOn)
        {
            FollowPlayer();
        }
        else
        {
            FollowTarget(target);
        }
    }

    //Follow the player
    void FollowPlayer()
    {
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition + Look(Input.GetAxis("RJoy X"), Input.GetAxis("RJoy Y"));
    }

    // Point the camera in a specific direction;
    Vector3 Look(float x, float y)
    {
        Vector2 camAdjustment = new Vector2(x, y);

        return camAdjustment * lookInfluence;
    }

    // Follow the locked target
    void FollowTarget(GameObject target)
    {
        Vector3 midpoint = ((player.transform.position + target.transform.position) / 2) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, midpoint, smoothSpeed);
        transform.position = smoothedPosition + Look(Input.GetAxis("RJoy X"), Input.GetAxis("RJoy Y"));
    }

    public int GetTargetFPS()
    {
        return this.targetFramesPerSecond;
    }
}
