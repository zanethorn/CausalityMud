using System.Collections;

namespace Causality.Mud.Common.Components;

public class Container:Component, ICollection<GameObject>
{
    private readonly IList<GameObject> _internalList = new List<GameObject>();
    
    public int Count => _internalList.Count;
    public bool IsReadOnly => false;

    public IEnumerator<GameObject> GetEnumerator() => _internalList.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _internalList.GetEnumerator();

    public void Add(GameObject item)
    {
        _internalList.Add(item);
    }

    public void Clear()
    {
        _internalList.Clear();
    }

    public bool Contains(GameObject item) => _internalList.Contains(item);

    public void CopyTo(GameObject[] array, int arrayIndex) => _internalList.CopyTo(array, arrayIndex);

    public bool Remove(GameObject item)
    {
        return _internalList.Remove(item);
    }


    protected override void OnUpdate(UpdateContext context)
    {
        base.OnUpdate(context);
        foreach (var item in this)
        {
            item.Update(context);
        }
    }
}