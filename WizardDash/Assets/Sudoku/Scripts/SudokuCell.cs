using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuCell : MonoBehaviour
{
	public SudokuTable sudokuTable;
	private Transform child;
	private SpriteRenderer childSpriteRenderer;
	public List<Transform> cells;
	public Sprite[] sprites;
	public Button[] buttons;
	private bool correct = false;
	private Color incorrectColor = new Color(1f, 0.4f, 0.4f, 1f);
	public int score = 0;
	private int wrongClicks = 0;
	public Button hintButton;
	public Text hintText;
	private int hintAmount;

	private void Start()
	{
		child = transform.GetChild(0);
		childSpriteRenderer = child.GetComponent<SpriteRenderer>();

		if (gameObject.CompareTag("unsolved"))
		{
			child.gameObject.SetActive(false);
		}
		else if (gameObject.CompareTag("solved"))
		{
			child.gameObject.SetActive(true);
		}

		hintAmount = System.Convert.ToInt32(hintText.text);
		if (hintAmount <= 0)
		{
			hintButton.interactable = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && gameObject.CompareTag("unsolved"))
		{
			sudokuTable.DetermineCellName(gameObject.name);
			Debug.Log(child.gameObject.name);

			for (int i = 0; i < buttons.Length; i++)
			{
				int buttonIndex = i;
				buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].onClick.RemoveAllListeners();
		}
	}

	private void OnButtonClick(int buttonIndex)
	{
		string answer = (buttonIndex + 1).ToString();
		if (answer == child.gameObject.name)
		{
			correct = true;
		}
		else
		{
			wrongClicks++;
		}

		if (gameObject.CompareTag("unsolved"))
		{
			if (buttonIndex >= 0 && buttonIndex < sprites.Length && correct)
			{
				child.gameObject.SetActive(true);
				childSpriteRenderer.sprite = sprites[buttonIndex];
				gameObject.tag = "solved";
				int increaseScore = 100 - 10 * wrongClicks;
				if (increaseScore > 0)
				{
					score += increaseScore;
				}
				else
				{
					score += 0;
				}
			}
			else if (!correct)
			{
				StartCoroutine(ChangeButtonColor(buttons[buttonIndex], incorrectColor, 0.25f));
			}
		}
	}
	
	private IEnumerator ChangeButtonColor(Button button, Color color, float duration)
	{
		Color originalColor = button.image.color;
		button.image.color = color;
		yield return new WaitForSeconds(duration);
		button.image.color = originalColor;
	}

	public void Hint()
	{
		if ( hintAmount > 0)
		{
			int randomIndex = Random.Range(0, cells.Count);
			Transform randomCell = cells[randomIndex];
			if (randomCell.gameObject.CompareTag("unsolved") && cells.Count > 0)
			{
				randomCell.GetChild(0).gameObject.SetActive(true);
				randomCell.gameObject.tag = "solved";
				score += 100;
				cells.RemoveAt(randomIndex);
				hintAmount -= 1;
				hintText.text = hintAmount.ToString();
				if (hintAmount == 0)
				{
					hintButton.interactable = false;
				}
			}
		}
	}
}
