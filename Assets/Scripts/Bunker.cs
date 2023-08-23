using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public int lifes;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            lifes--;
            if(lifes <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
