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
    DS_PRIORITYQUEUE = 5,
    DS_LINKEDLIST_MEMORY_SCREEN = 6,
    DS_DOUBLYLINKEDLIST = 7,
};

public class AppManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject placementScreen;
    public GameObject DataStructures;

    private GameObject DS_Stack;
    private GameObject DS_LinkedList;
    private GameObject DS_DoublyLinkedList;
    private GameObject DS_Queue;
    private GameObject DS_PriorityQueue;

    public AppStates currentState;

    private Button backButton;

    private void Awake()
    {
        menuScreen.SetActive(false);
        placementScreen.SetActive(true);

        DS_Stack = DataStructures.transform.Find("Stack").gameObject;
        DS_LinkedList = DataStructures.transform.Find("LinkedList").gameObject;
        DS_DoublyLinkedList = DataStructures.transform.Find("DoublyLinkedList").gameObject;
        DS_Queue = DataStructures.transform.Find("Queue").gameObject;
        DS_PriorityQueue = DataStructures.transform.Find("PriorityQueue").gameObject;

        DS_Stack.SetActive(false);
        DS_LinkedList.SetActive(false);
        DS_Queue.SetActive(false);
        DS_PriorityQueue.SetActive(false);
        DS_DoublyLinkedList.SetActive(false);

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

    public void moveToDoublyLinkedList()
    {
        menuScreen.SetActive(false);
        DS_DoublyLinkedList.SetActive(true);
        currentState = AppStates.DS_DOUBLYLINKEDLIST;
    }

    public void moveToLinkedListMemoryScreen()
    {
        DS_LinkedList.transform.GetChild(0).gameObject.SetActive(true);
        DS_LinkedList.transform.GetChild(1).gameObject.SetActive(false);
        DS_LinkedList.GetComponent<LinkedList>().UpdateMemoryLayout();
        currentState = AppStates.DS_LINKEDLIST_MEMORY_SCREEN;
    }

    public void moveToQueue()
    {
        menuScreen.SetActive(false);
        DS_Queue.SetActive(true);
        currentState = AppStates.DS_QUEUE;
    }

    public void moveToPriorityQueue()
    {
        menuScreen.SetActive(false);
        DS_PriorityQueue.SetActive(true);
        currentState = AppStates.DS_PRIORITYQUEUE;
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
            case AppStates.DS_DOUBLYLINKEDLIST:
                DS_DoublyLinkedList.SetActive(false);
                menuScreen.SetActive(true);
                currentState = AppStates.DS_MENU;
                DS_DoublyLinkedList.GetComponent<DoublyLinkedList>().reset();
            break;
            case AppStates.DS_LINKEDLIST_MEMORY_SCREEN:
                DS_LinkedList.transform.GetChild(0).gameObject.SetActive(false);
                DS_LinkedList.transform.GetChild(1).gameObject.SetActive(true);
                currentState = AppStates.DS_LINKEDLIST;
            break;
            case AppStates.DS_QUEUE:
                DS_Queue.SetActive(false);
                menuScreen.SetActive(true);
                currentState = AppStates.DS_MENU;
                DS_Queue.GetComponent<Queue>().reset();
            break;
            case AppStates.DS_PRIORITYQUEUE:
                DS_PriorityQueue.SetActive(false);
                menuScreen.SetActive(true);
                currentState = AppStates.DS_MENU;
                DS_PriorityQueue.GetComponent<PriorityQueue>().reset();
            break;
            case AppStates.DS_MENU:
                menuScreen.SetActive(false);
                placementScreen.SetActive(true);
                currentState = AppStates.BASE_PLACEMENT;
            break;
        }
    }
}
