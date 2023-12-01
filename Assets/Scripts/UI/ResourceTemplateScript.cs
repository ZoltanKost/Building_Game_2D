using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTemplateScript : MonoBehaviour
{
    private Text text;
    private Image image;
    private void Awake(){
        text = GetComponentInChildren<Text>();
        image = GetComponentInChildren<Image>();
    }

    public void SetImage(Sprite sprite){
        image.sprite = sprite;
    }
    public void SetNumber(int count){
        text.text = count.ToString();
    }
}
