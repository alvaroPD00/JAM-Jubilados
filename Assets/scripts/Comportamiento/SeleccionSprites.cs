using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleccionSprites : MonoBehaviour
{
    [Header("Lista de Objetos Hijos")]
    public List<GameObject> hijos; // Lista de hijos a manejar

    private void Start()
    {
        SeleccionarHijoAleatorio();
    }

    private void OnEnable()
    {
        SeleccionarHijoAleatorio();
    }

    private void SeleccionarHijoAleatorio()
    {
        if (hijos.Count == 0)
        {
            Debug.LogWarning("No hay objetos hijos asignados en " + gameObject.name);
            return;
        }

        // Elegir un índice aleatorio
        int indiceAleatorio = Random.Range(0, hijos.Count);

        // Activar solo el hijo seleccionado, desactivar los demás
        for (int i = 0; i < hijos.Count; i++)
        {
            hijos[i].SetActive(i == indiceAleatorio);
        }

        Debug.Log("Objeto activado: " + hijos[indiceAleatorio].name);
    }
}
