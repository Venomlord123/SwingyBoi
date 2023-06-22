using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class pointer : MonoBehaviour
{
    public Vector3 mouse;
    private Ray castPoint;
    public Vector3 direction;
    public float rotationSpeed;
    private Quaternion lookRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouse = Input.mousePosition;
        castPoint = Camera.main.ScreenPointToRay(mouse);
        //direction = (castPoint. - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
