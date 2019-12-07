using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform npc;
    private Transform player;
    private float Dist;
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        npc = this.transform;
    }
    void Update()
    {
        Distance();
    }
    void Distance()
       {
           for (var i = 0; i < 4; i++)
           {
               Dist = Vector3.Distance(player.position, npc.position);

               if (Dist < 5)
               {
                //    Debug.Log("You're close");
                   LookAt();
               }
           }
       }

    void LookAt()
    {
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
