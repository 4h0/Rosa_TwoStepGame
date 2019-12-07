using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class indicatorMovement_jenn : MonoBehaviour
{
    public float min = 2f;
    public float max = 3f;
    // Use this for initialization
    void Start()
    {

        min = transform.position.y;
        max = transform.position.y + 3;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * 2, max - min) + min, transform.position.z);

    }
}
