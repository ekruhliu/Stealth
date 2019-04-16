using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public float speed;
	private CharacterController cc;
	
	public GameObject GameOver;

	public Text start;
	public GameObject ogjDoor;

	public bool doorTrig;

	public bool takeCard;
	public Text PressE;
	
	public bool fanOn;
	public bool fanTrig;

	public bool cardTrig;
	public float step;

	public GameObject cardd;
	
	public ParticleSystem ps;
	public bool On;
	

	private void Awake() {cc = GetComponent<CharacterController>(); }

	private void Start()
	{
		ps.Stop();
		ps.gameObject.SetActive(false);
		step = 2.0f;
		PressE.gameObject.SetActive(false);
	}

	private void FixedUpdate() { Moves(); }
	
	private void Update()
	{
		if (!On)
			Invoke("offStart", 5);
		if (fanTrig)
		{
			if (Input.GetKey(KeyCode.E) && !fanOn)
			{
				ps.gameObject.SetActive(true);
				ps.Play();
				fanOn = true;
			}
			else if (Input.GetKey(KeyCode.E) && fanOn)
			{
				ps.Stop();
				ps.gameObject.SetActive(false);
				fanOn = false;
			}
		}

		if (cardTrig)
		{
			if (Input.GetKey(KeyCode.E))
			{
				PressE.gameObject.SetActive(false);
				cardd.SetActive(false);
				takeCard = true;
			}
		}

		if (doorTrig)
		{
			if (Input.GetKey(KeyCode.E))
			{
				PressE.gameObject.SetActive(false);
				ogjDoor.SetActive(false);
			}
		}
	}

	void Moves()
	{
		float h = Input.GetAxis("Horizontal") * speed;
		float v = Input.GetAxis("Vertical") * speed;

		Vector3 forwardMoves = transform.forward * v;
		Vector3 rightMoves = transform.right * h;
		cc.SimpleMove(forwardMoves + rightMoves);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals("finish"))
		{
			start.gameObject.SetActive(true);
			start.text = "You win!!!";
			GameOver.SetActive(true);
			Invoke("restart", 5);
		}
	}

	private void OnTriggerStay(Collider other)
	{
/*		if (other.gameObject.tag.Equals("finish"))
		{
			start.gameObject.SetActive(true);
			start.text = "You win!!!";
			GameOver.SetActive(true);
			Invoke("restart", 5);
		}*/
		if (other.gameObject.tag.Equals("door") && takeCard)
		{
			doorTrig = true;
			PressE.gameObject.SetActive(true);
		}
		if (other.gameObject.tag.Equals("fan"))
		{
			PressE.gameObject.SetActive(true);
			fanTrig = true;
		}

		if (other.gameObject.tag.Equals("card"))
		{
			PressE.gameObject.SetActive(true);
			cardTrig = true;
		}
		if (other.gameObject.tag.Equals("ps"))
			step = 0.5f;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag.Equals("door") && takeCard)
			PressE.gameObject.SetActive(false);
		if (other.gameObject.tag.Equals("fan"))
		{
			fanTrig = false;
			PressE.gameObject.SetActive(false);
		}
		if (other.gameObject.tag.Equals("card"))
		{
			PressE.gameObject.SetActive(false);
			cardTrig = false;
		}
		if (other.gameObject.tag.Equals("ps"))
			step = 2.0f;
	}

	void offStart()
	{
		start.gameObject.SetActive(false);
		On = true;
	}

	void restart()
	{
		Application.LoadLevel("SampleScene");
	}
}
