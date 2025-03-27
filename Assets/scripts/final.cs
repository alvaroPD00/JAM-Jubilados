using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jubilado"))
        {
            Debug.Log("Jugador entró en el área del trigger. Cargando escena...");

            if (!string.IsNullOrEmpty(sceneToLoad))
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogWarning("No se ha especificado una escena para cargar.");
            }
        }
    }
}
