using UnityEngine;

public class ProjectSettingsReferances : MonoBehaviour
{
    [SerializeField] Vector2 gravity;
    void Start()
    {
        Physics2D.gravity = gravity;
    }
}
