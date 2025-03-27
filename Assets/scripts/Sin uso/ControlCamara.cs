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

    [Header("Rotaci�n de C�mara")]
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

        // Aseg�rate de que la c�mara sea hija del jugador.
        if (cameraTransform == null || cameraTransform.parent != transform)
        {
            Debug.LogError("�La c�mara principal debe estar como hija del objeto jugador!");
            return;
        }

        // Bloquea el cursor en la pantalla.
        Cursor.lockState = CursorLockMode.Locked;

        // Sincroniza el �ngulo inicial de inclinaci�n de la c�mara
        cameraTilt = 0f; 
       // Debug.Log($"Rotaci�n inicial de la c�mara start (localEulerAngles): {cameraTransform.localEulerAngles}");
    }

    void Update()
    {

     //   Debug.Log($"Rotaci�n inicial de la c�mara void0 (localEulerAngles): {cameraTransform.localEulerAngles}");
        // Asegurarse de que no falten referencias.
        if (characterController == null || cameraTransform == null)
            return;

     //   Debug.Log($"Rotaci�n inicial de la c�mara void1 (localEulerAngles): {cameraTransform.localEulerAngles}");
        // Movimiento del personaje
        MovimientoPersonaje();

     //   Debug.Log($"Rotaci�n inicial de la c�mara void2 (localEulerAngles): {cameraTransform.localEulerAngles}");
        // Rotaci�n de la c�mara
        RotacionCamara();

      //  Debug.Log($"Rotaci�n inicial de la c�mara void3 (localEulerAngles): {cameraTransform.localEulerAngles}");
    }

    void MovimientoPersonaje()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        // Direcci�n del movimiento
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
        // Movimiento del rat�n
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotaci�n horizontal del personaje (eje Y)
        transform.Rotate(Vector3.up * mouseX);

        // Rotaci�n vertical de la c�mara (eje X)
        cameraTilt -= mouseY;
        cameraTilt = Mathf.Clamp(cameraTilt, -90f, 90f);

        // Aplicar rotaci�n a la c�mara
        cameraTransform.localRotation = Quaternion.Euler(cameraTilt, 0f, 0f);
    }
}
