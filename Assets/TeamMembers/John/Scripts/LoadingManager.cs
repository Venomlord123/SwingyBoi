using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
	[Tooltip("This is a list of gameplay levels. List the name of levels in the intended order of play")]
	public List<string> playableLevels;
	public int completedLevels;

	void Awake()
	{
		completedLevels = 0;
		DontDestroyOnLoad(this.gameObject);
	}
	public void StartGame()
	{
		SceneManager.LoadScene(playableLevels[completedLevels]);
	}

	public void QuitToMenu()
	{
		SceneManager.LoadScene("MainMenuScene");		
	}

	public void NextLevel()
	{
		completedLevels++;
		SceneManager.LoadScene(playableLevels[completedLevels]);
	}
}
