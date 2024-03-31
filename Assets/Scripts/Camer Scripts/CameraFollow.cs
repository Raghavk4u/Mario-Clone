using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float resetSpeed = 0.5f, cameraSpeed = 0.3f;

    public Bounds cameraBound;
    private Transform target;

    private float offsetZ;
    private Vector3 lastTargetPosition, currentVelocity;

    private bool followPlayer;

    private void Awake()
    {
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 12f);
        cameraBound = myCol.bounds;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag(Mytags.PLAYER_TAG).transform;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - lastTargetPosition).z;
        followPlayer = true;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (followPlayer)
        {
            Vector3 aheadTarget = target.position + Vector3.forward * offsetZ;

            if(aheadTarget.x >= transform.position.x)
            {
                Vector3 newcameraPosition = Vector3.SmoothDamp(transform.position, aheadTarget, ref currentVelocity, cameraSpeed);
                transform.position = new Vector3(newcameraPosition.x, transform.position.y, newcameraPosition.z);

                lastTargetPosition = target.position;
            }
        }
    }
}
