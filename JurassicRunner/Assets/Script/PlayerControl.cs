using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

	[SerializeField]
	private float translationSpeed = 10;

	[Space]
	[SerializeField] private Transform FeetTransform;
	[SerializeField] private LayerMask FloorMask;
	[SerializeField] private float jumpForce = 3f;
	/*private float MaxHeightJump = 0.1f;
	private float currentheightJump = 0;
	private float speedJump = 0.7f;
	private bool jumpInProgress = false;
	*/
	// Sans contact avec les barrières
	//private float MAX_X = 14.5f;
	//private float MIN_X = -14.5f;

	// Avec contact avec les barrières
	private float MAX_X = 15f;
	private float MIN_X = -15f;
	private float MIN_Y = 0;
	private float MAX_Y = 3f;

	[Space]
	[SerializeField] public float persoZ;


	//La 1ère chose est donc de déclarer 3 AudioClip que j'assignerai via l'inspecteur
	public AudioClip soundTouch;
	//Création d'une variable afin de garder une référence au composant AudioSource
	private AudioSource _audioSource;


	// Use this for initialization
	void Start () {

		//initialisation du player
		transform.position = new Vector3(0, MIN_Y, 0);

		//On récupère le composant AudioSource du gameObject et on l'assigne à la variable prévue à cet effet
		_audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {

		persoZ = transform.position.z;

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

		//if (Input.GetKeyDown(KeyCode.Space))
		if (Input.GetButton("Jump"))
        {


			/*

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
						*/
			Debug.Log("Press Jump "+ FeetTransform.position +"  "+ Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask));

			if (Physics.CheckSphere(FeetTransform.position, 1f, FloorMask))
            {
				Debug.Log("Jump");
				GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				_audioSource.clip = soundTouch;       //Ceci permet d'assigner le soundTouch comme AudioClip par défaut 
				_audioSource.Play();
			}

			

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

		if (other.name == "Lava")
		{
			Debug.Log("GameOver De Lave");
			SceneManager.LoadScene("finish");
		}

		if (GameController.Instance.Health > 0) { 
			GameController.Instance.Health -= 10; // On retire 10 point d'énergie si on a une colision

    

			if (GameController.Instance.Health <= 0)
			{
				// Scene manager
				Debug.Log("GameOver");
				SceneManager.LoadScene("finish");
			}
			_audioSource.clip = soundTouch;       //Ceci permet d'assigner le soundTouch comme AudioClip par défaut 
			_audioSource.Play();
		}
	}




}

