using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        EventManager.PostDamageEvent += LoseHealth;
    }

    private void OnDisable()
    {
        EventManager.PostDamageEvent -= LoseHealth;
    }

    public void LoseHealth()
    {

        Debug.Log("UI Manager damage");
    }
}
