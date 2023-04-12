using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float left = 0f;
        if (Input.GetKeyDown("o"))
        {
            left  = 20.0f;
        }
        float right = 0.0f;
        if (Input.GetKeyDown("p"))
        {
            right = 20.0f;
        }
        Vector3 movement = new Vector3(left, right, 0.0f);
        transform.position = transform.position + movement * Time.deltaTime;
    }
}
