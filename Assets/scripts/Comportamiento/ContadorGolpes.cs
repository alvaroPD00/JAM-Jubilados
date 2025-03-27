using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorGolpes : MonoBehaviour
{
    [Header("Contador de Golpes")]
    public int golpes = 0; // Cuenta los golpes recibidos

    [Header("Contador de recurso")]
    public int recurso = 0; // Cuenta los golpes recibidos

    public void Contar()
    {
        golpes++;
        Debug.Log($"Golpes recibidos: {golpes}");

        recurso++;
        Debug.Log($"recurso: {recurso}");
    }
}
