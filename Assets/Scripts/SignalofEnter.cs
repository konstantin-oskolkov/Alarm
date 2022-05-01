using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SignalofEnter : MonoBehaviour
{
   [SerializeField] private float _step = 0.07f;
   private AudioSource _audioSource;
   private Coroutine _incriase;
   private Coroutine _decriase;

   private void Start()
   {
      _audioSource = GetComponent<AudioSource>();
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.TryGetComponent<Player>(out Player player))
      {
         _audioSource.Play();
         if (_decriase != null)
         {
            StopCoroutine(_decriase);
         }
         _incriase = StartCoroutine(IncreaseVolume());
      }
   }
   private void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.TryGetComponent<Player>(out Player player))
      {
         StopCoroutine(_incriase);
         _decriase = StartCoroutine(DeacreaseVolume());
      }
   }

   private IEnumerator IncreaseVolume()
   {
      var wait = new WaitForSeconds(0.2F);
      float purpose = 1f;

      while (_audioSource.volume < 1)
      {
         _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, purpose, _step);
         yield return wait;
      }
   }

   private IEnumerator DeacreaseVolume()
   {
      var wait = new WaitForSeconds(0.2F);
      float purpose = 0f;

      while (_audioSource.volume > 0)
      {
         _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, purpose, _step);
         yield return wait;
      }
   }
}
