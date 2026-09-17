using UnityEngine;

public class ScriptableObjectTransition : MonoBehaviour
{
    [SerializeField] StatsSO stats; 
    public float moveSpeed; 
    void Awake() => moveSpeed = stats.MoveSpeed; 
}
