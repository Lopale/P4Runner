using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaPlane : MonoBehaviour
{

    private float posY = -10f;
    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(0, posY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        IDistance distance = ServicesLocator.GetService<IDistance>();
        transform.position = new Vector3(0, transform.position.y, distance.Get());

    }
}
