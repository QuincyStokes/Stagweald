using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [Header("Ammo Specs")]
    public float ammo; //this is more of an inventory thing..?
    private bool loaded; 
    public GameObject bolt;
    public Transform boltSpawnPoint;
    public float boltSpeed;
    private GameObject currentBolt;


    [Header("UI References")]
    public TMP_Text loadedAmmo;
    public TMP_Text reserveAmmo;


    [Header("Keybinds")]
    public KeyCode fireKeycode;
    public KeyCode reloadKeycode;



    void Start()
    {
        loaded = false;
        UpdateAmmoUI();
    }
    void Update()
    {
        InputCheck();
    }

    void InputCheck()
    {
        if(Input.GetKeyDown(fireKeycode))
        {
            Fire();
        }

        if(Input.GetKeyDown(reloadKeycode))
        {
            Reload();
        }
    }

    void Reload()
    {
        print("Reloading");
        if(!loaded)
        {
            print("Reload Success");
            ammo -= 1;
            //do some animation
            loaded = true;
            currentBolt = Instantiate(bolt, boltSpawnPoint.position, boltSpawnPoint.rotation);
            currentBolt.transform.SetParent(transform);
            //boltSpawnPoint.transform.SetParent(currentBolt.transform);
            currentBolt.GetComponent<Rigidbody>().useGravity = false;
        }
        UpdateAmmoUI();
       
    }


    void Fire()
    {
        print("Firing");
        if(loaded)
        {
            print("Fire Success");
            loaded = false;
            //do shoot animation
            currentBolt.GetComponent<Rigidbody>().useGravity = true;
            currentBolt.GetComponent<Rigidbody>().AddForce(transform.forward * boltSpeed, ForceMode.Impulse);
        }
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        reserveAmmo.text = ammo.ToString();
        if(loaded)
        {
            loadedAmmo.text = "1";
        }
        else 
        {
           loadedAmmo.text = "0";
        }
        
    }

    

}
