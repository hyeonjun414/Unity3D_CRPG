using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null && Vector3.Distance(transform.position, target.position) > 5f)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.Lerp(transform.position, target.position+Vector3.up, Time.deltaTime/2);
            
        }
        transform.LookAt(target.position + Vector3.up);
    }
}
