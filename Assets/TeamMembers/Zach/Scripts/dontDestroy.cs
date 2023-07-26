using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<dontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<dontDestroy>()[i] != this)
            {
                if (Object.FindObjectsOfType<dontDestroy>()[i].name == gameObject.name)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
