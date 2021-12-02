using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsUI : MonoBehaviour
{
    [SerializeField] Notification notifPrefab;
    [SerializeField] float notifDelay;

    bool busy;
    Queue<Notification> nQueue = new Queue<Notification>();

    public void Notify(string text, Color color)
    {
        Notification notif = Instantiate(notifPrefab, transform);
        notif.Text = text;
        notif.Color = color;
        Enqueue(notif);
    }

    public void Notify(string text)
    {
        Notification notif = Instantiate(notifPrefab, transform);
        notif.Text = text;
        Enqueue(notif);
    }

    void Enqueue(Notification notif)
    {
        nQueue.Enqueue(notif);
        if (!busy)
            DisplayNext();
    }

    void DisplayNext()
    {
        if(nQueue.Count > 0)
        {
            busy = true;
            nQueue.Dequeue().Display();
            StartCoroutine(WaitForNext());
        }
        else
        {
            busy = false;
        }
    }

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(notifDelay);
        DisplayNext();
    }
}
