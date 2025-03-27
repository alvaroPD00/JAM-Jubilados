using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ControlCamara : MonoBehaviour
{
    [Header("Movimiento")]
    [Range(1f, 10f)]
    public float movementSpeed = 5f;
    [Range(1f, 10f)]
    public float runMultiplier = 2f;

    [Header("Rotación de Cámara")]
    [Range(50f, 1000f)]
    public float mouseSensitivity = 200f;

    [Header("Salto y Gravedad")]
    [Range(0f, 10f)]
    public float jumpStrength = 2f;
    public float gravity = 9.8f;

    private CharacterController characterController;
    private Transform cameraTransform;
    private float verticalSpeed = 0f;
    private float cameraTilt = 0f;
    private bool isRunning = false;

    void Start()
    {
        // Referencias
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Asegúrate de que la cámara sea hija del jugador.
        if (cameraTransform == null || cameraTransform.parent != transform)
        {
            Debug.LogError("¡La cámara principal debe estar como hija del objeto jugador!");
            return;
        }

        // Bloquea el cursor en la pantalla.
        Cursor.lockState = CursorLockMode.Locked;

        // Sincroniza el ángulo inicial de inclinación de la cámara
        cameraTilt = 0f; 
       // Debug.Log($"Rotación inicial de la cámara start (localEulerAngles): {cameraTransform.localEulerAngles}");
    }

    void Update()
    {

     //   Debug.Log($"Rotación inicial de la cámara void0 (localEulerAngles): {cameraTransform.localEulerAngles}");
        // Asegurarse de que no falten referencias.
        if (characterController == null || cameraTransform == null)
            return;

     //   Debug.Log($"Rotación inicial de la cámara void1 (localEulerAngles): {cameraTransform.localEulerAngles}");
        // Movimiento del personaje
        MovimientoPersonaje();

     //   Debug.Log($"Rotación inicial de la cámara void2 (localEulerAngles): {cameraTransform.localEulerAngles}");
        // Rotación de la cámara
        RotacionCamara();

      //  Debug.Log($"Rotación inicial de la cámara void3 (localEulerAngles): {cameraTransform.localEulerAngles}");
    }

    void MovimientoPersonaje()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Dirección del movimiento
        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;
        moveDirection.Normalize();

        // Velocidad (correr o caminar)
        float speed = isRunning ? movementSpeed * runMultiplier : movementSpeed;

        // Movimiento vertical (gravedad y salto)
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
                verticalSpeed = jumpStrength;
            else
                verticalSpeed = 0;
        }
        verticalSpeed -= gravity * Time.deltaTime;

        // Aplicar movimiento
        Vector3 velocity = moveDirection * speed + Vector3.up * verticalSpeed;
        characterController.Move(velocity * Time.deltaTime);
    }

    void RotacionCamara()
    {
        // Movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación horizontal del personaje (eje Y)
        transform.Rotate(Vector3.up * mouseX);

        // Rotación vertical de la cámara (eje X)
        cameraTilt -= mouseY;
        cameraTilt = Mathf.Clamp(cameraTilt, -90f, 90f);

        // Aplicar rotación a la cámara
        cameraTransform.localRotation = Quaternion.Euler(cameraTilt, 0f, 0f);
    }
}
