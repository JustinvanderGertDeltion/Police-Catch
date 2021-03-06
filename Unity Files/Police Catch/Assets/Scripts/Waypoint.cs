﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public GameObject policeStation;
    public GameObject dealer;
    public float turnSpeed;

	void Update ()
    {
        dealer = GameObject.FindGameObjectWithTag("Dealer");

        if (dealer != null && dealer.GetComponent<Dealer>().isCaught == false)
        {
            Vector3 targetDir = dealer.transform.position - transform.position;
            float step = turnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        else if(dealer == null)
        {
            Vector3 targetDir = policeStation.transform.position - transform.position;
            float step = turnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}