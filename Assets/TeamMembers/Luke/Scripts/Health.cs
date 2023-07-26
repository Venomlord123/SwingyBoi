using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public static event Action DamageEvent;

    public bool health = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Damage();
    }

    public void Damage()
    {
        if(!health)
        {
            DamageEvent?.Invoke();
            Debug.Log("Health script damage ");
            health = true;
        }
    }
}
