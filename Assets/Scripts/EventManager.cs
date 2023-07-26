using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action PostDamageEvent;

    private void OnEnable()
    {
        Health.DamageEvent += Health_DamageEvent;
    }

    private void OnDisable()
    {
        Health.DamageEvent -= Health_DamageEvent;
    }

    private void Health_DamageEvent()
    {
        PostDamageEvent?.Invoke();
        Debug.Log("EventManager damage");
    }
}
