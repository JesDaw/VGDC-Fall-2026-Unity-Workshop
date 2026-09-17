using UnityEngine;

public class StatsHolder : MonoBehaviour 
{ 
    public float moveSpeed; 
    [SerializeField] GameObject characterPrefab; 
    void OnSceneUnloaded() 
    { 
        if (characterPrefab.TryGetComponent<StatsHolder>(out StatsHolder prefabStats)) 
            prefabStats.moveSpeed = moveSpeed;    
    } 
}
