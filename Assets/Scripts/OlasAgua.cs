using UnityEngine;

public class OlasAgua : MonoBehaviour
{
    public float velocidad = 1f;
    public float amplitud = 0.15f;
    public float frecuencia = 2f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.localPosition;
    }

    void Update()
    {
        float movimiento = Mathf.Sin(Time.time * frecuencia)
                          * amplitud;

        transform.localPosition = new Vector3(
            posicionInicial.x,
            posicionInicial.y + movimiento,
            posicionInicial.z
        );
    }
}
