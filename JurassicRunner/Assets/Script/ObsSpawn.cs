using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject obstacleOrigin;

    int maxSpawning;
    int nbSpawn = 0;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script Spawn");

        //RoadPlane
        GameObject RoadPlane = GameObject.Find("RoadPlane"); // On détect la route
        if (RoadPlane) // SI on la trouve
        {
            Debug.Log("RoadPlane Find");
        }

        float sx = RoadPlane.transform.localScale.x; // Dimension en X
        float sz = RoadPlane.transform.localScale.z; // Dimension en Z

        maxSpawning = 10;

        List<GameObject> lstSpawn = new List<GameObject>(); // On créer une liste vide pour stocker les objets générés

        while (nbSpawn < maxSpawning)
        {
            GameObject newObstacle = Instantiate(obstacleOrigin);
            Collider colliderNewObstacle = newObstacle.GetComponent<Collider>(); // On récupère son collider
            bool ok = false; // Bool pour le test de colision
            do
            {
                Vector3 newPos = new Vector3(
                    Random.Range(RoadPlane.transform.localPosition.x - sx / 2, RoadPlane.transform.localPosition.x + sx / 2),
                    1.2f,
                    Random.Range(RoadPlane.transform.localPosition.x - sz / 2, RoadPlane.transform.localPosition.x + sz / 2)
                    );

                newObstacle.transform.position = newPos;

                ok = true;

                foreach (GameObject item in lstSpawn)
                {
                    Collider oldObstacle = item.GetComponent<Collider>();
                    if ( Vector3.Distance(newObstacle.transform.position, item.transform.position) < newObstacle.transform.localScale.x )
                    {
                        ok = false;
                        break;
                    }

                }
            } while (!ok);
            lstSpawn.Add(newObstacle);
            nbSpawn++;
            Debug.Log("Nouvel Obstacle");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
