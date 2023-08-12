using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action PostDamageEvent;
    public static event Action PostLevelEndEvent;

    private void OnEnable()
    {
        Health.DamageEvent += Health_DamageEvent;
        Goal.LevelCompleteEvent += LevelCompleteEvent;
    }

    

    private void OnDisable()
    {
        Health.DamageEvent -= Health_DamageEvent;
        Goal.LevelCompleteEvent -= LevelCompleteEvent;
    }
    private void LevelCompleteEvent()
    {
        PostLevelEndEvent?.Invoke();
    }

    private void Health_DamageEvent()
    {
        PostDamageEvent?.Invoke();
        Debug.Log("EventManager damage");
    }
}
