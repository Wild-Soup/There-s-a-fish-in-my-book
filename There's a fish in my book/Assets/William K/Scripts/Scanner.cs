using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public LibrarianAI librarian;
    public float angerIncrease;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            bool isCorrect = GameManager.instance.ScanBook(other.GetComponent<Book>());

            if (isCorrect)
            {

            }
            else
            {
                librarian.IncreaseAnger(angerIncrease);
            }

        }
    }
}
