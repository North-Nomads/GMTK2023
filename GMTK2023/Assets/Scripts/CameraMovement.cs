using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private float speed;
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;   
    }

    
    void Update()
    {
        float heightMovement = speed * Input.GetAxis("Height") != 0 ? speed * Input.GetAxis("Height") : speed * Input.GetAxis("Mouse ScrollWheel");
        _camera.transform.Translate(speed * Input.GetAxis("Horizontal"), speed * Input.GetAxis("Vertical"), heightMovement) ;
    }
}
