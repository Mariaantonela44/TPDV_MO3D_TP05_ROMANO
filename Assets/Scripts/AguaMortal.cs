
using UnityEngine;

public class AguaMortal : MonoBehaviour
{
    private bool reiniciando = false;

    void OnCollisionEnter(Collision otro)
    {
        ComprobarJugador(otro.transform);
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
            reiniciando = true;

            jugador.ReiniciarPosicion();

            reiniciando = false;
        }
    }
}
