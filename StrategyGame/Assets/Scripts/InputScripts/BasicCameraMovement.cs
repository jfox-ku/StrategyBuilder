using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraMovement : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if(x!=0 || y != 0) {
            this.transform.position += new Vector3(x, y, 0);
            this.transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x, -10, 10),
            Mathf.Clamp(this.transform.position.y, -10, 10),
            this.transform.position.z);

        }

    }
}
