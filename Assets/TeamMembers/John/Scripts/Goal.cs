using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
	public GameObject loader;
	public int nextLevel;

	public static event Action LevelCompleteEvent; // Corrected event name

	/*private void OnEnable()
	{
		EventManager.onGoalCheck += CompleteLevel;
	}

	private void OnDisable()
	{
		EventManager.onGoalCheck -= CompleteLevel;
	}*/

	// Start is called before the first frame update
	void Awake()
	{
		//loader = GameObject.FindGameObjectWithTag("Manager").GetComponent<LoadingManager>().gameObject;
	}

	/*void CompleteLevel()
	{
		LevelCompleteEvent?.Invoke();
		Debug.Log("Load Event Invoked");
	}*/

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			LevelCompleteEvent?.Invoke();
		}
	}
}