using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{ 
    private  CharacterController controller;
    private float Speed;
    private float Mouse_speed = 30f;
    private Vector3 MousePosition;
    [SerializeField] private GameObject GunPoint; 
    [SerializeField]private  Camera cam;
    [SerializeField] private Units Player;
    [SerializeField] private Transform playerPreab;
    private Vector3 direction;
    public bool MovementIstrue {get; private set;}
    public event Action<bool> MovementStateChanged;
    public Vector3 Direction{get{return direction;}}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Speed = Player.Speed;
        MovementIstrue = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
              controller.Move(transform.forward * Time.deltaTime * Speed);
              
              
        }

         if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            controller.Move(transform.right * Time.deltaTime * -Speed);
            
            
        }
         if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            controller.Move(transform.right * Time.deltaTime * Speed);
            
            
        }
        
         if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            controller.Move(transform.forward * Time.deltaTime * -Speed);
          
            
            
        }
         
         
         if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
         {
             MovementIstrue = true;
         }
         else 
         {
             MovementIstrue = false;
         }
        

        if (MovementIstrue)
        {
            MovementStateChanged?.Invoke(MovementIstrue);
        }
        else 
        {
             
             MovementStateChanged?.Invoke(MovementIstrue);
        }
        
        Vector2 screenPos = Input.mousePosition;
        // Z = distance from camera (pick a value that suits your scene)
        Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 10f));

         direction = worldPos - transform.position;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {

           // GunPoint.transform.rotation = Quaternion.LookRotation(direction);
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            playerPreab.rotation =
                lookRotation * Quaternion.Euler(0f, 30f, 0f);


        }

    }
}
