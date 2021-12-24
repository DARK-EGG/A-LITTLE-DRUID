using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _FileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();//대사 리스트 
        TextAsset dialogueString = Resources.Load<TextAsset>(_FileName);
        JsonData dialogueData = JsonMapper.ToObject(dialogueString.ToString());

        for (int i = 0; i < dialogueData.Count;i++)
        {
            Dialogue dialogue = new Dialogue();
            dialogue.name = dialogueData[i]["name"].ToString();
            List<string> contextList = new List<string>();
            for(int j = 0; j < dialogueData[i]["lines"].Count; j++)
            {
                contextList.Add(dialogueData[i]["lines"][j]["line"].ToString());
            }
            dialogue.contexts = contextList.ToArray();
            dialogueList.Add(dialogue);
        }
        return dialogueList.ToArray();//각 캐릭터의 대사들 배열로 리턴
    }
}
