using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
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
      float target = 1f;

      if (collision.TryGetComponent<Player>(out Player player))
      {
         _audioSource.Play();

         if (_decriase != null)
         {
            StopCoroutine(_decriase);
         }
         _incriase = StartCoroutine(IncreaseVolume(target));
      }
   }

   private void OnTriggerExit2D(Collider2D collision)
   {
      float target = 0f;

      if (collision.TryGetComponent<Player>(out Player player))
      {
         StopCoroutine(_incriase);
         _decriase = StartCoroutine(IncreaseVolume(target));
      }
   }

   private IEnumerator IncreaseVolume(float target)
   {
      var wait = new WaitForSeconds(0.2F);
      float purpose = 1f;

      if (target == purpose)
      {
         while (_audioSource.volume < target)
         {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _step);
            yield return wait;
         }
      }
      if (target == 0)
      {
         while (_audioSource.volume > target)
         {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _step);
            yield return wait;
         }
      }
   }
}