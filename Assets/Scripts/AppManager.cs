using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum AppStates {
    BASE_PLACEMENT = 0,
    DS_MENU = 1,
    DS_STACK = 2,
    DS_LINKEDLIST = 3,
    DS_QUEUE = 4,
    DS_PRIORITYQUEUE = 5,
    DS_LINKEDLIST_MEMORY_SCREEN = 6,
    DS_DOUBLYLINKEDLIST = 7,
    DS_CIRCULARQUEUE = 8,
};

public class AppManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject placementScreen;
    public GameObject DataStructures;

    public GameObject PopUp;
    public GameObject HelpScreen;

    private GameObject DS_Stack;
    private GameObject DS_LinkedList;
    private GameObject DS_DoublyLinkedList;
    private GameObject DS_Queue;
    private GameObject DS_CircularQueue;
    private GameObject DS_PriorityQueue;

    public AppStates currentState;

    private Button backButton;

    private void Awake()
    {
        menuScreen.SetActive(false);
        placementScreen.SetActive(true);
        HelpScreen.SetActive(false);

        DS_Stack = DataStructures.transform.Find("Stack").gameObject;
        DS_LinkedList = DataStructures.transform.Find("LinkedList").gameObject;
        DS_DoublyLinkedList = DataStructures.transform.Find("DoublyLinkedList").gameObject;
        DS_Queue = DataStructures.transform.Find("Queue").gameObject;
        DS_PriorityQueue = DataStructures.transform.Find("PriorityQueue").gameObject;
        DS_CircularQueue = DataStructures.transform.Find("CircularQueue").gameObject;

        DS_Stack.SetActive(false);
        DS_LinkedList.SetActive(false);
        DS_Queue.SetActive(false);
        DS_PriorityQueue.SetActive(false);
        DS_DoublyLinkedList.SetActive(false);
        DS_CircularQueue.SetActive(false);

        PopUp.SetActive(false);

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

    public void showHelp()
    {
        if (HelpScreen.activeSelf) {
            HelpScreen.SetActive(false);
            return;
        } else {
            HelpScreen.SetActive(true);
        } 

        GameObject HelperPrompt = HelpScreen.transform.GetChild(0).gameObject;
        GameObject Description = HelpScreen.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).gameObject;

        switch(currentState)
        {
            case AppStates.DS_STACK:
                HelperPrompt.GetComponent<TMP_Text>().text = "Stack";
                Description.GetComponent<TMP_Text>().text = "A stack is a linear data structure that follows the Last-In-First-Out (LIFO) principle. It is a collection of elements where the insertion and deletion of items can only occur at one end called the top. The basic operations that can be performed on a stack are push (adding an item to the top of the stack) and pop (removing an item from the top of the stack). Stacks are commonly used in computer programming and memory management, as well as in algorithms such as depth-first search and backtracking.";
            break;
            case AppStates.DS_LINKEDLIST:
                HelperPrompt.GetComponent<TMP_Text>().text = "Singly Linked List";
                Description.GetComponent<TMP_Text>().text = " A singly linked list is a linear data structure in computer science that consists of a sequence of nodes, where each node stores an element of data and a reference (or pointer) to the next node in the sequence. The first node is called the head of the list, and the last node's reference points to null, indicating the end of the list. Singly linked lists allow for efficient insertion and deletion of elements, but accessing a specific element in the list requires traversing the list from the head until the desired element is reached.  The following operations are performed on a Single Linked List:\n Insertion: The insertion operation can be performed in three ways. They are as follows…\n *Inserting At the Beginning of the list\n *Inserting At End of the list\n *Inserting At Specific location in the list\n Deletion: The deletion operation can be performed in three ways. They are as follows…\n *Deleting from the Beginning of the list\n *Deleting from the End of the list\n *Deleting a Specific Node\n Search: It is a process of determining and retrieving a specific node either from the front, the end or anywhere in the list.\n Display: This process displays the elements of a Single-linked list.";
            break;
            case AppStates.DS_DOUBLYLINKEDLIST:
                HelperPrompt.GetComponent<TMP_Text>().text = "Doubly Linked List";
                Description.GetComponent<TMP_Text>().text = " A doubly linked list is a linear data structure in computer science that consists of a sequence of nodes, where each node stores an element of data and references (or pointers) to the next and previous nodes in the sequence. The first node is called the head of the list, and the last node is called the tail. Doubly linked lists allow for efficient insertion and deletion of elements, as well as efficient traversal of the list in both forward and backward directions. However, they require more memory than singly linked lists due to the extra reference in each node.\n In a double-linked list, we perform the following operations:\n Insertion: The insertion operation can be performed in three ways as follows:\n *Inserting At the Beginning of the list\n *Inserting after a given node.\n *Inserting at the end.\n *Inserting before a given node\n Deletion: The deletion operation can be performed in three ways as follows…\n *Deleting from the Beginning of the list\n *Deleting from the End of the list\n *Deleting a Specific Node\n Display: This process displays the elements of a double-linked list.";
            break;
            case AppStates.DS_LINKEDLIST_MEMORY_SCREEN:
                HelperPrompt.GetComponent<TMP_Text>().text = "Memory";
                Description.GetComponent<TMP_Text>().text = "Memory fragmentation in a linked list can occur when memory is allocated and deallocated for nodes in a non-contiguous manner, leading to inefficient memory utilization. There are two types of memory fragmentation that can affect linked lists: external fragmentation and internal fragmentation.";
            break;
            case AppStates.DS_QUEUE:
                HelperPrompt.GetComponent<TMP_Text>().text = "Queue";
                Description.GetComponent<TMP_Text>().text = "A queue is a linear data structure in computer science that follows the First-In-First-Out (FIFO) principle. It is similar to a queue of people waiting in line, where the first person to join the line is the first to be served. A queue consists of a collection of elements where new elements are added at one end, called the rear or tail, and existing elements are removed from the other end, called the front or head. The basic operations that can be performed on a queue are enqueue (adding an element to the rear of the queue) and dequeue (removing an element from the front of the queue). Queues are commonly used in computer programming for tasks such as scheduling, buffering, and handling requests.";
            break;
            case AppStates.DS_CIRCULARQUEUE:
                HelperPrompt.GetComponent<TMP_Text>().text = "Circular Queue";
                Description.GetComponent<TMP_Text>().text = "A circular queue is a variation of a queue data structure in computer science where the last element in the queue is connected to the first element to create a circular arrangement. This allows for efficient use of space in memory and efficient processing of data in some applications. A circular queue consists of a fixed-size array and two pointers, one pointing to the front of the queue and one pointing to the rear of the queue. The basic operations that can be performed on a circular queue are enqueue (adding an element to the rear of the queue), dequeue (removing an element from the front of the queue), and peek (retrieving the element at the front of the queue without removing it). When the rear pointer reaches the end of the array, it wraps around to the beginning of the array to continue adding elements in a circular fashion. Similarly, when the front pointer reaches the end of the array, it wraps around to the beginning of the array to continue removing elements.";
            break;
            case AppStates.DS_PRIORITYQUEUE:
                HelperPrompt.GetComponent<TMP_Text>().text = "Priority Queue";
                Description.GetComponent<TMP_Text>().text = "A priority queue is a variation of a queue data structure in computer science where each element is assigned a priority value, and the element with the highest priority is dequeued first. Elements with the same priority are dequeued based on their order of arrival in the queue. Priority queues are typically implemented using a heap data structure, which allows for efficient insertion, removal, and retrieval of the highest priority element. The basic operations that can be performed on a priority queue include insertion (adding an element with a priority value), deletion (removing an element with the highest priority), and peeking (retrieving the element with the highest priority without removing it). Priority queues are commonly used in computer programming for tasks such as scheduling, event-driven simulations, and graph algorithms.";
            break;
            case AppStates.DS_MENU:
                HelperPrompt.GetComponent<TMP_Text>().text = "About Auctus";
                Description.GetComponent<TMP_Text>().text = "Auctus is an augmented reality (AR) based computer science education app that is dedicated to teaching data structure implementation. It offers a unique and immersive learning experience by utilizing AR technology to visualize and explore various data structures.\nThe app is designed specifically for computer science students and enthusiasts who want to enhance their understanding and implementation skills in data structures. Auctus utilizes AR to bring these abstract concepts to life by overlaying virtual objects and visualizations onto the real world, making it easier for users to grasp the intricacies of implementing different data structures.\nThrough Auctus, users can interact with three-dimensional representations of popular data structures such as arrays, linked lists, stacks and queues. They can manipulate these structures, observe their behavior, and gain practical experience in implementing them. The AR environment allows users to visualize the inner workings of data structures, understand their relationships, and explore how they store and organize data.";
            break;
            case AppStates.BASE_PLACEMENT:
                HelperPrompt.GetComponent<TMP_Text>().text = "About Auctus";
                Description.GetComponent<TMP_Text>().text = "Auctus is an augmented reality (AR) based computer science education app that is dedicated to teaching data structure implementation. It offers a unique and immersive learning experience by utilizing AR technology to visualize and explore various data structures.\nThe app is designed specifically for computer science students and enthusiasts who want to enhance their understanding and implementation skills in data structures. Auctus utilizes AR to bring these abstract concepts to life by overlaying virtual objects and visualizations onto the real world, making it easier for users to grasp the intricacies of implementing different data structures.\nThrough Auctus, users can interact with three-dimensional representations of popular data structures such as arrays, linked lists, stacks and queues. They can manipulate these structures, observe their behavior, and gain practical experience in implementing them. The AR environment allows users to visualize the inner workings of data structures, understand their relationships, and explore how they store and organize data.";
            break;
        }
    }

    public void moveToQueue()
    {
        menuScreen.SetActive(false);
        DS_Queue.SetActive(true);
        currentState = AppStates.DS_QUEUE;
    }

    public void moveToCircularQueue()
    {
        menuScreen.SetActive(false);
        DS_CircularQueue.SetActive(true);
        currentState = AppStates.DS_CIRCULARQUEUE;
    }

    public void moveToPriorityQueue()
    {
        menuScreen.SetActive(false);
        DS_PriorityQueue.SetActive(true);
        currentState = AppStates.DS_PRIORITYQUEUE;
    }

    public void goBack()
    {
        if (HelpScreen.activeSelf) {
            HelpScreen.SetActive(false);
            return;
        }

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
            case AppStates.DS_CIRCULARQUEUE:
                DS_CircularQueue.SetActive(false);
                menuScreen.SetActive(true);
                currentState = AppStates.DS_MENU;
                DS_CircularQueue.GetComponent<CircularQueue>().reset();
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
