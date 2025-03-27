using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sucumbir : MonoBehaviour
{
    [SerializeField] private Transform esfera; // Referencia a la esfera
    [SerializeField] private float velocidadMovimiento = 1f; // Velocidad de acercamiento
    [SerializeField] private float duracionRotacion = 2f; // Tiempo en segundos para completar la inclinación

    private bool siendoAtraido = false; // Controla si el edificio debe moverse e inclinarse
    private Quaternion rotacionInicial; // Guarda la rotación original del edificio
    private Quaternion rotacionObjetivo; // Guarda la rotación final hacia la esfera
    private float tiempoRotacion = 0f; // Contador para la interpolación de rotación

    void Start()
    {
        Debug.Log($"se guarda la rotacion inicial: transform.rotation");
        rotacionInicial = transform.rotation; // Guardamos la rotación inicial
    }

    void Update()
    {
        if (siendoAtraido && esfera != null)
        {

            Debug.Log($"detonando atraccion");
            // Movimiento progresivo hacia el centro de la esfera
            transform.position = Vector3.MoveTowards(transform.position, esfera.position, velocidadMovimiento * Time.deltaTime);

            // Rotación progresiva hacia la esfera
            tiempoRotacion += Time.deltaTime / duracionRotacion;
            transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionObjetivo, tiempoRotacion);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log($"colision detectada");
        if (other.transform == esfera)
        {

            Debug.Log($"siendo atraido");
            siendoAtraido = true;
            DefinirRotacionObjetivo();
        }
    }

    private void DefinirRotacionObjetivo()
    {
        Vector3 direccionHaciaEsfera = (esfera.position - transform.position).normalized;
        float anguloX = Mathf.Clamp(Vector3.Dot(direccionHaciaEsfera, Vector3.forward) * 90f, -90f, 90f);
        float anguloZ = Mathf.Clamp(Vector3.Dot(direccionHaciaEsfera, Vector3.right) * -90f, -90f, 90f); // Invertido

        rotacionObjetivo = Quaternion.Euler(anguloX, transform.rotation.eulerAngles.y, anguloZ);
    }

}
