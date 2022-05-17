using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
   [SerializeField] private float _step = 0.07f;
   [SerializeField] private float _wait = 0.2f;

   private AudioSource _audioSource;
   private Coroutine _volume;

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

         if (_volume != null)
         {
            StopCoroutine(_volume);
         }
         _volume = StartCoroutine(ChangeVolume(target));
      }
   }

   private void OnTriggerExit2D(Collider2D collision)
   {
      float target = 0f;

      if (collision.TryGetComponent<Player>(out Player player))
      {
         StopCoroutine(_volume);
         _volume = StartCoroutine(ChangeVolume(target));
      }
   }

   private IEnumerator ChangeVolume(float target)
   {
      while (_audioSource.volume != target)
      {
         _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _step);
         yield return new WaitForSeconds(_wait);
      }
      yield break;
   }
}