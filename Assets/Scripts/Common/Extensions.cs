using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static partial class Extensions
{
    public static bool TryGetComponentResult<T>(this GameObject go, out T component)
    {
        go.TryGetComponent(out component);
        return component != null;
    }
    public static bool TryGetComponentResult<T>(this Transform transform, out T component)
    {
        return transform.gameObject.TryGetComponentResult(out component);
    }

    public static IEnumerable<T> GetComponentsInChildren<T>(this GameObject go, System.Func<T, bool> selector, bool includeInactive = true)
    {
        return go.GetComponentsInChildren<T>(includeInactive).Where(selector);
    }
    
    public static IEnumerable<Transform> ChildrenTransforms(this Transform transform)
    {
        return Enumerable.Range(0, transform.childCount).Select(num => transform.GetChild(num));
    }
    public static IEnumerable<GameObject> ChildrenGameObjects(this Transform transform)
    {
        return Enumerable.Range(0, transform.childCount).Select(num => transform.GetChild(num).gameObject);
    }

    public static void SetTo(this Transform source, Transform destination, bool global = true)
    {
        if (global)
        {
            destination.SetPositionAndRotation(source.position, source.rotation);
            destination.localScale = destination.InverseTransformVector(source.lossyScale);
        }
        else
        {
            destination.localPosition = source.localPosition;
            destination.localEulerAngles = source.localEulerAngles;
            destination.localScale = source.localScale;
        }
    }

    public static string FullName (this GameObject go) {
        string name = go.name;
        while (go.transform.parent != null) {

            go = go.transform.parent.gameObject;
            name = go.name + "/" + name;
        }
        return name;
    }
    
    public static Vector3 Cast(this IEnumerable<float> array)
    {
        int cnt = array.Count();
        return new Vector3(
            cnt > 0 ? array.ElementAt(0) : 0, 
            cnt > 1 ? array.ElementAt(1) : 0, 
            cnt > 2 ? array.ElementAt(2) : 0);
    }

    public static float AbsMax(this Vector2 vector)
    {
        return Enumerable.Range(0, 2)
            .Select(i => vector[i])
            .Aggregate((max, next) => max > Mathf.Abs(next) ? max : Mathf.Abs(next));
    }
    public static float AbsMax(this Vector3 vector)
    {
        return Enumerable.Range(0, 3)
            .Select(i => vector[i])
            .Aggregate((max, next) => max > Mathf.Abs(next) ? max : Mathf.Abs(next));
    }

    public static Vector3 Scaler(this Vector3 v1, Vector3 v2)
    {
        v1.Scale(v2);
        return v1;
    }
    public static Vector3 Transponate(this Vector3 v1) => new Vector3(1.0f / v1.x, 1.0f / v1.y, 1.0f / v1.z);

    public static IEnumerable<float> ToIEnumerable(this Vector2 v2)
    {
        return Enumerable.Range(0, 2).Select(i => v2[i]);
    }
    public static IEnumerable<float> ToIEnumerable(this Vector3 v3)
    {
        return Enumerable.Range(0, 3).Select(i => v3[i]);
    }
    public static float Average(this IEnumerable<float> arr, bool abs)
    {
        int cnt = arr.Count();
        if (cnt == 0)
            return 0;

        return arr.Aggregate(0.0f,
            (summ, next) => summ + (abs ? Mathf.Abs(next) : next),
            summ => summ / cnt);
    }
    
    
    public static bool None<T>(this IEnumerable<T> _enum, System.Func<T, bool> func) => !_enum.Any(func);

    public static IEnumerable<T> AddToList<T>(this IEnumerable<T> _enum, T element)
    {
        var list = _enum.ToList();
        list.Add(element);
        return list;
    }
    public static IEnumerable<T> AddRangeToList<T>(this IEnumerable<T> _enum, IEnumerable<T> elements)
    {
        var list = _enum.ToList();
        list.AddRange(elements);
        return list;
    }

    public static IEnumerable<T> RemoveAllFromList<T>(this IEnumerable<T> _enum, System.Predicate<T> keySelector)
    {
        var list = _enum.ToList();
        list.RemoveAll(keySelector);
        return list;
    }
    
    /// <summary>
    /// Минимальный элемент по ключу. Для typeof(TKey) == bool работает НЕверно!
    /// Точно не уверен, что эта функция может вывести значнеие default, поэтому рекомендую перед этим проверять значение на существование (.Any())
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="items"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static TItem MinByKey<TItem, TKey>(this IEnumerable<TItem> items, System.Func<TItem, TKey> keySelector)
    {
        var comparer = Comparer<TKey>.Default;

        var enumerator = items.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new System.InvalidOperationException("Collection is empty.");

        TItem minItem = enumerator.Current;
        TKey minKey = keySelector(minItem);


        while (enumerator.MoveNext())
        {
            TKey key = keySelector(enumerator.Current);
            if (comparer.Compare(key, minKey) < 0)
            {
                minItem = enumerator.Current;
                minKey = key;
            }
        }

        return minItem;
    }
    /// <summary>
    /// Минимальный элемент по ключу. Для typeof(TKey) == bool работает НЕверно!
    /// Точно не уверен, что эта функция может вывести значнеие default, поэтому рекомендую перед этим проверять значение на существование (.Any())
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="items"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static int MinByKey_Index<TItem, TKey>(this IEnumerable<TItem> items, System.Func<TItem, TKey> keySelector)
    {
        var comparer = Comparer<TKey>.Default;

        var enumerator = items.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new System.InvalidOperationException("Collection is empty.");

        TItem minItem = enumerator.Current;
        TKey minKey = keySelector(minItem);
        int index = 0;
        int savedIndex = 0;

        while (enumerator.MoveNext())
        {
            TKey key = keySelector(enumerator.Current);
            ++index;
            if (comparer.Compare(key, minKey) < 0)
            {
                minItem = enumerator.Current;
                minKey = key;
                savedIndex = index;
            }
        }

        return savedIndex;
    }
    public static TItem MaxByKey<TItem, TKey>(this IEnumerable<TItem> items, System.Func<TItem, TKey> keySelector)
    {
        var comparer = Comparer<TKey>.Default;

        var enumerator = items.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new System.InvalidOperationException("Collection is empty.");

        TItem maxItem = enumerator.Current;
        TKey maxKey = keySelector(maxItem);


        while (enumerator.MoveNext())
        {
            TKey key = keySelector(enumerator.Current);
            if (comparer.Compare(key, maxKey) > 0)
            {
                maxItem = enumerator.Current;
                maxKey = key;
            }
        }

        return maxItem;
    }
}

