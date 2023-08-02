using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	//public static LoadingManager instance;
	public List<string> playableLevelsID;


	void Awake()
	{
		// = this;
		DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartGame()
	{
		SceneManager.LoadScene(1);
	}

	public void NextLevel(int nextLevel, LoadSceneMode mode)
	{
		SceneManager.LoadScene(nextLevel, mode);
	}
}
