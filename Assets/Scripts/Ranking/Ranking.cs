using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Ranking
{
    public const string RankingPath = "rankings.sr";


    /// <summary>
    /// Deleta um rank a partir de um nome
    /// </summary>
    /// <param name="name"></param>
    public static void DeleteRanking(string name)
    {
        //Cria uma lista de scores
        List<SerializableScore> scoreList;
        //Pega os scores atuais gravados no arquivo
        scoreList = GetRankings();
        //Procura um score com a mesma pontuação do parametro e remove
        scoreList.Remove(scoreList.Find(x => x.Name == name));
        //Salva a lista de scores
        SaveRankings(scoreList);


    }
    /// <summary>
    /// Deleta um rank a partir de um score
    /// </summary>
    /// <param name="score"></param>
    public static void DeleteRanking(int score)
    {
        //Cria uma lista de scores
        List<SerializableScore> scoreList;
        //Pega os scores atuais gravados no arquivo
        scoreList = GetRankings();
        //Procura um score com a mesma pontuação do parametro e remove
        scoreList.Remove(scoreList.Find(x => x.Score == score));
        //Salva a lista de scores
        SaveRankings(scoreList);
    }

    /// <summary>
    /// Salva um Score no arquivo rankings.sr
    /// </summary>
    /// <param name="serializableScore"></param>
    public static void SaveRanking(SerializableScore serializableScore)
    {

        List<SerializableScore> scoreList;
        //Caso o arquivo não exista
        if (!File.Exists(GetRankingPath()))
        {
            //Cria uma nova lista de scores
            scoreList = new List<SerializableScore>();
            //Adiciona um score a lista de scores
            scoreList.Add(serializableScore);

        }
        else
        {
            //Caso o arquivo exista, pega os scores dentro dele
            scoreList = GetRankings();

            //Caso a lista estiver vazia ou não existir
            if (scoreList != null || scoreList.Count <= 0)
            {
                //Cria uma nova lista
                scoreList = new List<SerializableScore>();
                //Adiciona o score na lista
                scoreList.Add(serializableScore);
                //Ordena a lista por maior score
                scoreList.Sort((x, y) => y.Score.CompareTo(x.Score));
            }
            else
            {
                //Cria uma nova lista de scores
                scoreList = new List<SerializableScore>();
                //Adiciona um score a lista de scores
                scoreList.Add(serializableScore);
            }
        }

        //Cria um arquivo, ou grava em cima de um existente
        using (FileStream fs = File.Create(GetRankingPath()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            //Serializa a lista para dentro do arquivo
            bf.Serialize(fs, scoreList);
        }


    }
    /// <summary>
    /// Salva uma lista de score no arquivo rankings.sr
    /// </summary>
    /// <param name="scoreList"></param>
    public static void SaveRankings(List<SerializableScore> scoreList)
    {
        //Caso a lista esteja vasia ou não exista a função não é executada
        if (scoreList == null || scoreList.Count <= 0) return;

        //Ordena a lista de scores
        scoreList.Sort((x, y) => y.Score.CompareTo(x.Score));

        using (FileStream fs = File.Create(GetRankingPath()))
        {
            BinaryFormatter bf = new BinaryFormatter();
            //Serializa a lista para dentro do arquivo
            bf.Serialize(fs, scoreList);
        }

    }


    /// <summary>
    /// Retorna uma lista de scores do arquivo do rankings.sr
    /// </summary>
    /// <returns></returns>
    public static List<SerializableScore> GetRankings()
    {

        //Cria uma lista de scores
        List<SerializableScore> score;
        if (File.Exists(GetRankingPath()))
        {
            //Abri o arquivo caso ele exista
            using (FileStream fs = File.Open(GetRankingPath(), FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                //Deserializa a lista e atribui ao objeto score
                score = (List<SerializableScore>)bf.Deserialize(fs);
            }
            //retorna a lista
            return score;
        }

        //Caso não exista o arquivo retorna null
        Debug.LogWarning("Ranking file not exist");
        return null;

    }

    public static string GetRankingPath()
    {
        //Retorna o caminho do arquivo Ranking
        return Path.Combine(Application.persistentDataPath, RankingPath);
    }

    /// <summary>
    /// Função de debug
    /// </summary>
    public static void ListRankings()
    {
        List<SerializableScore> scoreList = GetRankings();

        foreach (SerializableScore a in scoreList)
        {
            Debug.Log(a.Name + " - " + a.Score);
        }
    }
}
