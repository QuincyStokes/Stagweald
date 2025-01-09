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
        if(!loaded)
        {
            ammo -= 1;
            //do some animation
            loaded = true;
            currentBolt = Instantiate(bolt, boltSpawnPoint.position, boltSpawnPoint.rotation);
            currentBolt.transform.SetParent(transform);
            //boltSpawnPoint.transform.SetParent(currentBolt.transform);
            Rigidbody rb = currentBolt.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        UpdateAmmoUI();
       
    }


    void Fire()
    {
        if(loaded)
        {
            loaded = false;
            //do shoot animation
            Rigidbody rb = currentBolt.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            rb.AddForce(transform.forward * boltSpeed, ForceMode.Impulse);
            StartCoroutine(BoltDecay(currentBolt));
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

    public IEnumerator BoltDecay(GameObject bolt)
    {
        yield return new WaitForSeconds(5f);
        Destroy(bolt);
    }


}
