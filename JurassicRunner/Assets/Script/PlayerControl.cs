using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	[SerializeField]
	private float translationSpeed = 10;

//	[SerializeField]
	private float MaxHeightJump = 0.1f;
	private float currentheightJump = 0;
	private float speedJump = 0.7f;
	private bool jumpInProgress = false;

	private float MAX_X = 14.5f;
	private float MIN_X = -14.5f;
	private float MIN_Y = 0.6f;
	private float MAX_Y = 3f;


	// Use this for initialization
	void Start () {

		//initialisation du player
		transform.position = new Vector3(0, MIN_Y, 0);

	}
	
	// Update is called once per frame
	void Update () {



		IDistance distance = ServicesLocator.GetService<IDistance>();
		distance.Add(transform.position.z);

		float horizontalMove = Input.GetAxis("Horizontal") * translationSpeed * Time.deltaTime;
		transform.position += transform.right * horizontalMove ;
		// ajouter angle de +/- 10° et si je lache on revient à 0

		if (Input.GetAxis("Vertical") != 0)
		{
			float VerticallMove = Input.GetAxis("Vertical") * translationSpeed * Time.deltaTime;
			transform.position += transform.forward * VerticallMove;
		}
		else
		{
			//le personnage avance toujours un minimum
			float VerticallMove = 0.5f * translationSpeed * Time.deltaTime;
			transform.position += transform.forward * VerticallMove;
		}

		if (Input.GetButton("Jump"))
        {




			Debug.Log("Jump currentheightJump: "+currentheightJump+ " MaxHeightJump: "+ MaxHeightJump);

			
			currentheightJump += speedJump * Time.deltaTime;
			if(currentheightJump > MaxHeightJump)
            {
				// Tmp il faut le faire redescendre
				currentheightJump = MaxHeightJump;
			}
            if (transform.position.y <= MAX_Y)
            {
				transform.position += transform.up * currentheightJump;
			} 
			
			//GetComponent<Rigidbody>().AddForce(Vector3.up * 5);
			

		}
     


        

		// Ne pas sortir de la route sur les côté
		if (transform.position.x > MAX_X)
		{
			transform.position = new Vector3(MAX_X, transform.position.y, transform.position.z);
		}
		if (transform.position.x <= MIN_X)
		{
			transform.position = new Vector3(MIN_X, transform.position.y, transform.position.z);
		}



	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Collision avec "+ other.name);

		//Destroy(other);

		if (GameController.Instance.Health > 0) { 
			GameController.Instance.Health -= 10; // On retire 10 point d'énergie si on a une colision
			if (GameController.Instance.Health <= 0)
			{
				// Scene manager
				Debug.Log("GameOver");
			}
		}
	}




}

