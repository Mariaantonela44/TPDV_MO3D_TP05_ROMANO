using UnityEngine;
using System.Collections;

public class CuboAplastante : MonoBehaviour
{
    public float velocidadBajada = 12f;
    public float velocidadSubida = 3f;
    public float tiempoEspera = 1.5f;
    public float distanciaSuelo = 0.5f;

    private Vector3 posicionInicial;
    private float posicionFinalY;

    private bool bajando = false;
    private bool reiniciando = false;

    void Start()
    {
        posicionInicial = transform.position;
        posicionFinalY = transform.position.y - distanciaSuelo;
    }

    void Update()
    {
        if (!bajando && !reiniciando)
        {
            StartCoroutine(CicloAplastante());
        }
    }

    IEnumerator CicloAplastante()
    {
        bajando = true;

        yield return new WaitForSeconds(tiempoEspera);

        while (transform.position.y > posicionFinalY)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(
                    posicionInicial.x,
                    posicionFinalY,
                    posicionInicial.z
                ),
                velocidadBajada * Time.deltaTime
            );

            yield return null;
        }

        yield return new WaitForSeconds(tiempoEspera);

        while (transform.position.y < posicionInicial.y)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                posicionInicial,
                velocidadSubida * Time.deltaTime
            );

            yield return null;
        }

        yield return new WaitForSeconds(tiempoEspera);

        bajando = false;
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

        if (jugador != null && !reiniciando)
        {
            StartCoroutine(ReiniciarJugador(jugador));
        }
    }

    IEnumerator ReiniciarJugador(ControladorRodolfo jugador)
    {
        reiniciando = true;

        jugador.ReiniciarPosicion();

        yield return new WaitForSeconds(0.5f);

        reiniciando = false;
    }
}
