using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarObjetos : MonoBehaviour
{
    [Header("Tags a detectar dentro de la jerarquía")]
    public List<string> tagsDetectables; // Lista de tags a monitorear

    private Dictionary<string, int> conteoPorTag = new Dictionary<string, int>(); // Almacena la cantidad de objetos por tag
    private HashSet<GameObject> objetosDetectados = new HashSet<GameObject>(); // Almacena los objetos detectados

    void Start()
    {
        InicializarConteo();
        BuscarObjetosEnJerarquia();
        ImprimirConteoEnConsola();
    }

    void Update()
    {
        BuscarNuevosObjetos();
    }

    void InicializarConteo()
    {
        foreach (string tag in tagsDetectables)
        {
            if (!conteoPorTag.ContainsKey(tag))
            {
                conteoPorTag[tag] = 0;
            }
        }
    }

    void BuscarObjetosEnJerarquia()
    {
        foreach (Transform hijo in GetComponentsInChildren<Transform>())
        {
            VerificarYAgregarObjeto(hijo.gameObject);
        }
    }

    void BuscarNuevosObjetos()
    {
        Transform[] hijos = GetComponentsInChildren<Transform>();

        bool hayCambios = false;

        foreach (Transform hijo in hijos)
        {
            if (!objetosDetectados.Contains(hijo.gameObject))
            {
                hayCambios |= VerificarYAgregarObjeto(hijo.gameObject);
            }
        }

        if (hayCambios)
        {
            ImprimirConteoEnConsola();
        }
    }

    bool VerificarYAgregarObjeto(GameObject objeto)
    {
        if (tagsDetectables.Contains(objeto.tag) && !objetosDetectados.Contains(objeto))
        {
            objetosDetectados.Add(objeto);
            conteoPorTag[objeto.tag]++;
            Debug.Log($"Nuevo objeto detectado: {objeto.name} con tag {objeto.tag}");
            return true;
        }
        return false;
    }

    void ImprimirConteoEnConsola()
    {
        Debug.Log(" Conteo actualizado de objetos por tag:");
        foreach (var entry in conteoPorTag)
        {
            Debug.Log($"Tag: {entry.Key} -> Cantidad: {entry.Value}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objetosDetectados.Contains(other.gameObject))
        {
            Debug.Log($"Colisión detectada entre {gameObject.name} y {other.gameObject.name}");
            // Aquí puedes agregar lógica personalizada según la tag del objeto colisionado
        }
    }
}
