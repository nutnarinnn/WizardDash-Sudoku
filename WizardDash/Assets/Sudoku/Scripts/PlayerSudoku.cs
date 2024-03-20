using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSudoku : MonoBehaviour
{
	public float moveSpeed = 5f;
	public SudokuTable sudokuTable;
	public Text scoreText;
	private SudokuCell[] sdkCells;
	private Animator animator;

	void Start()
	{
		sdkCells = FindObjectsOfType<SudokuCell>();
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		HandleMovement();
		UpdateScoreText();
	}

	void HandleMovement()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized * moveSpeed * Time.deltaTime;
		transform.Translate(movement, Space.World);

		if (movement != Vector3.zero)
		{
			transform.rotation = Quaternion.LookRotation(movement);
			animator.SetBool("isMoving", true);
		}
		else
		{
			animator.SetBool("isMoving", false);
		}
	}

	void UpdateScoreText()
	{
		int totalScore = 0;
		foreach (SudokuCell cell in sdkCells)
		{
			totalScore += cell.score;
		}
		scoreText.text = "Score: " + totalScore.ToString();
	}
}
