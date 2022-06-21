using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /* Cr�ation singleton */
    public static GameController Instance;

    [SerializeField]
    private GameObject[] RoadsPrefab; // Liste de tous les tron�ons de sol qui existent
    [SerializeField]
    private GameObject[] RoadsOnStage; // Liste des tron�ons en cours

    private GameObject Player; // r�cup�ration du Player

    private float RoadSize; // Longueur d'un tron�on

    private int NbRoads = 4; // Nombre de tron�ons que je veeux en m�me temps sur la sc�ne

    private bool firstRoad = true; // Est ce qu'il s'agit du d�marage ?

    [SerializeField]
    private Image HealthBarRed;
    [SerializeField]
    private Image HealthBarGreen;

    [SerializeField]
    public int Health; // Energie du joueur

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        Health = 100; // Initialisation de la vie du joueur � 100;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player"); // On trouve l'objet player

        RoadsOnStage = new GameObject[NbRoads]; // Instanciation de la liste de tron�ons en cours

        for (int i = 0; i < NbRoads; i++)
        {
            if(firstRoad == true)
            {
                RoadsOnStage[i] = Instantiate(RoadsPrefab[0]);
                firstRoad = false;
            }
            else { 
                int n = Random.Range(0, RoadsPrefab.Length); // Tire un nombre entre 0 et le nombre de tron�on possible -1
                RoadsOnStage[i] = Instantiate(RoadsPrefab[n]); // Cr�er sur la sc�ne le tron�on avec le num�ro tir� � la ligne pr�c�dente
            }
        }

        RoadSize = RoadsOnStage[0].GetComponentInChildren<Transform>().Find("RoadPlane").localScale.z; // R�cup�ration de la taille z du cube RoadPlane qui est dans le Tron�on

        float pos = Player.transform.position.z + RoadSize / 2 - 1.5f; // On se position sur la road
        foreach (var road in RoadsOnStage)
        {
            road.transform.position = new Vector3(0, 0, pos); // On position chacun des tron�on qui sont daans la liste onStage
            pos += RoadSize;    // On ajoute la longueur d'un tron�on � la distance pour les places les uns apr�s les autres
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = RoadsOnStage.Length-1; i >= 0; i-- ) // ON parcourt la liste des tron�on d�j� plac� � l'envers pour supprimer celui qui sera derri�re nous
        {
            GameObject road = RoadsOnStage[i];
            if(road.transform.position.z + RoadSize/2 < Player.transform.position.z - 6f)
            {
                float z = road.transform.position.z;// r�cup�r� la position du tron�on pour pouvoir la r�utiliser apr�s
                Destroy(road); // On d�truit le tron�on
                int n = Random.Range(0, RoadsPrefab.Length); // On tire de nouveau au hasard le nouveau tron�on
                road = Instantiate(RoadsPrefab[n]); // On instancie le tron�on
                road.transform.position = new Vector3(0, 0, z + RoadSize*NbRoads); // On le position au bout de la route
                RoadsOnStage[i] = road; // On ajoute ce nouveau tron�on � la route

            }
        }

        //Adapter la taille de Health Bar en pourcentage
        float coef = Health / 100.0f;
        HealthBarGreen.rectTransform.sizeDelta = new Vector2(HealthBarRed.rectTransform.sizeDelta.x * coef, HealthBarRed.rectTransform.sizeDelta.y);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
