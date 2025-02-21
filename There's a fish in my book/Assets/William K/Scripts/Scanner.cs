using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scanner : MonoBehaviour
{
    public LibrarianAI librarian;
    public float angerIncrease;
    public GameObject librarianAnger;
    public GameObject scannerEffect;
    public Collider scanner;
    private bool isAngerOpen = false;
    private bool isScannerActive = false;
    public InputActionReference librarianAngerButton;
    public InputActionReference scannerButton;

    public Material redScan;
    public Material greenScan;
    public AudioSource susccessSource;
    public AudioSource failedSource;

    public void OnEnable()
    {
        librarianAngerButton.action.Enable();
        scannerButton.action.Enable();

        scannerButton.action.performed += ActivateScanner;
        scannerButton.action.canceled += TurnOffScanner;
        librarianAngerButton.action.performed += OpenAngerMenu;
    }

    public void OpenAngerMenu(InputAction.CallbackContext ctx)
    {
        print("OpenAngerMenu");

        if (!isAngerOpen)
        {
            librarianAnger.SetActive(true);
            isAngerOpen = true;
        }
        else
        {
            librarianAnger.SetActive(false);
            isAngerOpen = false;
        }
    }

    public void ActivateScanner(InputAction.CallbackContext ctx)
    {
        print("ActivateScanner");

        scanner.enabled = true;
        scannerEffect.SetActive(true);
        isScannerActive = true;
    }
    public void TurnOffScanner(InputAction.CallbackContext ctx)
    {
        scanner.enabled = false;
        scannerEffect.SetActive(false);
        isScannerActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book") && other.GetComponent<Book>().isBookOpen)
        {
            bool isCorrect = GameManager.instance.ScanBook(other.GetComponent<Book>());

            if (isCorrect)
            {
                scannerEffect.GetComponent<MeshRenderer>().material.color = greenScan.color;
                susccessSource.Play();
                StartCoroutine(ScanColorChange());
            }
            else
            {
                failedSource.Play();
                librarian.IncreaseAnger(angerIncrease);
            }
        }

    }

    public IEnumerator ScanColorChange()
    {
        yield return new WaitForSeconds(0.5f);
        scannerEffect.GetComponent<MeshRenderer>().material.color = redScan.color;
    }

}
