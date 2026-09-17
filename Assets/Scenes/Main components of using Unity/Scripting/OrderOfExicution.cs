using UnityEngine;

public class OrderOfExicution : MonoBehaviour
{
    #region startup
    void Awake()
    {
        // Called once when the script instance is being loaded (even if the script component is disabled).
    }
    void OnEnable()
    {
        // Called every time the GameObject or script component becomes enabled and active.
    }
    void Start()
    {
        // Called on the frame when the script is enabled before any Update methods are called for the first time.
    }
    #endregion

    #region every frame
    void FixedUpdate()
    {
        // Called at regular fixed time intervals independent of frame rate (used for physics calculations)
    }
    void Update()
    {
        // Called once per frame. used for non-physics gameplay logic and frame rate dependent operations.
    }
    void LateUpdate()
    {
        // Called once per frame right after all Update functions have finished running (ideal for camera movement).
    }
    #endregion

    #region Editor only
    void OnValidate()
    {
        // Called in the Unity Editor when the script is loaded or when a value is changed in the Inspector.
    }
    void Reset()
    {
        // Called when the component is first added to a GameObject or when the user resets it in the Inspector
    }
    void OnApplicationPause()
    {
        // Called when the application pauses (app sent to background on mobile) or resumes.
    }
    void OnApplicationQuit()
    {
        // Called on all GameObjects when the application exits or play mode is stopped in the Editor.
    }
    #endregion

    #region cleanup
    void OnDisable()
    {
        // Called every time the GameObject or script component becomes disabled or inactive.
    }
    void OnDestroy()
    {
        // Called once when the GameObject or script component is destroyed or when the scene unloads.
    }
    #endregion
}