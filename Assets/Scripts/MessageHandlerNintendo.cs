using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageHandlerNintendo : MonoBehaviour
{
    [SerializeField] private MessagesManager messagesManager;
    [SerializeField] private Image Slot1;
    [SerializeField] private Image Slot2;
    [SerializeField] private Image Slot3;

    [SerializeField] private Sprite OizoBackground;
    [SerializeField] private Sprite LapingBackground;
    private MessagesManager.Message[] shownMessages;

    [SerializeField] private AudioClip SendAudio;
    [SerializeField] private AudioClip ReceiveAudio;
    public int NintendoPlayerIndex;
    private AudioSource TPSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        messagesManager = FindAnyObjectByType<MessagesManager>();
        MessagesManager.OnNewMessageSent += OnNewMessageSent;
        shownMessages = new MessagesManager.Message[3] { null, null, null };
        TPSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnNewMessageSent()
    {
        Debug.Log(shownMessages.Length);
        for (int i = shownMessages.Length - 1; i > 0; i--)
        {
            shownMessages[i] = shownMessages[i - 1];
        }
        shownMessages[0] = messagesManager.GetLatestMessage();
        if (shownMessages[0].senderId == NintendoPlayerIndex)
        {
            TPSound.PlayOneShot(SendAudio);
        }
        else
        {
            TPSound.PlayOneShot(ReceiveAudio);
        }
        updateUI();
    }

    void updateUI()
    {
        if (shownMessages[0] != null)
        {
            Slot1.enabled = true;
            Slot1.sprite = shownMessages[0].senderId == 1 ? OizoBackground : LapingBackground;
            Slot1.GetComponentInChildren<TextMeshProUGUI>().text = shownMessages[0].msgContent;
        }
        if (shownMessages[1] != null)
        {
            Slot2.enabled = true;
            Slot2.sprite = shownMessages[1].senderId == 1 ? OizoBackground : LapingBackground;
            Slot2.GetComponentInChildren<TextMeshProUGUI>().text = shownMessages[1].msgContent;
        }
        if (shownMessages[2] != null)
        {
            Slot3.enabled = true;
            Slot3.sprite = shownMessages[2].senderId == 1 ? OizoBackground : LapingBackground;
            Slot3.GetComponentInChildren<TextMeshProUGUI>().text = shownMessages[2].msgContent;
        }
    }
}
