using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Gradient : MonoBehaviour
{
    public Gradient gradient;

    [Range(0,1)]
    public float t;
    public Color Evaluate(float t)
    {
        return Color.Lerp(Color.white, Color.black, t);
    }

    private Image image;

    void Start()
    {
        image = transform.GetComponent<Image>();
    }
 
    void Update()
    {
        image.color = gradient.Evaluate(t);
    }
}
