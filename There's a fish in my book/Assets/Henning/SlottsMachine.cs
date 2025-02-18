using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlottsMachine : MonoBehaviour
{
    public int[] imageValues;


    public Image[] display = new Image[6];
    public float time;
    public List<Sprite> images;
    public float respinDelay;
    public int amountOfAttempts = 5;
    private bool[] notSPin = new bool[6];

    private int[] cyclesValues = new int[6];
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void offEnable()
    {
        Invoke("DealyNum", time);
    }
    public void OnEnable()
    {
        enabled = true;
    }
    void DealyNum()
    {
        enabled = false;
    }
    public IEnumerator SpinMachine()
    {
        notSPin = new bool[6] { false, false, false, false, false, false };
        for (int r = 0; r < amountOfAttempts; r++)
        {
            for (int i = 0; i < cyclesValues.Length; i++)
            {
                cyclesValues[i] = Random.Range(0, images.Count) + 5;

                if (i > 0)
                    cyclesValues[i] += cyclesValues[i - 1];
            }
            int cycles = 1;

            while (cycles < cyclesValues[cyclesValues.Length - 1] + 1)
            {
                for (int i = 0; i < display.Length; i++)
                {
                    if (cycles < cyclesValues[i] && !notSPin[i])
                        display[i].sprite = images[cycles % images.Count];
                }
                cycles++;
                yield return new WaitForSeconds(time);
            }

            DoubleList list = new DoubleList(0);

            foreach (Image sp in display)
                list.AddSprite(sp.sprite);

            for (int i = 0; i < display.Length; i++)
                notSPin[i] = list.CheckIfBiggest(display[i].sprite);

            yield return new WaitForSeconds(respinDelay);
        }

        int win = 0;
        for (int i = 0; i < notSPin.Length; i++)
        {
            if (win == i && notSPin[i])
                win++;
            else
                break;
        }

        win *= imageValues[images.FindIndex(x => x == display[0].sprite)];

        Debug.Log(win);
    }
}

public struct DoubleList
{
    List<Sprite> sprite;
    List<int> amount;

    public DoubleList(int i)
    {
        sprite = new List<Sprite>();
        amount = new List<int>();
    }

    public void AddSprite(Sprite newSprite)
    {
        if (sprite.Contains(newSprite))
        {
            amount[sprite.FindIndex(x => x == newSprite)]++;
        }
        else
        {
            sprite.Add(newSprite);
            amount.Add(1);
        }
    }

    public bool CheckIfBiggest(Sprite testSprite)
    {
        int currentBiggest = 0;

        foreach (int value in amount)
            if (value > currentBiggest)
                currentBiggest = value;

        List<int> indexToRemove = new List<int>();
        for (int i = 0; i < amount.Count; i++)
        {
            if (amount[i] != currentBiggest)
                indexToRemove.Add(i);
        }

        for (int i = 0; i < indexToRemove.Count; i++)
        {
            amount.RemoveAt(indexToRemove[i] - i);
            sprite.RemoveAt(indexToRemove[i] - i);
        }


        if (sprite[0] == testSprite)
            return true;

        return false;
    }
}