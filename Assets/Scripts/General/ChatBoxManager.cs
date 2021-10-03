using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBoxManager : MonoBehaviour
{
    public static ChatBoxManager _instance { get; private set; }
    public RectTransform chatPanel;
    private Image image;
    private TextMeshProUGUI title;
    private TextMeshProUGUI text;
    void Awake()
    {
        _instance = this;

        image = chatPanel.transform.Find("Chat Image").GetComponent<Image>();
        title = chatPanel.transform.Find("Chat Texts").Find("Chat Title").GetComponent<TextMeshProUGUI>();
        text = chatPanel.transform.Find("Chat Texts").Find("Chat Text").GetComponent<TextMeshProUGUI>();

        _instance.CloseChat_private();
    }
    private void OpenChat_private(ChatNPCObject npc)
    {
        chatPanel.gameObject.SetActive(true);
        image.sprite = npc.sprite;
        title.SetText(npc.name);
        title.ForceMeshUpdate();
        text.SetText(npc.message);
        text.ForceMeshUpdate();
    }

    private void CloseChat_private()
    {
        chatPanel.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Update _instance if gets put back to null. This happens on scene change
        if (_instance == null)
        {
            _instance = this;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            _instance.CloseChat_private();
        }
    }

    public static void OpenChat(ChatNPCObject npc)
    {
        _instance.OpenChat_private(npc);
    }

    public static void CloseChat()
    {
        _instance.CloseChat_private();
    }
}
