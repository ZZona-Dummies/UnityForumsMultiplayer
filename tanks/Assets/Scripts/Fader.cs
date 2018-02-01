using UnityEngine;

public class Fader : MonoBehaviour
{
    private void Update()
    {
        Color temp = GetComponent<Renderer>().material.color;
        temp.a -= 0.01f;
        GetComponent<Renderer>().material.color = temp;
    }
}