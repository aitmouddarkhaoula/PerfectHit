using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public float floorLenght;
    public float floorWidth;

    // Start is called before the first frame update
    void Start()
    {
        floorWidth = transform.localScale.x / 2;
        floorLenght = transform.localScale.z;
    }


    // Update is called once per frame
    void Update()
    {
        

    }
}
