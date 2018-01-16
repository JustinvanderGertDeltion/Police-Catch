﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    [SerializeField] Transform Player;
    NavMeshAgent agent;

    public Text pressToCatch;

    public float clickCountdown;
    public float catchRange;
    public float range = 30;
    public int health = 4;
    float multiplier = 0.2f;

    public float totalClicks = 0;
    float requiredClicks = 10;

    bool isDowned;
    public bool isCaught;
    public bool done;
    public bool isTazed;
    public GameObject dealer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        dealer.SetActive(false);
    }

    void Update()
    {
        Vector3 runTo = transform.position + ((transform.position - Player.position) * multiplier);
        float distance = Vector3.Distance(transform.position, Player.position);
        if (distance < range) agent.SetDestination(runTo);

        if (distance <= catchRange & isDowned || distance <= catchRange & isTazed)
        {
            pressToCatch.text = ("Press Q repeatedly to catch Dealer");
            if (Input.GetButtonDown("Q")) { totalClicks += 1; }
        }
        else { pressToCatch.text = (""); }

        if (totalClicks >= 0) { totalClicks -= 1 * (Time.deltaTime * clickCountdown); }
        if (totalClicks >= requiredClicks) { Caught(); }
        Debug.Log(totalClicks);
    }

    void Caught()
    {   
        isCaught = true;
        pressToCatch.text = ("");
        totalClicks = 0;
        gameObject.SetActive(false);
        dealer.SetActive(true);
        gameObject.GetComponentInChildren<CatchBar>().StopBar();
    }
}