﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.AI;

public class RobotMovement : MonoBehaviour
{
       public Transform goal;
       
       void Start () {
          NavMeshAgent agent = GetComponent<NavMeshAgent>();
          agent.destination = goal.position; 
       }
}
