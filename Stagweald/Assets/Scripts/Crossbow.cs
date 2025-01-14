using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.Profiling;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    [Header("Ammo Specs")]
    //public float ammo; //this is more of an inventory thing..?
    private bool loaded; 
    public GameObject bolt;
    public Transform boltSpawnPoint;
    public float boltSpeed;
    private GameObject currentBolt;

    [Header("Crossbow Settings")]
    public float movementError;
    public float reloadCooldown;
    public float fireCooldown;
    private bool canReload;
    private bool canFire;
    public bool isUpgraded;


    [Header("UI References")]
    public TMP_Text loadedAmmo;
    public TMP_Text reserveAmmo;
    public GameObject scopeUI;


    [Header("Keybinds")]
    public KeyCode fireKeycode;
    public KeyCode reloadKeycode;
    public KeyCode aimDownSightsKeycode;


    [Header("Animation")]
    public Animator animator;
    public Animation crossbowAnimation;
    

    [Header("References")]
    public Rigidbody playerRb;
    public GameObject scope;
    
    [Header("Camera Settings")]
    public Camera mainCamera;
    public float cameraScopeFOV;
    private float baseScopeFOV;
    public bool hasScope;


    void Start()
    {
        loaded = false;
        UpdateAmmoUI();
        baseScopeFOV = mainCamera.fieldOfView;
        canFire = true;
        canReload = true;
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
        if(!loaded && InventoryManager.Instance.numBolts >= 1 && canReload)
        {
            canFire = false;
            InventoryManager.Instance.SubtractBolts(1);
            //do some animation
            crossbowAnimation.Play("Reload");
            loaded = true;
            currentBolt = Instantiate(bolt, boltSpawnPoint.position, boltSpawnPoint.rotation);
            currentBolt.transform.SetParent(transform);
            //boltSpawnPoint.transform.SetParent(currentBolt.transform);
            Rigidbody rb = currentBolt.GetComponentInChildren<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            canReload = false;
            StartCoroutine(FireTimer());
            
        }
        UpdateAmmoUI();
       
    }


    void Fire()
    {
        if(loaded && canFire)
        {
            canReload = false;
            loaded = false;
            //do shoot animation
            Rigidbody rb = currentBolt.GetComponentInChildren<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;
            //movement penalty, apply a small random offset to projectile direction
            rb.AddForce(transform.forward * boltSpeed, ForceMode.Impulse);
            crossbowAnimation.Play("Fire");
            StartCoroutine(BoltDecay(currentBolt));
            StartCoroutine(ReloadTimer());
        }
        UpdateAmmoUI();
    
    }

    public void UpdateAmmoUI()
    {
        reserveAmmo.text = InventoryManager.Instance.numBolts.ToString();
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
        if(hasScope)
        {
            StartCoroutine(ScopeUIDelay(.067f));
        }
    }

    void UnADS()
    {
        animator.SetBool("ADS", false);
        if(hasScope)
        {
            scope.SetActive(true);
            scopeUI.SetActive(false);
            mainCamera.fieldOfView = baseScopeFOV;
        }
        
    }

    private IEnumerator ScopeUIDelay(float time)
    {
        yield return new WaitForSeconds(time);
        scope.SetActive(false);
        scopeUI.SetActive(true);
        mainCamera.fieldOfView = cameraScopeFOV;
    }

    private IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(reloadCooldown);
        canReload = true;
    }

    private IEnumerator FireTimer()
    {
        yield return new WaitForSeconds(fireCooldown);
        canFire = true;
    }



}
