using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitbox
{
    float DamageModifier{
        get; set;
    }
    void OnTriggerEnter(Collider other);

}
