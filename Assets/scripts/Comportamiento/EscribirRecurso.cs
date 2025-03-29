using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EscribirRecurso : MonoBehaviour
{
    public ContadorGolpes contador; // Referencia al ContadorGolpes
    public TextMeshProUGUI textoRecurso; // Referencia al TextMeshProUGUI

    private void Update()
    {
        if (contador != null && textoRecurso != null)
        {
            textoRecurso.text = "" + contador.recurso;
        }
        else
        {
            Debug.LogWarning("MostrarRecurso: Falta asignar ContadorGolpes o TextMeshProUGUI en el Inspector.");
        }
    }
}
