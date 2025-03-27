using System.Collections;
using UnityEngine;

public class Acelerador : MonoBehaviour
{
    [Header("Configuración del Acelerador")]
    public Spawner spawner; // Referencia al script Spawner
    public float tiempoEntreAceleraciones = 5f; // Tiempo en segundos entre cada aceleración
    public float multiplicador = 1.5f; // Valor por el que se multiplicará el intervaloSpawn

    private void Start()
    {
        // Verifica si se ha asignado el spawner
        if (spawner == null)
        {
            Debug.LogWarning("Acelerador: Asegúrate de asignar un Spawner en el Inspector.");
            return;
        }

        // Inicia la corrutina de aceleración
        StartCoroutine(AcelerarIntervalo());
    }

    private IEnumerator AcelerarIntervalo()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreAceleraciones); // Espera el tiempo entre aceleraciones

            // Multiplica el intervaloSpawn por el valor del multiplicador
            spawner.intervaloSpawn *= multiplicador;
            Debug.Log($"El intervalo de spawn ha sido multiplicado por {multiplicador}. Nuevo intervalo: {spawner.intervaloSpawn}");
        }
    }
}
