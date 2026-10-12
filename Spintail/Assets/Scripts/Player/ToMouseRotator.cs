using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMouseRotator : MonoBehaviour
{
    private Camera cam;
    public Vector3 difference;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition - new Vector3(0, 0, Camera.main.transform.position.z));
        Debug.DrawLine(transform.position, mouseToWorld);
        mouseToWorld.z = 0f;
        difference = mouseToWorld - transform.position;
        difference.Normalize();

        //Debug.Log("DIRREFENCE IS " + difference);

        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 45);
    }
}
