using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;

public class RuntimeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navmeshSurface;
    // Start is called before the first frame update
    void Start()
    {
        navmeshSurface= GetComponent<NavMeshSurface>();
        // navmeshSurface.BuildNavMesh();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavmesh);
    }

    public void BuildNavmesh()
    {
        StartCoroutine(BuildNavmeshRoutine());
    }

    public IEnumerator BuildNavmeshRoutine()
    {
        yield return new WaitForEndOfFrame();
        navmeshSurface.BuildNavMesh();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
