using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LoadingManager : MonoBehaviour
{
	[Tooltip("Temp String For Testing, Remove When Complete")]
	public string MainMenuName;

	[Tooltip("This is a list of gameplay levels. List the name of levels in the intended order of play")]
	public List<string> playableLevels;
	public int completedLevels;



    //Updated to use events
    //TODO make sure this works later
    private void OnEnable()
    {
		//EventManager.PostLevelEndEvent += NextLevel;
    }

    private void OnDisable()
    {
        //EventManager.PostLevelEndEvent -= NextLevel;
    }

    void Awake()
	{
		completedLevels = 0;
		DontDestroyOnLoad(this.gameObject);
	}
	public void StartGame()
	{
		completedLevels = 0;
		SceneManager.LoadScene(playableLevels[completedLevels]);
	}

	public void QuitToMenu()
	{
		SceneManager.LoadScene(MainMenuName);		
	}

	public void NextLevel()
	{
		completedLevels++;
		SceneManager.LoadScene(playableLevels[completedLevels]);
	}
}
