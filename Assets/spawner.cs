using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class spawner : MonoBehaviour
{
    public float spawanTimer = 1;
    public GameObject prefabSpawner;

    public float minEdgeDistance = 0.3f;
    public MRUKAnchor.SceneLabels spawnlabels;
    public float normalOffset;

    private float timer;

    public int spawnTry = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized)
            return;


        timer += Time.deltaTime;
        if(timer> spawanTimer )
        {
            SpawnMummy();
            timer -= spawanTimer;
        }
    }

    public void SpawnMummy() { 

        MRUKRoom room =MRUK.Instance.GetCurrentRoom();

        int currentTry = 0;
        while( currentTry < spawnTry )
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, LabelFilter.Included(spawnlabels), out Vector3 pos, out Vector3 normal);

            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + normal * normalOffset;
                randomPositionNormalOffset.y = 0;

                Instantiate(prefabSpawner, randomPositionNormalOffset, Quaternion.identity);
                return;
            }
            else
            {
                currentTry++;
            }

        }

        /*
            Vector3 randomPosition = Random.insideUnitSphere*3;
            randomPosition.y = 0;

            Instantiate( prefabSpawner,randomPosition,Quaternion.identity );*/


    }
}
