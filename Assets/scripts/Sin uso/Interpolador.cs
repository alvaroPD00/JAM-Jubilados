using UnityEngine;

public class Interpolador : MonoBehaviour
{
    [Header("Objetos a Modificar")]
    public Transform orbe; // Referencia al objeto Orbe
    public Light luzDireccional; // Referencia a la luz direccional
    public Transform portal; // Referencia al objeto Portal
    public AudioSource audioSource; // Referencia al AudioSource

    [Header("Valores Finales ORBE")]
    public float escalaFinalY = 2f;
    public float posicionFinalY = 10f;

    [Header("Valores Finales LUZ")]
    public float rotacionFinalX = 45f;

    [Header("Valores Finales PORTAL")]
    public float portalEscalaFinalX = 2f;

    [Header("Valores Finales AUDIO")]
    public float pitchInicial = 1f;
    public float pitchFinal = 2f;

    [Header("Valores Finales SKYBOX")]
    public float sunSizeFinal = 0.5f;
    public float atmosphereThicknessFinal = 1.5f;
    public float exposureFinal = 1.2f;

    [Header("Tiempo de Interpolación")]
    public float duracion = 5f;

    private float timer = 0f;
    private float escalaInicialY;
    private float posicionInicialY;
    private float rotacionInicialX;
    private float portalescalaInicialX;

    // Variables para valores iniciales del Skybox
    private Material skyboxMaterial;
    private float sunSizeInicial;
    private float atmosphereThicknessInicial;
    private float exposureInicial;

    void Start()
    {
        // Guardar los valores iniciales
        escalaInicialY = orbe.localScale.y;
        posicionInicialY = orbe.position.y;
        portalescalaInicialX = portal.localScale.x;
        rotacionInicialX = luzDireccional.transform.rotation.eulerAngles.x;

        if (audioSource != null)
            audioSource.pitch = pitchInicial;

        // Obtener el material del Skybox activo
        skyboxMaterial = RenderSettings.skybox;
        if (skyboxMaterial != null)
        {
            sunSizeInicial = skyboxMaterial.GetFloat("_SunSize");
            atmosphereThicknessInicial = skyboxMaterial.GetFloat("_AtmosphereThickness");
            exposureInicial = skyboxMaterial.GetFloat("_Exposure");
        }
    }

    void Update()
    {
        if (timer < duracion)
        {
            timer += Time.deltaTime;
            float progreso = timer / duracion;

            // Interpolación para la escala en X del Portal
            float nuevaEscalaXPortal = Mathf.Lerp(portalescalaInicialX, portalEscalaFinalX, progreso);
            portal.localScale = new Vector3(nuevaEscalaXPortal, portal.localScale.y, portal.localScale.z);

            // Interpolación para la escala proporcional del Orbe
            float nuevaEscalaY = Mathf.Lerp(escalaInicialY, escalaFinalY, progreso);
            orbe.localScale = new Vector3(nuevaEscalaY, nuevaEscalaY, nuevaEscalaY);

            // Interpolación para la posición en Y del Orbe
            float nuevaPosicionY = Mathf.Lerp(posicionInicialY, posicionFinalY, progreso);
            orbe.position = new Vector3(orbe.position.x, nuevaPosicionY, orbe.position.z);

            // Interpolación para la rotación en X de la Luz Direccional
            float nuevaRotacionX = Mathf.Lerp(rotacionInicialX, rotacionFinalX, progreso);
            luzDireccional.transform.rotation = Quaternion.Euler(nuevaRotacionX, luzDireccional.transform.rotation.eulerAngles.y, luzDireccional.transform.rotation.eulerAngles.z);

            // Interpolación para el pitch del AudioSource
            if (audioSource != null)
                audioSource.pitch = Mathf.Lerp(pitchInicial, pitchFinal, progreso);

            // Interpolación de valores del Skybox
            if (skyboxMaterial != null)
            {
                skyboxMaterial.SetFloat("_SunSize", Mathf.Lerp(sunSizeInicial, sunSizeFinal, progreso));
                skyboxMaterial.SetFloat("_AtmosphereThickness", Mathf.Lerp(atmosphereThicknessInicial, atmosphereThicknessFinal, progreso));
                skyboxMaterial.SetFloat("_Exposure", Mathf.Lerp(exposureInicial, exposureFinal, progreso));
            }
        }
    }
}
