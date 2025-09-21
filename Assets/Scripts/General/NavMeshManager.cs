using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        UpdateNavMesh();
        if (NavMesh.SamplePosition(transform.position, out var hit, 1.0f, NavMesh.AllAreas))
        {
            Debug.Log("NavMesh exists!");
        }
        else
        {
            //navMeshSurface.BuildNavMesh();
            Debug.LogWarning("No NavMesh found under this position!");
        }
    }
    // Call this to include both Road and Ground layers as walkable
    public void MakeAllWalkable()
    {
        navMeshSurface.layerMask = LayerMask.GetMask("Road", "Tile");
        UpdateNavMesh();
    }

    // Call this to make only Road walkable again
    public void MakeOnlyRoadWalkable()
    {
        navMeshSurface.layerMask = LayerMask.GetMask("Road");
        UpdateNavMesh();
    }
    public void UpdateNavMesh()
    {
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh(); // Re-bakes the NavMesh
        }
    }
}
