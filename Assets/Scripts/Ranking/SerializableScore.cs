using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe de score serializavel para ser grava em um arquivo
/// </summary>
[System.Serializable]
public class SerializableScore
{
    //Essas duas variaveis poderiam ser um dictionary também
    public string Name { get; set; }
    public int Score { get; set; }

    public SerializableScore(string name, int score)
    {
        this.Name = name;
        this.Score = score;
    }
    public SerializableScore() { }

}
