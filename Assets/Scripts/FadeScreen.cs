using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image image;
    private float fadeSpeed = 2f;
    private Color tempColor;
    int black;
  

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeToBlackScreen()
    {
        gameObject.SetActive(true);
        //black = 0;
        //image.color = new Color(image.color.r, image.color.g, image.color.b, black);
        while (image.color.a < 1f)
        {
            tempColor = image.color;
            tempColor.a += (fadeSpeed * Time.deltaTime);
            image.color = tempColor;
            //print(image.color.a);
            yield return null;

        }
       // gameObject.SetActive(false);
    }

    public IEnumerator FadeBlackToScreen()
    {
        //black = 1;
        //image.color = new Color(image.color.r, image.color.g, image.color.b, black);
        gameObject.SetActive(true);
        while (image.color.a >= 0f)
        {
            tempColor = image.color;
            tempColor.a -= (fadeSpeed * Time.deltaTime);
            image.color = tempColor;
            //print(image.color.a);
            yield return null;

        }

        gameObject.SetActive(false);

            //image.color = Color.Lerp(image.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    public void SetOpacity(int set)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, set);
    }

}
