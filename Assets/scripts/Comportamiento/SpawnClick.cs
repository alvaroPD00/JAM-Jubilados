using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnClick : MonoBehaviour
{
    public Spawner spawner; // Referencia al script Spawner
    public ContadorGolpes contador; // Referencia al ContadorGolpes
    public int precio = 5; // Precio necesario para activar el botón

    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        // Obtener referencias a los componentes
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();

        // Inicialmente desactiva solo los componentes, no el objeto entero
        ActualizarEstado();
    }

    private void Update()
    {
        if (contador != null)
        {
            // Activar o desactivar los componentes según el recurso disponible
            ActualizarEstado();
        }
        else
        {
            Debug.LogWarning("SpawnClick: No se ha asignado un ContadorGolpes en el Inspector.");
        }
    }

    private void OnMouseDown()
    {
        if (contador != null && spawner != null)
        {
            if (contador.recurso >= precio)
            {
                contador.recurso -= precio; // Restar el precio al recurso
                spawner.SpawnObjeto(); // Ejecutar la compra (spawnear)
            }
        }
        else
        {
            Debug.LogWarning("SpawnClick: Falta asignar ContadorGolpes o Spawner en el Inspector.");
        }
    }

    private void ActualizarEstado()
    {
        bool activo = contador.recurso >= precio;
        if (boxCollider != null) boxCollider.enabled = activo;
        if (meshRenderer != null) meshRenderer.enabled = activo;
    }
}
