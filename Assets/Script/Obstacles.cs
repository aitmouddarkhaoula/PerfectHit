using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obstacles : MonoBehaviour
{
    public Obstacle ObstaclePrefab;
    public List<Obstacle> obstacles;
    [SerializeField] int max = 6;
    [SerializeField] int min = 1;
    [SerializeField] Floor floor;
    // Start is called before the first frame update
    void Start()
    {
        RandomGenerate();
        /*for (int i = 0; i < 20; i++)
        {
            Obstacle body = Instantiate(ObstaclePrefab);
            body.transform.position = new Vector3(Random.Range(floor.floorWidth, -floor.floorWidth), 1.27f, Random.Range(40f, floor.floorLenght));
            body.SetNumber(Random.Range(min, max));
            obstacles.Add(body);
        }*/


    }
    /*public void PutObstacles()
    {
        Obstacle body = Instantiate(ObstaclePrefab);
        body.transform.position = new Vector3(Random.Range(floor.floorPrefab.transform.localScale.x / 2, -floor.floorPrefab.transform.localScale.x / 2), 1.27f);
        body.SetNumber(Random.Range(min, max));
        obstacles.Add(body);

    }*/

    // Update is called once per frame
    void Update()
    {

    }
    void RandomGenerate()
    {
        Vector3 v3T = Vector3.zero;
        Vector3[] arv3 = new Vector3[20];
        float fMinDist = 20f;   // Minimum distance they need to be apart
        int iMaxTries = 100;    // Number of times to try to generate a position
        float fMinTry = Mathf.Infinity;

        int i;
        for (i = 0; i < 20; i++)
        {

            for (int j = 0; j < iMaxTries; j++)
            {
                v3T.x = Random.Range(floor.floorWidth, -floor.floorWidth);
                v3T.z = Random.Range(40f, floor.floorLenght);

                fMinTry = Mathf.Infinity;
                for (int k = 0; k < i; k++)
                    fMinTry = Mathf.Min((v3T - arv3[k]).magnitude, fMinTry);

                if (fMinTry > fMinDist) // Far enough apart
                    break;
            }
            if (fMinTry < fMinDist)
            {
                Debug.Log("Generation failed -- only found " + i + " points");
                break;
            }
            arv3[i] = v3T;
            

        }

        for (int j = 0; j < i; j++)
        {
            Obstacle body = Instantiate(ObstaclePrefab);
            body.transform.position = new Vector3(arv3[j].x, 1.27f, arv3[j].z);
            body.SetNumber(Random.Range(min, max));
            obstacles.Add(body);
            //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //go.transform.position = arv3[j];
        }


    }
}
