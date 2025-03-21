using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialImage : MonoBehaviour
{
    public List<Sprite> images = new List<Sprite>();
    int index;

    public void Start()
    {
        index = 0;
        GetComponent<Image>().sprite = images[index];
    }

    public void ChangeImage(string buttonType)
    {
        switch (buttonType)
        {
            case "Forward":
                if (index < images.Count-1)
                {
                    index++;
                    GetComponent<Image>().sprite = images[index];
                }
                else if(index == images.Count-1)
                {
                    index = 0;
                    GetComponent<Image>().sprite = images[index];
                }
                break;
            case "Backward":
                if (index > 0)
                {
                    index--;
                    GetComponent<Image>().sprite = images[index];
                }
                else if(index == 0)
                {
                    index = images.Count - 1;
                    GetComponent<Image>().sprite = images[index];
                }
                break;
            default:
                break;
        }
    }
}
public enum Button { Forward, Backward }