using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    //public GameObject loader;
    public int nextLevel;


    public static event Action LevelCompleteEvent;

    // Start is called before the first frame update
    void Awake()
    {
        //loader = GameObject.FindGameObjectWithTag("Manager").GetComponent<LoadingManager>().gameObject;

    }

    // Update is called once per frame
   /* public void OnTriggerEnter(Collider other)
    {
        
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //loader.GetComponent<LoadingManager>().NextLevel();

            Debug.Log("Trigger Active");
            //Updated to use events instead of get component 
            LevelCompleteEvent?.Invoke();
        }
    }
}

