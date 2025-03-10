using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public List<GameObject> Enemies = new List<GameObject>();
   public List<GameObject> characters = new List<GameObject>();
   GameObject _enemy;
   public GameObject player;
   public GameObject Panel;
   public AudioSource bg;
   public AudioClip fight;
   public AudioClip free;
   private void Awake()
   {
     instance = this;
   }
   void Update()
   {
     if(player.GetComponent<_playerStats>().isAlive != true || player == null){
      Panel.SetActive(true);
      StartCoroutine(LoadBase());
     }
   }   
   public void SpawnEnemy(int enemyAmount, List<Transform> SpawnPosition, string enemy)
   {
     bg.clip = fight;
     bg.Play();
     for(int i = 0; i < Enemies.Count; i++)
     {
       if(enemy == Enemies[i].name) _enemy = Enemies[i];
     }
     for(int i = 0; i < enemyAmount; i ++)
     {
        Instantiate(_enemy, SpawnPosition[i].position, Quaternion.identity);
     }
   }
   IEnumerator LoadBase()
   {
     yield return new WaitForSeconds(5f);
     SceneManager.LoadScene("Base Scene");
   }
}
