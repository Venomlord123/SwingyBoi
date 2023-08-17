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

	private void OnEnable()
	{
		EventManager.onGoalCheck += CompleteLevel;
	}

	private void OnDisable()
	{
		EventManager.onGoalCheck -= CompleteLevel;
	}

	// Start is called before the first frame update
	void Awake()
	{
		//loader = GameObject.FindGameObjectWithTag("Manager").GetComponent<LoadingManager>().gameObject;
	}

	void CompleteLevel()
	{
		LevelCompleteEvent?.Invoke();
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			CompleteLevel();
		}
	}
}