
using UnityEngine;

public class CamaraSeguidora : MonoBehaviour
{
    public Transform jugador;
    public Renderer fondo;
    public float suavidad = 5f;

    private Camera camara;

    void Start()
    {
        camara = GetComponent<Camera>();
        camara.orthographic = true;
    }

    void LateUpdate()
    {
        if (jugador == null || fondo == null)
        {
            return;
        }

        float mitadPantalla = camara.orthographicSize * camara.aspect;

        Bounds limites = fondo.bounds;

        float limiteIzquierdo = limites.min.x + mitadPantalla;
        float limiteDerecho = limites.max.x - mitadPantalla;

        float posicionX = Mathf.Clamp(
            jugador.position.x,
            limiteIzquierdo,
            limiteDerecho
        );

        Vector3 posicionObjetivo = new Vector3(
            posicionX,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            posicionObjetivo,
            suavidad * Time.deltaTime
        );
    }
}
