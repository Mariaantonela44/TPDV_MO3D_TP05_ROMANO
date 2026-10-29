using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorRodolfo : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 7f;
    public float velocidadRotacion = 360f;

    public Renderer fondo;
    public Transform indicador;

    private Rigidbody cuerpo;
    private bool enSuelo = true;
    private bool girandoEnAire = false;

    private float alturaIndicador = 1.1f;
    private float distanciaIndicador = 0.45f;

    private Vector3 posicionInicial;

    void Start()
    {
        cuerpo = GetComponent<Rigidbody>();

        if (cuerpo == null)
        {
            Debug.LogError("Rodolfo necesita un Rigidbody.");
            enabled = false;
            return;
        }

        posicionInicial = cuerpo.position;
    }

    void Update()
    {
        if (Time.timeScale == 0f || cuerpo == null)
        {
            return;
        }

        float movimiento = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.leftArrowKey.isPressed ||
                Keyboard.current.aKey.isPressed)
            {
                movimiento = -1f;
            }

            if (Keyboard.current.rightArrowKey.isPressed ||
                Keyboard.current.dKey.isPressed)
            {
                movimiento = 1f;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame && enSuelo)
            {
                cuerpo.AddForce(
                    Vector3.up * fuerzaSalto,
                    ForceMode.Impulse
                );

                enSuelo = false;
                girandoEnAire = true;
            }
        }

        float nuevaX = cuerpo.position.x +
                       movimiento * velocidad * Time.deltaTime;

        Collider colisionador = cuerpo.GetComponent<Collider>();

        if (fondo != null && colisionador != null)
        {
            Bounds limites = fondo.bounds;
            float mitadJugador = colisionador.bounds.extents.x;

            float limiteIzquierdo = limites.min.x + mitadJugador;
            float limiteDerecho = limites.max.x - mitadJugador;

            if (limiteIzquierdo <= limiteDerecho)
            {
                nuevaX = Mathf.Clamp(
                    nuevaX,
                    limiteIzquierdo,
                    limiteDerecho
                );
            }
        }

        if (float.IsNaN(nuevaX) || float.IsInfinity(nuevaX) ||
            float.IsNaN(cuerpo.position.x) ||
            float.IsInfinity(cuerpo.position.x))
        {
            Debug.LogError(
                "Posición inválida de Rodolfo. Revisá su posición y el fondo."
            );

            cuerpo.linearVelocity = Vector3.zero;
            return;
        }

        float velocidadX = 0f;

        if (Time.deltaTime > 0f)
        {
            velocidadX = (nuevaX - cuerpo.position.x) /
                         Time.deltaTime;
        }

        if (float.IsNaN(velocidadX) || float.IsInfinity(velocidadX))
        {
            velocidadX = 0f;
        }

        cuerpo.linearVelocity = new Vector3(
            velocidadX,
            cuerpo.linearVelocity.y,
            0f
        );

        if (girandoEnAire)
        {
            transform.Rotate(
                0f,
                0f,
                -velocidadRotacion * Time.deltaTime,
                Space.World
            );
        }
    }

    void LateUpdate()
    {
        if (Time.timeScale == 0f || indicador == null)
        {
            return;
        }

        indicador.position = new Vector3(
            transform.position.x,
            transform.position.y + alturaIndicador,
            transform.position.z - distanciaIndicador
        );

        indicador.rotation = Quaternion.identity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
            girandoEnAire = false;
            transform.eulerAngles = Vector3.zero;
        }
    }

    public void ReiniciarPosicion()
    {
        if (cuerpo == null)
        {
            return;
        }

        cuerpo.position = posicionInicial;
        cuerpo.linearVelocity = Vector3.zero;
        cuerpo.angularVelocity = Vector3.zero;

        transform.rotation = Quaternion.identity;

        enSuelo = false;
        girandoEnAire = false;

        CuboFlotante[] cubos =
            FindObjectsByType<CuboFlotante>(
                FindObjectsInactive.Include
            );

        foreach (CuboFlotante cubo in cubos)
        {
            cubo.ReiniciarCubo();
        }
    }
}
