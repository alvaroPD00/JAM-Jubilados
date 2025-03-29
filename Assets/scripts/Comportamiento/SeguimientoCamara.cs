using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    [Header("Configuración de Seguimiento")]
    public string targetTag;  // Tag de los objetos a seguir
    public Transform parentObject; // Objeto padre opcional (si no se usa tag)

    [Header("Restricciones de Movimiento")]
    public float minZ = 0f;  // Valor mínimo de la cámara en Z
    public float maxZ = 10f; // Valor máximo de la cámara en Z
    public float followSpeed = 5f; // Velocidad de seguimiento

    private Transform target; // Objeto actual a seguir
    private string lastTargetName = ""; // Para evitar mensajes repetidos en consola

    void Update()
    {
        BuscarObjetivo(); // Encuentra el objeto con menor Z dinámicamente

        if (target != null)
        {
            SeguirObjetivo(); // Sigue al objeto dentro de los límites
        }
    }

    void BuscarObjetivo()
    {
        Transform mejorObjetivo = null;
        float menorZ = float.MaxValue; // Buscar el menor valor en Z

        // Buscar en la jerarquía si se usa un objeto padre
        if (parentObject != null)
        {
            foreach (Transform child in parentObject)
            {
                if (child.position.z < menorZ)
                {
                    menorZ = child.position.z;
                    mejorObjetivo = child;
                }
            }
        }
        // Buscar por tag si no se usa objeto padre
        else if (!string.IsNullOrEmpty(targetTag))
        {
            GameObject[] objetos = GameObject.FindGameObjectsWithTag(targetTag);
            foreach (GameObject obj in objetos)
            {
                if (obj.transform.position.z < menorZ)
                {
                    menorZ = obj.transform.position.z;
                    mejorObjetivo = obj.transform;
                }
            }
        }

        // Si el objetivo cambió, mostrar en consola
        if (mejorObjetivo != null && mejorObjetivo.name != lastTargetName)
        {
            lastTargetName = mejorObjetivo.name;
            Debug.Log("Nuevo objeto detectado: " + lastTargetName);
        }

        target = mejorObjetivo;
    }

    void SeguirObjetivo()
    {
        Vector3 nuevaPosicion = transform.position;

        // Verifica si el objeto está dentro de los límites y muévete hacia él
        float nuevaZ = Mathf.Clamp(target.position.z, minZ, maxZ);

        // Mueve suavemente la cámara hacia el objetivo dentro del rango permitido
        nuevaPosicion.z = Mathf.Lerp(transform.position.z, nuevaZ, followSpeed * Time.deltaTime);
        transform.position = nuevaPosicion;
    }
}
