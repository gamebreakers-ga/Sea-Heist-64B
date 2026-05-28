using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 StartPoint;
    private Vector3 EndPoint;
    public float Range;
    // Start is called before the first frame update
    void Start()
    {
        StartPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, EndPoint, 1);
        if (Vector3.Distance(transform.position, EndPoint) < 1 || Vector3.Distance(transform.position, StartPoint) >= Range)
        {
            Destroy(gameObject);
        }
    }

    public void SetEndPoint(Vector3 endPoint, float range)
    {
        EndPoint = new Vector3(endPoint.x, endPoint.y, endPoint.z);
        Range = range;
    }
}
