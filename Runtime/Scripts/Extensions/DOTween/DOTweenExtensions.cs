#if DOTWEEN
/**
 * Authors: Will Lacey
 * Date Created: June 03, 2025
 * 
 * Additional Comments: 
 *      File Line Length: ~140
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Kokowolo.Utilities
{
    public static class DOTweenExtensions
    {
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        public static Tween SetLinkAndId(this Tween tween, GameObject gameObject)
        {
            return tween.SetLink(gameObject).SetId(gameObject.GetInstanceID());
        }

        // public static Tween DOScaleInOut(this Transform transform, bool value, float duration)
        // {
        //     if (value) transform.gameObject.SetActive(true);
        //     transform.localScale = value ? Vector3.zero : Vector3.one;
        //     Tween tween = transform.DOScale(value ? 1 : 0, duration);
        //     if (!value) tween.OnComplete(() => transform.gameObject.SetActive(false));
        //     return tween;
        // }

        // TODO: ScheduleEventManager doesn't really interface with DOTween, change ScheduleEventManager such that it better interacts with it
        public static Sequence AppendCoroutine(this Sequence sequence, IEnumerator routine)
        {
            sequence.AppendCallback(_Run);
            // sequence.AppendInterval(0.05f);
            sequence.AppendCallback(_Pause);
            sequence.AppendInterval(0.05f); // HACK: I think this is needed to guarentee the pause executes
            return sequence;

            void _Run()
            {
                DOTween.instance.StartCoroutine(_Routine());
                IEnumerator _Routine()
                {
                    yield return routine;
                    sequence.Play();
                    // sequence.Complete(withCallbacks: true);
                }
            }
            
            void _Pause() => sequence.Pause();
        }

        // public static Sequence CoroutineSequence(IEnumerator routine)
        // {
        //     return DOTween.Sequence().AppendCoroutine(routine);
        // }

        // public static Sequence JoinCoroutine(this Sequence sequence, IEnumerator routine)
        // {
        //     return sequence.Join(CoroutineSequence(routine));
        // }

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}
#endif