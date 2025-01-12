using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    public float movementError;


    [Header("UI References")]
    public TMP_Text loadedAmmo;
    public TMP_Text reserveAmmo;


    [Header("Keybinds")]
    public KeyCode fireKeycode;
    public KeyCode reloadKeycode;
    public KeyCode aimDownSightsKeycode;


    [Header("Animation")]
    public Animator animator;
    public Animation crossbowAnimation;

    [Header("References")]
    public Rigidbody playerRb;


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

        if(Input.GetKeyDown(aimDownSightsKeycode))
        {
            ADS();
        }
        if(Input.GetKeyUp(aimDownSightsKeycode))
        {
            UnADS();
        }
    }

    void Reload()
    {
        if(!loaded && ammo >= 1)
        {
            ammo -= 1;
            //do some animation
            crossbowAnimation.Play("Reload");
            loaded = true;
            currentBolt = Instantiate(bolt, boltSpawnPoint.position, boltSpawnPoint.rotation);
            currentBolt.transform.SetParent(transform);
            //boltSpawnPoint.transform.SetParent(currentBolt.transform);
            Rigidbody rb = currentBolt.GetComponentInChildren<Rigidbody>();
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
            Rigidbody rb = currentBolt.GetComponentInChildren<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            //movement penalty, apply a small random offset to projectile direction
            rb.AddForce(transform.forward * boltSpeed, ForceMode.Impulse);
            crossbowAnimation.Play("Fire");
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

    void ADS()
    {
        animator.SetBool("ADS", true);
    }

    void UnADS()
    {
        animator.SetBool("ADS", false);
    }


}
