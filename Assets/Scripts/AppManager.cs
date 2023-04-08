using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject placementScreen;
    public GameObject DataStructures;

    private GameObject DS_Stack;
    private GameObject DS_LinkedList;

    private void Awake()
    {
        menuScreen.SetActive(false);
        placementScreen.SetActive(true);

        DS_Stack = DataStructures.transform.Find("Stack").gameObject;
        DS_LinkedList = DataStructures.transform.Find("LinkedList").gameObject;

        DS_Stack.SetActive(false);
        DS_LinkedList.SetActive(false);
    }

    void Update()
    {
    }

    public void moveToMenuScreen()
    {
        placementScreen.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void moveToStack()
    {
        menuScreen.SetActive(false);
        DS_Stack.SetActive(true);
    }

    public void moveToLinkedList()
    {
        menuScreen.SetActive(false);
        DS_LinkedList.SetActive(true);
    }
}
