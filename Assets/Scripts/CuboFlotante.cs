
using UnityEngine;
using System.Collections;

public class CuboFlotante : MonoBehaviour
{
    public float amplitud = 0.15f;
    public float velocidad = 2f;
    public float tiempoParaDesaparecer = 2f;

    private Vector3 posicionInicial;
    private bool tocado = false;

    private Renderer renderizador;
    private Collider colisionador;
    private Coroutine corrutinaDesaparicion;

    void Start()
    {
        posicionInicial = transform.position;
        renderizador = GetComponent<Renderer>();
        colisionador = GetComponent<Collider>();
    }

    void Update()
    {
        if (!tocado)
        {
            float movimiento = Mathf.Sin(Time.time * velocidad) * amplitud;

            transform.position = new Vector3(
                posicionInicial.x,
                posicionInicial.y + movimiento,
                posicionInicial.z
            );
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        ComprobarJugador(collision.transform);
    }

    void OnTriggerEnter(Collider otro)
    {
        ComprobarJugador(otro.transform);
    }

    void ComprobarJugador(Transform otro)
    {
        ControladorRodolfo jugador =
            otro.GetComponentInParent<ControladorRodolfo>();

        if (jugador == null)
        {
            jugador = otro.root.GetComponent<ControladorRodolfo>();
        }

        if (jugador != null && !tocado)
        {
            tocado = true;
            corrutinaDesaparicion = StartCoroutine(Desaparecer());
        }
    }

    IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(tiempoParaDesaparecer);

        if (renderizador != null)
        {
            renderizador.enabled = false;
        }

        if (colisionador != null)
        {
            colisionador.enabled = false;
        }

        corrutinaDesaparicion = null;
    }

    public void ReiniciarCubo()
    {
        if (corrutinaDesaparicion != null)
        {
            StopCoroutine(corrutinaDesaparicion);
            corrutinaDesaparicion = null;
        }

        transform.position = posicionInicial;
        tocado = false;

        if (renderizador != null)
        {
            renderizador.enabled = true;
        }

        if (colisionador != null)
        {
            colisionador.enabled = true;
        }
    }
}
