using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    /* Création singleton */
    public static GameController Instance;

    [SerializeField]
    private GameObject[] RoadsPrefab; // Liste de tous les tronçons de sol qui existent
    [SerializeField]
    private GameObject[] RoadsOnStage; // Liste des tronçons en cours

    private GameObject Player; // récupération du Player

    private float RoadSize; // Longueur d'un tronçon

    private int NbRoads = 4; // Nombre de tronçons que je veeux en même temps sur la scène

    private bool firstRoad = true; // Est ce qu'il s'agit du démarage ?

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

        Health = 100; // Initialisation de la vie du joueur à 100;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player"); // On trouve l'objet player

        RoadsOnStage = new GameObject[NbRoads]; // Instanciation de la liste de tronçons en cours

        for (int i = 0; i < NbRoads; i++)
        {
            if(firstRoad == true)
            {
                RoadsOnStage[i] = Instantiate(RoadsPrefab[0]);
                firstRoad = false;
            }
            else { 
                int n = Random.Range(0, RoadsPrefab.Length); // Tire un nombre entre 0 et le nombre de tronçon possible -1
                RoadsOnStage[i] = Instantiate(RoadsPrefab[n]); // Créer sur la scène le tronçon avec le numéro tiré à la ligne précédente
            }
        }

        RoadSize = RoadsOnStage[0].GetComponentInChildren<Transform>().Find("RoadPlane").localScale.z; // Récupération de la taille z du cube RoadPlane qui est dans le Tronçon

        float pos = Player.transform.position.z + RoadSize / 2 - 1.5f; // On se position sur la road
        foreach (var road in RoadsOnStage)
        {
            road.transform.position = new Vector3(0, 0, pos); // On position chacun des tronçon qui sont daans la liste onStage
            pos += RoadSize;    // On ajoute la longueur d'un tronçon à la distance pour les places les uns après les autres
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = RoadsOnStage.Length-1; i >= 0; i-- ) // ON parcourt la liste des tronçon déjà placé à l'envers pour supprimer celui qui sera derrière nous
        {
            GameObject road = RoadsOnStage[i];
            if(road.transform.position.z + RoadSize/2 < Player.transform.position.z - 6f)
            {
                float z = road.transform.position.z;// récupéré la position du tronçon pour pouvoir la réutiliser après
                Destroy(road); // On détruit le tronçon
                int n = Random.Range(0, RoadsPrefab.Length); // On tire de nouveau au hasard le nouveau tronçon
                road = Instantiate(RoadsPrefab[n]); // On instancie le tronçon
                road.transform.position = new Vector3(0, 0, z + RoadSize*NbRoads); // On le position au bout de la route
                RoadsOnStage[i] = road; // On ajoute ce nouveau tronçon à la route

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
