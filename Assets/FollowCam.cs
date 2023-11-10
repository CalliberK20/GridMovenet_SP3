using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float speed = 2;
    private Vector3 vec;
    [Space, Header("Border Setting")]
    public bool withBorder;
    public Vector2 center;
    public Vector2 border;

    // Update is called once per frame
    void Update()
    {
        Vector3 vector = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, vector, ref vec, speed);

        if (withBorder)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -border.x, border.x), Mathf.Clamp(transform.position.y, -border.y, border.y), -10f);
        }
    }

    private void OnDrawGizmos()
    {
        if(withBorder)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(center, 0.5f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(center, border);
        }
    }
}