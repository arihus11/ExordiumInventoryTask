using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform Player;
    public Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (Player.position.x + Offset.x, Player.position.y + Offset.y, Player.position.z +Offset.z); 
    }
}
