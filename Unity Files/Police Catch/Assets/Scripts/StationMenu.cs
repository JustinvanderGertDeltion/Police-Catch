﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class StationMenu : MonoBehaviour
{
    public List<string> objective = new List<string>();
    public List<string> details = new List<string>();
    public List<GameObject> spawnPoints = new List<GameObject>();

    public Dealer dealerScript;

    public GameObject stationCameraSpot;
    public GameObject garageCameraSpot;
    public GameObject stationCanvas;
    public GameObject dealerPrefab;
    public GameObject firstMenu;
    public GameObject dealer;
    public GameObject player;

    public GameObject panel1;
    public GameObject panel2;
    public GameObject panel3;

    public GameObject carSpot;
    public GameObject cruiser;
    public GameObject scooter;
    public GameObject van;
    
    public bool missionButton;
    public bool loadoutButton;
    public bool garageButton;
    public bool inMenu;

    public Text missionDetails;
    private int listCount = 0;

	void Start ()
    {
        stationCameraSpot.SetActive(false);
        dealer = GameObject.FindGameObjectWithTag("Dealer");
        dealerScript = dealer.GetComponent<Dealer>();
	}
	
	void Update ()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 8 & inMenu == false & Input.GetButtonDown("E"))
        {
            stationCameraSpot.SetActive(true);
            stationCanvas.SetActive(true);
            inMenu = true;
        }
        else if (inMenu == true & Input.GetButtonDown("E"))
        {
            stationCameraSpot.SetActive(false);
            stationCanvas.SetActive(false);
            inMenu = false;

            //Om garage uit te zetten.
            firstMenu.SetActive(true);
            garageCameraSpot.SetActive(false);
            garageButton = false;
            panel3.SetActive(false);
        }
    }

    //Buttons in menu.
    public void MissionsClicked()
    {
        missionButton = !missionButton;
        panel1.SetActive(missionButton);

        if(loadoutButton == true || garageButton == true)
        {
            loadoutButton = false;
            garageButton = false;
            panel1.SetActive(missionButton);
            panel2.SetActive(loadoutButton);
            garageCameraSpot.SetActive(garageButton);
        }
    }
    public void LoadoutClicked()
    {
        loadoutButton = !loadoutButton;
        panel2.SetActive(loadoutButton);

        if (missionButton == true || garageButton == true)
        {
            missionButton = false;
            garageButton = false;
            panel1.SetActive(missionButton);
            panel2.SetActive(loadoutButton);
            garageCameraSpot.SetActive(garageButton);
        }
    }
    public void GarageClicked()
    {
        garageButton = !garageButton;
        garageCameraSpot.SetActive(garageButton);
        firstMenu.SetActive(false);
        panel3.SetActive(true);

        if (loadoutButton == true || missionButton == true)
        {
            loadoutButton = false;
            missionButton = false;
            panel1.SetActive(missionButton);
            panel2.SetActive(loadoutButton);
            garageCameraSpot.SetActive(garageButton);
        }
    }
    public void BackClicked()
    {
        firstMenu.SetActive(true);
        garageCameraSpot.SetActive(false);
        garageButton = false;
        panel3.SetActive(false);
    }

    public void ScooterClicked()
    {
        scooter.transform.position = carSpot.transform.position;
        scooter.transform.rotation = carSpot.transform.rotation;
        scooter.SetActive(true);
        if(cruiser.activeSelf || van.activeSelf)
        {
            cruiser.SetActive(false);
            van.SetActive(false);
        }
    }
    public void CruiserClicked()
    {
        cruiser.transform.position = carSpot.transform.position;
        cruiser.transform.rotation = carSpot.transform.rotation;
        cruiser.SetActive(true);
        if (scooter.activeSelf || van.activeSelf)
        {
            scooter.SetActive(false);
            van.SetActive(false);
        }
    }
    public void VanClicked()
    {
        van.transform.position = carSpot.transform.position;
        van.transform.rotation = carSpot.transform.rotation;
        van.SetActive(true);
        if (scooter.activeSelf || cruiser.activeSelf)
        {
            scooter.SetActive(false);
            cruiser.SetActive(false);
        }
    }

    public void MissionClicked()
    {
        int i = Random.Range(0, spawnPoints.Count);
        Instantiate(dealerPrefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);

        dealer = GameObject.FindGameObjectWithTag("Dealer");
        dealerScript = dealer.GetComponent<Dealer>();
    }

    public void Mission1Hover()
    {
        missionDetails.text = (details[listCount]);
    }
    public void Mission2Hover()
    {
        missionDetails.text = (details[listCount +1]);
    }
    public void Mission3Hover()
    {
        missionDetails.text = (details[listCount +2]);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (dealerScript.GetComponent<Dealer>().isCaught)
        {
            dealerScript.GetComponent<Dealer>().done = true;
            dealerScript.GetComponent<Dealer>().dealerImage.SetActive(false);
            Destroy(dealer);
        }
    }
}