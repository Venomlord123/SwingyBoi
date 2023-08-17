using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
	public static event Action PostDamageEvent;
	public static event Action onGoalCheck; // New event for goal reached

	private void OnEnable()
	{
		Health.DamageEvent += Health_DamageEvent;
		Goal.LevelCompleteEvent += GoalEvent; // Subscribe to the GoalReachedEvent
	}

	private void OnDisable()
	{
		Health.DamageEvent -= Health_DamageEvent;
		Goal.LevelCompleteEvent -= GoalEvent; // Unsubscribe from the GoalReachedEvent
	}

	private void GoalEvent()
	{
		onGoalCheck?.Invoke(); // Invoke the GoalReachedEvent
	}

	private void Health_DamageEvent()
	{
		PostDamageEvent?.Invoke();
		Debug.Log("EventManager damage");
	}
}
