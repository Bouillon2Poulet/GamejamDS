using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MessagesManager : MonoBehaviour
{
    private uint currentMessageIndex = 0;

    public class Message
    {
        public uint senderId;
        public string msgContent;
        public Message(uint senderId, string msgContent) { this.senderId = senderId; this.msgContent = msgContent; }
    }

    public static event Action OnNewMessageSent;

    private List<Message> MessagesQueue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MessagesQueue = new List<Message>
{
    new Message(2, "t ou??"),
    new Message(1, "j arrive attend"),
    new Message(1, "je susi perdu !!!"),
    new Message(2, "je sui au 3e je croi"),
    new Message(2, "je desceng alors"),
    new Message(1, "je trouve pas l escalier??"),
    new Message(1, "je suis passe devant la pisine"),
    new Message(1, "je me suis encore perdu"),
    new Message(2, "il arrive !!!!"),
    new Message(2, "fais attention a toa<3"),
    new Message(1, "g vu une chose bizare"),
    new Message(2, "la piece a change"),
    new Message(1, "je sais pas ou je suis.."),
    new Message(1, "tu reconnais quelque chose?"),
    new Message(2, "je fais le plus vite que je peu!!"),
    new Message(2, "tu devrais peut etre revenir en arriere"),
    new Message(1, "je suis coince dans un couloir"),
    new Message(1, "je trouve pas la sorti"),
    new Message(2, "g vu un petit passage tout a l heure.."),
    new Message(2, "allo"),
    new Message(2, "tu m entend"),
    new Message(1, "j ai peur"),
    new Message(1, "il me suis!!!"),
    new Message(1, "ct toi ?"),
    new Message(2, "tu sais ce que c cet endroit ?"),
    new Message(2, "trop bizarre ici"),
    new Message(1, "je t avais ramene un souvenir de vacance"),
    new Message(1, "il faut que je te raconte un truk"),
    new Message(2, "c toi joshua??"),
    new Message(1, "qui d'autre ?"),
    new Message(1, "tu m esquive ou quoi.."),
    new Message(2, "on va se retrouver"),
    new Message(2, "je te cherche"),
    new Message(1, "pourquoi tu repond plu"),
    new Message(1, "tu fais la tete?"),
    new Message(1, "tu te cache?"),
    new Message(2, "on se retrouve ou")
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
            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5));
            Debug.Log(MessagesQueue[(int)currentMessageIndex].msgContent);
            OnNewMessageSent.Invoke();
            currentMessageIndex++;
        }
    }

    public Message GetLatestMessage()
    {
        return MessagesQueue[(int)currentMessageIndex];
    }
}
