using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource; // Referencia al AudioSource

    [Header("Distancias Proporcionales")]
    public float minDistanceFactor = 0.5f; // Factor para la distancia mínima (proporcional al 50% de la escala)
    public float maxDistanceOffset = 97f; // Diferencia fija entre maxDistance y minDistance

    private float escalaInicial; // Valor inicial de la escala del Orbe

    private float minDistanceInicial; // Valor inicial de minDistance
    private float maxDistanceInicial; // Valor inicial de maxDistance

    void Start()
    {
        // Si no se asigna un AudioSource, buscamos uno automáticamente
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        // Guardar los valores iniciales de minDistance y maxDistance
        minDistanceInicial = audioSource.minDistance;
        maxDistanceInicial = audioSource.maxDistance;

        // Guardar la escala inicial del objeto
        escalaInicial = transform.localScale.x; // Usamos el eje X para la escala (suposición de esfera)
    }

    void Update()
    {
        // Calcular el factor de escala basado en la escala actual y la escala inicial
        float factorEscala = transform.localScale.x / escalaInicial;

        // Modificar minDistance en función de la escala (mitad de la escala)
        float nuevaMinDistance = minDistanceInicial * factorEscala * minDistanceFactor;
        audioSource.minDistance = nuevaMinDistance;

        // Calcular maxDistance como minDistance + el offset de 97 unidades
        audioSource.maxDistance = nuevaMinDistance + maxDistanceOffset;
    }
}
