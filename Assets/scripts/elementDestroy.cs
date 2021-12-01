using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elementDestroy : MonoBehaviour
{
    public int numero;
    private mouse evento;
    // Start is called before the first frame update
    void Start()
    {
        evento = GameObject.Find("Point Light 2D").GetComponent<mouse>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        evento.loseOrWin(numero);
        Destroy(gameObject);
    }
}
