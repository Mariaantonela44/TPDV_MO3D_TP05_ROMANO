
using UnityEngine;
using TMPro;

public class MetaRodolfo : MonoBehaviour
{
    public GameObject pantallaVictoria;
    private bool victoria = false;

    void Start()
    {
        if (pantallaVictoria != null)
        {
            pantallaVictoria.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision otro)
    {
        ComprobarVictoria(otro.transform);
    }

    void OnTriggerEnter(Collider otro)
    {
        ComprobarVictoria(otro.transform);
    }

    void ComprobarVictoria(Transform otro)
    {
        if (otro.root.GetComponent<ControladorRodolfo>() != null
            && !victoria)
        {
            victoria = true;

            if (pantallaVictoria != null)
            {
                pantallaVictoria.SetActive(true);
            }
            Time.timeScale = 0f;
            Debug.Log("¡VICTORIA!");
        }
    }
}
