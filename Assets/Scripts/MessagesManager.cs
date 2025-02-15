using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessagesManager : MonoBehaviour
{
    private uint currentMessageIndex = 0;

    class Message
    {
        public uint senderId;
        public string msgContent;
        public Message(uint senderId, string msgContent) { this.senderId = senderId; this.msgContent = msgContent; }
    }
    private List<Message> MessagesQueue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MessagesQueue = new List<Message>
        {
            new Message(1, "cc"),
            new Message(2, "slt"),
            new Message(1, "toi ossi t coincé"),
            new Message(2, "ui"),
            new Message(1, "B^)")
        };

        StartCoroutine("SendMessages");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SendMessages()
    {
        int lastMessageIndex = -1;

        while (currentMessageIndex < MessagesQueue.Count)
        {
            yield return new WaitForSeconds(Random.Range(0, 1));
            Debug.Log(MessagesQueue[(int)currentMessageIndex].msgContent);
            currentMessageIndex++;
        }
    }
}
