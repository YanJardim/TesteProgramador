using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fazendo a classe ser serializavel para gravar em um arquivo
[System.Serializable]
public class SerializableScore
{
    public string Name { get; set; }
    public int Score { get; set; }

    public SerializableScore(string name, int score)
    {
        this.Name = name;
        this.Score = score;
    }
    public SerializableScore() { }

}
