using System;

public class ListItem<T>
{
    public T Value { get; private set; }

    public ListItem<T> Next { get; set; }

    public ListItem(T value)
    {
        this.Value = value;
    }
}