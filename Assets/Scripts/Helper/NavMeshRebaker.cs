using UnityEngine;
using UnityEngine.AI;

public class NavMeshRebaker : MonoBehaviour
{
    #region Methods
    public void RebakeNavMesh() 
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
    #endregion
}
