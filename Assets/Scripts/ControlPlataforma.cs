using UnityEngine;
using System.Collections;

public class ControlPlataforma : MonoBehaviour
{
    public Transform plataforma1;
    public Transform plataforma2;
    public Transform plataforma3;

    public float distanciaAbajo = 5f;
    public float velocidad = 2f;

    private Vector3 destino1;
    private Vector3 destino2;
    private Vector3 destino3;

    private bool activado = false;

    void Start()
    {
        destino1 = plataforma1.position;
        destino2 = plataforma2.position;
        destino3 = plataforma3.position;

        plataforma1.position -= Vector3.up * distanciaAbajo;
        plataforma2.position -= Vector3.up * distanciaAbajo;
        plataforma3.position -= Vector3.up * distanciaAbajo;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ControladorRodolfo>() != null
            && !activado)
        {
            bool tocaDesdeArriba = false;

            foreach (ContactPoint contacto in collision.contacts)
            {
                if (contacto.normal.y < -0.5f)
                {
                    tocaDesdeArriba = true;
                }
            }

            if (tocaDesdeArriba)
            {
                activado = true;

                StartCoroutine(Subir(plataforma1, destino1));
                StartCoroutine(Subir(plataforma2, destino2));
                StartCoroutine(Subir(plataforma3, destino3));
            }
        }
    }

    IEnumerator Subir(Transform plataforma, Vector3 destino)
    {
        if (plataforma == null)
        {
            yield break;
        }

        while (Vector3.Distance(plataforma.position, destino) > 0.01f)
        {
            plataforma.position = Vector3.MoveTowards(
                plataforma.position,
                destino,
                velocidad * Time.deltaTime
            );

            yield return null;
        }

        plataforma.position = destino;
    }
}
