using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Hitbox : MonoBehaviour, IHitbox
{

    [SerializeField]
    private float damageModifier;
    public float DamageModifier { get => damageModifier; set => damageModifier = value; }

    private PlayerMovement player;
    [SerializeField] AudioClip hitmark;
    [SerializeField] AudioMixerGroup audioMixerGroup;


    
        
    public void OnTriggerEnter(Collider other)
    {
        print("Hit");
        if (other.CompareTag("Bolt"))
        {
            print(" by a bolt");
            Bolt bolt = other.GetComponent<Bolt>();
            AudioManager.Instance.PlayOneShot(hitmark, 1f, audioMixerGroup);
            if(bolt != null)
            {
                print("Deer took " + DamageModifier * bolt.damage + " damage in the " + this.name);
                player = GameObject.FindObjectOfType<PlayerMovement>();

                GetComponentInParent<SmartDeer>().TakeDamage(DamageModifier * bolt.damage, player.transform.position);
                Destroy(bolt.gameObject);
            }
        }
    }
}
