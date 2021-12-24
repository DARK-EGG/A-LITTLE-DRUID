using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public string name;
    public string[] contexts;
}
public class DatabaseManager : MonoBehaviour
{
    public bool en = false;//영어 여부
    public Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();

    void Awake()
    {
        //test용 코드 테스트가 끝나면 지워야 함
        /*Dialogue[] dialogues = GameObject.FindGameObjectWithTag("Manager").GetComponent<DialogueParser>().Parse("Dialogue_KR");
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueDic.Add(i, dialogues[i]);
        }*/
    }

    public string GetDialogue(int id, int index)
    {
        if (index == dialogueDic[id - 1].contexts.Length)
            return null;
        else
            return dialogueDic[id - 1].contexts[index];
    }
}
