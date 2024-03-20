using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float jumpForce = 6f;
	private float normalSpeed = 0.15f;
	public float moveSpeed;

	public bool isGrounded = true;
	public bool die = false;
	public bool boost = false;
	public bool box = false;

	public float score = 0;
	public int boxCount = 0;
	private int boxCountend = 5;
	public Text scoreText;
	public Text BoxText;
	public Image scoreBox;
	public float lastScore;
	public Text bestScoreText;
	private float timer = 0f;
	private float distance = 2f;
	public Button restartButton;

	private Animator anim;
	private Rigidbody rb;
	private List<GameObject> obstacles = new List<GameObject>();
	public GameObject boostEffect;
	public GameObject root;
	public GameObject body;

	public AudioSource myAudio;
	public AudioClip itemCollect;
	public AudioClip boostCollect;
	public AudioClip gameOver;

	void Start()
	{
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		myAudio = GetComponent<AudioSource>();
		lastScore = PlayerPrefs.GetFloat("MyScore");
		restartButton = GetComponent<Button>();
		moveSpeed = normalSpeed;
		FindObstacles();
	}

	void FindObstacles()
	{
		GameObject[] obstacleArray = GameObject.FindGameObjectsWithTag("Obstacle");
		obstacles.AddRange(obstacleArray);
	}

	void Update()
	{
		scoreText.text = "Score : " + score.ToString();
		if (score > lastScore)
		{
			bestScoreText.text = "Best Score : " + score.ToString() + "\nYour Score : " + score.ToString();
		}
		else
		{
			bestScoreText.text = "Best Score : " + lastScore.ToString() + "\nYour Score : " + score.ToString(); ;
		}

		BoxText.text = " : " + boxCount.ToString();

		if (!die)
		{
			timer += Time.deltaTime;
			scoreBox.gameObject.SetActive(false);
			transform.Translate(0, 0, moveSpeed);

			if (isGrounded && Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				Vector3 movement = Vector3.left * distance;
				transform.Translate(movement);
			}
			if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				Vector3 movement = Vector3.right * distance;
				transform.Translate(movement);
			}

			if (boost)
			{
				if (timer >= 0.05f)
				{
					score++;
					timer = 0f;
				}
				anim.SetBool("isJumping", false);
				boostEffect.SetActive(true);
				foreach (GameObject obstacle in obstacles)
				{
					if (obstacle != null)
						obstacle.GetComponent<Collider>().enabled = false;
				}
				rb.isKinematic = true;
			}
			else
			{
				if (score >= 3000)
				{
					normalSpeed = 0.25f;
					moveSpeed = normalSpeed;
				}
				else if (score >= 1500)
				{
					normalSpeed = 0.20f;
					moveSpeed = normalSpeed;
				}

				if (timer >= 0.1f)
				{
					score++;
					timer = 0f;
				}
				foreach (GameObject obstacle in obstacles)
				{
					if (obstacle != null)
						obstacle.GetComponent<Collider>().enabled = true;
				}
				rb.isKinematic = false;
			}
		}
		else
		{
			scoreText.gameObject.SetActive(false);
			scoreBox.gameObject.SetActive(true);
			transform.Translate(0, 0, 0);
		}
	}

	// RESTART BUTTON
	public void Restart()
	{
		SceneManager.LoadScene("EndlessRunning");
	}

    // BACK BUTTON
    public void Back()
    {
        SceneManager.LoadScene("Open");
    }

    // QUIT BUTTON
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }

    void Jump()
	{
		rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
		isGrounded = false;
		anim.SetBool("isJumping", true);
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
			anim.SetBool("isJumping", false);
		}
		else if (col.gameObject.CompareTag("Obstacle"))
		{
			die = true;
			myAudio.PlayOneShot(gameOver, 1f);
			rb.AddForce(-transform.forward * 5.0f, ForceMode.Impulse);
			anim.SetBool("Die", die);

			if (score > lastScore)
			{
				PlayerPrefs.SetFloat("MyScore", score);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Item")
		{
			if (other.gameObject.name == "Gem")
			{
				score += 10f;
				myAudio.PlayOneShot(itemCollect, 0.7f);
				other.transform.localScale = Vector3.zero;
				Destroy(other.gameObject, 0.6f);
			}
			else if (other.gameObject.name == "Boost")
			{
				myAudio.PlayOneShot(boostCollect, 0.7f);
				other.transform.localScale = Vector3.zero;
				Destroy(other.gameObject, 1f);
				StartCoroutine(BoostController());
			}
			else if (other.gameObject.name == "Box")
			{
				boxCount++;
				myAudio.PlayOneShot(boostCollect, 0.7f);
				other.transform.localScale = Vector3.zero;
				Destroy(other.gameObject, 1f);
				if (boxCount == boxCountend)
				{
					SceneManager.LoadScene("Sudoku");
				}
			}
		}
	}

	IEnumerator BoostController()
	{
		boost = true;
		moveSpeed = 1f;
		gameObject.transform.position = new Vector3(transform.position.x, 43.26942f, transform.position.z);
		yield return new WaitForSeconds(3);
		StartCoroutine(BoostEnding(1));
	}

	IEnumerator BoostEnding(float duration)
	{
		moveSpeed = normalSpeed;
		float blinkInterval = 0.1f;
		float timer = 0f;

		while (timer < duration)
		{
			root.SetActive(false);
			body.SetActive(false);
			yield return new WaitForSeconds(blinkInterval);
			root.SetActive(true);
			body.SetActive(true);
			yield return new WaitForSeconds(blinkInterval);
			timer += blinkInterval;
		}

		root.SetActive(true);
		body.SetActive(true);
		boostEffect.SetActive(false);
		boost = false;
	}
}
