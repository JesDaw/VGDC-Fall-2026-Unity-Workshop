using UnityEngine; 
using UnityEngine.Events;

public class Referancing : MonoBehaviour 
{ 
    [SerializeField] SpriteRenderer component; 
    [SerializeField] GameObject targetGameObject; 
    public UnityEvent UnityEvent; 


    void Awake() 
    { 
        if (targetGameObject != null) 
        { 
            targetGameObject.TryGetComponent<SpriteRenderer>(out component); 
        } 
        else 
        { 
            this.gameObject.TryGetComponent<SpriteRenderer>(out component); 
        } 
        Debug.Log($"component = {component}"); 
        Debug.Log($"targetGameObject = {targetGameObject}"); 
        UnityEvent?.Invoke();
    } 
}
