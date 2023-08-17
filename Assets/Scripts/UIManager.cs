using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{

    public Canvas CompleteLevel;
    public Canvas PauseMenu;
    public Canvas MainMenu;

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
        EventManager.onGoalCheck += CompleteLevelUI;
    }

    

    private void OnDisable()
    {
        EventManager.PostDamageEvent -= LoseHealth;
        EventManager.onGoalCheck -= CompleteLevelUI;
    }

    private void CompleteLevelUI()
    {
        CompleteLevel.gameObject.SetActive(true);
    }


    public void LoseHealth()
    {

        Debug.Log("UI Manager damage");
    }
}
