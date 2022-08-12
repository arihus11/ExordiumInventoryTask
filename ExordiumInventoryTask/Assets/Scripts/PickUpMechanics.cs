using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMechanics : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(CollisionDetection._pickUpEnabled)
            {
                Debug.Log("Picked up");
            }
        }
    }
}
