using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MessegeManager : MonoBehaviour
{
    public static MessegeManager instance;
    public TextMeshProUGUI text;


    // Start is called before the first frame update
    void Start()
    {
        // makes this a singelton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateText(string text, float time = 2)
    {
        this.text.text = text;

        Invoke("DesctivateText", time);
    }

    public void DeactivateText()
    {
        text.text = "";
    }

}
