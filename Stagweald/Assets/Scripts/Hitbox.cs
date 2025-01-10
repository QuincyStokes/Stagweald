using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour, IHitbox
{

    [SerializeField]
    private float damageModifier;
    public float DamageModifier { get => damageModifier; set => damageModifier = value; }

    public void OnTriggerEnter(Collider other)
    {
        print("Hit");
        if (other.CompareTag("Bolt"))
        {
            print(" by a bolt");
            Bolt bolt = other.GetComponent<Bolt>();
            if(bolt != null)
            {
                print("Deer took " + DamageModifier * bolt.damage + " damage in the " + this.name);
                GetComponentInParent<Deer>().TakeDamage(DamageModifier * bolt.damage);
            }
        }
    }
}
