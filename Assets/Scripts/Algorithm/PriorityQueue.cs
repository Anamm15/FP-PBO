using System.Collections.Generic;

public class PriorityQueue<T> {
    private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();

    public int Count => elements.Count;

    public void Enqueue(T item, float priority) {
        elements.Add(new KeyValuePair<T, float>(item, priority));
        elements.Sort((x, y) => x.Value.CompareTo(y.Value));
    }

    public T Dequeue() {
        var bestItem = elements[0];
        elements.RemoveAt(0);
        return bestItem.Key;
    }

    public bool Contains(T item) {
        return elements.Exists(x => EqualityComparer<T>.Default.Equals(x.Key, item));
    }
}
