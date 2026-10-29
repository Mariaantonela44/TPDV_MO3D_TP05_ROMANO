
using UnityEngine;

public class PinchosMortales : MonoBehaviour
{
    private bool reiniciando = false;

    void OnTriggerEnter(Collider otro)
    {
        ControladorRodolfo jugador =
            otro.GetComponentInParent<ControladorRodolfo>();

        if (jugador == null)
        {
            jugador = otro.transform.root.GetComponent<ControladorRodolfo>();
        }

        if (jugador != null && !reiniciando)
        {
            reiniciando = true;
            jugador.ReiniciarPosicion();
            reiniciando = false;
        }
    }
}