public static class RectTransformExtensions
{
    public static void AnchorToCorners(this RectTransform transform)
    {
        if (transform == null)
            throw new System.ArgumentNullException("transform");

        if (transform.parent == null)
            return;

        var parent = transform.parent.GetComponent<RectTransform>();

        Vector2 newAnchorsMin = new Vector2(transform.anchorMin.x + transform.offsetMin.x / parent.rect.width,
                          transform.anchorMin.y + transform.offsetMin.y / parent.rect.height);

        Vector2 newAnchorsMax = new Vector2(transform.anchorMax.x + transform.offsetMax.x / parent.rect.width,
                          transform.anchorMax.y + transform.offsetMax.y / parent.rect.height);

        transform.anchorMin = newAnchorsMin;
        transform.anchorMax = newAnchorsMax;
        transform.offsetMin = transform.offsetMax = new Vector2(0, 0);
    }

    public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
    {
        trans.pivot = aVec;
        trans.anchorMin = aVec;
        trans.anchorMax = aVec;
    }
    public static void SetAnchors(this RectTransform trans, Vector2 aMin, Vector2 aMax)
    {
        trans.anchorMin = aMin;
        trans.anchorMax = aMax;
    }

    public static Vector2 GetSize(this RectTransform trans)
    {
        return trans.rect.size;
    }

    public static float GetWidth(this RectTransform trans)
    {
        return trans.rect.width;
    }

    public static float GetHeight(this RectTransform trans)
    {
        return trans.rect.height;
    }

    public static void SetSize(this RectTransform trans, Vector2 newSize)
    {
        Vector2 oldSize = trans.rect.size;
        Vector2 deltaSize = newSize - oldSize;
        trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
        trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
    }

    public static void SetSizeWithCurrentAnchors(this RectTransform trans, int axis, float size)
    {
        Vector2 sizeDelta = trans.sizeDelta;
        ref Vector2 local = ref sizeDelta;
        
        int savedAxis = axis;
        double _1savedSize = (double) size;
        
        Vector2 vector2 = (trans.parent as RectTransform).GetSize();
        double _2parentSize = (double) vector2[axis];
        
        vector2 = trans.anchorMax;
        double _3anchMax = (double) vector2[axis];
        
        vector2 = trans.anchorMin;
        double _4anchMin = (double) vector2[axis];
        
        double _5anchDelta = _3anchMax - _4anchMin;
        double _6scaledParent = _2parentSize * _5anchDelta;
        double _7result = _1savedSize - _6scaledParent;
        local[savedAxis] = (float) _7result;
        trans.sizeDelta = sizeDelta;
    }

    public static void SetWidth(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(newSize, trans.rect.size.y));
    }

    public static void SetHeight(this RectTransform trans, float newSize)
    {
        SetSize(trans, new Vector2(trans.rect.size.x, newSize));
    }

    public static void SetBottomLeftPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }

    public static void SetTopLeftPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }

    public static void SetBottomRightPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
    }

    public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
    {
        trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
    }
}
