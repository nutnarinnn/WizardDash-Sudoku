using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuTable : MonoBehaviour
{
	private bool isPlayerInTable = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerInTable = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			isPlayerInTable = false;
			Debug.Log("Not in the Table");
		}
	}

	public void DetermineCellName(string cellName)
	{
		if (!isPlayerInTable)
		{
			Debug.Log("Player is not in the Table");
			return;
		}

		Debug.Log("Player is in Cell: " + cellName);
	}
}
