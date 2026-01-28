using System;
using System.Collections;
using UnityEngine;

namespace FlexusTest.Utils
{
  public static class MonoBehaviorExtensions
  {
    public static Coroutine WaitConditionAndCallAction(this MonoBehaviour mb, Func<bool> condition, Action action) =>
      mb.StartCoroutine(WaitConditionAndCallActionCoroutine(condition, action));

    private static IEnumerator WaitConditionAndCallActionCoroutine(Func<bool> condition, Action action)
    {
      yield return new WaitUntil(condition);
      action();
    }
  }
}