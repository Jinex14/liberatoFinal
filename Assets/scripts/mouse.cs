using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using Random = System.Random;

public class mouse : MonoBehaviour
{
    private Light2D lg;
    private float radius = 2.073805f;
    private int animState = 0;
    private float startedRadius = 60;
    private bool moveActive = false;
    [SerializeField] private GameObject endGame;
    [SerializeField] private Vector2[] positionValid;
    [SerializeField] private GameObject[] obj;
    [SerializeField] private GameObject chucu;

    private void Awake()
    {
        lg = GetComponent<Light2D>();
        lg.pointLightOuterRadius = startedRadius;

        var rng = new Random();
        rng.Shuffle(obj);
        rng.Shuffle(obj);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startAnim());
    }

    // Update is called once per frame
    void Update()
    {
        if (moveActive)
        {
            var pos = Input.mousePosition;
            pos.z = 10;
            pos = Camera.main.ScreenToWorldPoint(pos);
            transform.position = Vector3.Lerp(transform.position, pos, 5 * Time.deltaTime);
        }

            if (animState == 1)
        {
            print("aqui");
            startedRadius -= Time.deltaTime * 10;
            if (startedRadius <= 0)
            {
                startedRadius = 0;
                animState = 2;
                chucu.SetActive(false);
            }
            lg.pointLightOuterRadius = startedRadius;
        }

        if(animState == 2)
        {
            startedRadius += Time.deltaTime * 10;
            if (startedRadius >= radius)
            {
                startedRadius = radius;
                animState = 3;
                setshuffleAndPosition(obj);
                moveActive = true;
            }
            lg.pointLightOuterRadius = startedRadius;
        }
    }

    void setshuffleAndPosition(GameObject[] pre)
    {
        var rng = new Random();
        rng.Shuffle(pre);
        rng.Shuffle(pre);
        
        for (int x = 0; x < pre.Length; x++)
        {
            pre[x].transform.position = positionValid[x];
        }
    }

    IEnumerator startAnim()
    {
        yield return new WaitForSeconds(0.2f);
        animState = 1;
    }

   public void loseOrWin(int x)
    {
        print($"{x} - {obj[x].name}");
        if (obj[x].name == "playerEnemigo")
        {
            endGame.SetActive(true);
        }
    }


}


static class RandomExtensions
{
    public static void Shuffle<T>(this Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}