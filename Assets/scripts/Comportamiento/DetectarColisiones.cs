using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarColisiones : MonoBehaviour
{
    [Header("Tiempo en segundos para realizar el movimiento")]
    public float tiempoMovimiento = 1.0f;

    [System.Serializable]
    public class ConfiguracionColision
    {
        public string tag1;
        public string tag2;
        public float desplazamientoZ1;
        public float desplazamientoZ2;
        public int golpesA; // Daño que recibe el objeto con tag1
        public int golpesB; // Daño que recibe el objeto con tag2
    }

    [Header("Configuraciones de colisiones")]
    public List<ConfiguracionColision> colisionesDetectables;

    private void OnTriggerEnter(Collider other)
    {
        string tagA = gameObject.tag;
        string tagB = other.gameObject.tag;

        foreach (var config in colisionesDetectables)
        {
            if ((config.tag1 == tagA && config.tag2 == tagB) || (config.tag1 == tagB && config.tag2 == tagA))
            {
                GameObject objetoA = gameObject;
                GameObject objetoB = other.gameObject;

                float desplazamientoA = (config.tag1 == tagA) ? config.desplazamientoZ1 : config.desplazamientoZ2;
                float desplazamientoB = (config.tag1 == tagB) ? config.desplazamientoZ1 : config.desplazamientoZ2;

                int golpesA = (config.tag1 == tagA) ? config.golpesA : config.golpesB;
                int golpesB = (config.tag1 == tagB) ? config.golpesA : config.golpesB;

                StartCoroutine(MoverObjeto(objetoA, desplazamientoA));
                StartCoroutine(MoverObjeto(objetoB, desplazamientoB));

                // Aplicar daño si los objetos tienen el script Vida
                AplicarGolpe(objetoA, golpesA);
                AplicarGolpe(objetoB, golpesB);

                break;
            }
        }
    }

    IEnumerator MoverObjeto(GameObject objeto, float desplazamientoZ)
    {
        if (objeto == null || desplazamientoZ == 0) yield break;

        Vector3 posicionInicial = objeto.transform.position;
        Vector3 posicionFinal = posicionInicial + new Vector3(0, 0, desplazamientoZ);

        float tiempo = 0;

        while (tiempo < tiempoMovimiento)
        {
            objeto.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempo / tiempoMovimiento);
            tiempo += Time.deltaTime;
            yield return null;
        }

        objeto.transform.position = posicionFinal;
    }

    void AplicarGolpe(GameObject objeto, int cantidad)
    {
        if (cantidad > 0)
        {
            Vida vida = objeto.GetComponent<Vida>();
            if (vida != null)
            {
                vida.RecibirGolpe(cantidad);
            }
        }
    }
}
