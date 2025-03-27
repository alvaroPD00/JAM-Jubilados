using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SeguirObjetivo : MonoBehaviour
{
    public string tagObjetivo = "Player"; // La tag del objeto a seguir
    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Transform destinoMasCercano = ObtenerObjetivoMasCercano();

        if (destinoMasCercano != null)
        {
            agente.SetDestination(destinoMasCercano.position);
        }
    }

    Transform ObtenerObjetivoMasCercano()
    {
        GameObject[] objetivos = GameObject.FindGameObjectsWithTag(tagObjetivo);
        Transform objetivoMasCercano = null;
        float distanciaMinima = Mathf.Infinity;
        Vector3 posicionActual = transform.position;

        foreach (GameObject objetivo in objetivos)
        {
            float distancia = Vector3.Distance(posicionActual, objetivo.transform.position);
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                objetivoMasCercano = objetivo.transform;
            }
        }

        return objetivoMasCercano;
    }
}
