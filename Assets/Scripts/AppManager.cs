using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AppStates {
    BASE_PLACEMENT = 0,
    DS_MENU = 1,
    DS_STACK = 2,
    DS_LINKEDLIST = 3,
    DS_QUEUE = 4,
};

public class AppManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject placementScreen;
    public GameObject DataStructures;

    private GameObject DS_Stack;
    private GameObject DS_LinkedList;
    private GameObject DS_Queue;

    public AppStates currentState;

    private Button backButton;

    private void Awake()
    {
        menuScreen.SetActive(false);
        placementScreen.SetActive(true);

        DS_Stack = DataStructures.transform.Find("Stack").gameObject;
        DS_LinkedList = DataStructures.transform.Find("LinkedList").gameObject;
        DS_Queue = DataStructures.transform.Find("Queue").gameObject;

        DS_Stack.SetActive(false);
        DS_LinkedList.SetActive(false);
        DS_Queue.SetActive(false);

        currentState = AppStates.BASE_PLACEMENT;
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
    }

    void Update()
    {
        if (currentState == AppStates.BASE_PLACEMENT)
            backButton.enabled = false;
        else
            backButton.enabled = true;
    }

    public void moveToMenuScreen()
    {
        placementScreen.SetActive(false);
        menuScreen.SetActive(true);
        currentState = AppStates.DS_MENU;
    }

    public void moveToStack()
    {
        menuScreen.SetActive(false);
        DS_Stack.SetActive(true);
        currentState = AppStates.DS_STACK;
    }

    public void moveToLinkedList()
    {
        menuScreen.SetActive(false);
        DS_LinkedList.SetActive(true);
        currentState = AppStates.DS_LINKEDLIST;
    }

    public void moveToQueue()
    {
        menuScreen.SetActive(false);
        DS_Queue.SetActive(true);
        currentState = AppStates.DS_QUEUE;
    }

    public void goBack()
    {
        switch(currentState)
        {
            case AppStates.DS_STACK:
                DS_Stack.SetActive(false);
                menuScreen.SetActive(true);
                currentState = AppStates.DS_MENU;
                DS_Stack.GetComponent<Stack>().reset();
            break;
            case AppStates.DS_LINKEDLIST:
                DS_LinkedList.SetActive(false);
                menuScreen.SetActive(true);
                currentState = AppStates.DS_MENU;
                DS_LinkedList.GetComponent<LinkedList>().reset();
            break;
            case AppStates.DS_QUEUE:
                DS_Queue.SetActive(false);
                menuScreen.SetActive(true);
                currentState = AppStates.DS_MENU;
                DS_Queue.GetComponent<Queue>().reset();
            break;
            case AppStates.DS_MENU:
                menuScreen.SetActive(false);
                placementScreen.SetActive(true);
                currentState = AppStates.BASE_PLACEMENT;
            break;
        }
    }
}
