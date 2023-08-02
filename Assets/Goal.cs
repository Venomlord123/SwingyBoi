using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject loader;
    public int nextLevel;
    // Start is called before the first frame update
    void Awake()
    {
        loader = GameObject.FindGameObjectWithTag("Manager").GetComponent<LoadingManager>().gameObject;

    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
           loader.GetComponent<LoadingManager>().NextLevel(nextLevel, LoadSceneMode.Single);
            
        }
    }
        
}

