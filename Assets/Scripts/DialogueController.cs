using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
   private AudioSource _audio;
   public List<AudioClip> dialogue = new List<AudioClip>();
   int interact =0;

   void Awake()
   {
      _audio = GetComponent<AudioSource>();
   }
   public void Interact()
   {
      _audio.clip = dialogue[interact];
      _audio.Play();
      interact += 1;
      if(interact >= dialogue.Count) interact = 0;
   }
}
