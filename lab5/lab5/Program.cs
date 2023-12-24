using System;

class MyMatrix
{
    private int[,] matrix;
    private int rows;
    private int columns;
    private Random random = new Random();

    public MyMatrix(int rows, int columns, int minValue, int maxValue)
    {
        this.rows = rows;
        this.columns = columns;
        matrix = new int[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = random.Next(minValue, maxValue + 1);
            }
        }
    }

    public void ChangeSize(int newRows, int newColumns, int minValue, int maxValue)
    {
        if (newRows == rows && newColumns == columns)
            return;

        int[,] newMatrix = new int[newRows, newColumns];

        for (int i = 0; i < Math.Min(rows, newRows); i++)
        {
            for (int j = 0; j < Math.Min(columns, newColumns); j++)
            {
                newMatrix[i, j] = matrix[i, j];
            }
        }

        if (newRows > rows || newColumns > columns)
        {
            for (int i = 0; i < newRows; i++)
            {
                for (int j = 0; j < newColumns; j++)
                {
                    if (i >= rows || j >= columns)
                    {
                        newMatrix[i, j] = random.Next(minValue, maxValue);
                    }
                }
            }
        }

        matrix = newMatrix;
        rows = newRows;
        columns = newColumns;
    }

    public void PrintPartialy(int startRow, int endRow, int startColumn, int endColumn)
    {
        for (int i = startRow; i <= endRow && i < rows; i++)
        {
            for (int j = startColumn; j <= endColumn && j < columns; j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void Print()
    {
        PrintPartialy(0, rows - 1, 0, columns - 1);
    }

    public int this[int index1, int index2]
    {
        get
        {
            if (index1 >= 0 && index1 - 1 < rows && index2 >= 0 && index2 - 1 < columns)
            {
                return matrix[index1 - 1, index2 - 1];
            }
            else
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
        }
        set
        {
            if (index1 >= 0 && index1 - 1 < rows && index2 >= 0 && index2 - 1 < columns)
            {
                matrix[index1 - 1, index2 - 1] = value;
            }
            else
            {
                throw new IndexOutOfRangeException("Index out of range");
            }
        }
    }
}

class lab051
{
    static void Main()
    {
        Console.Write("Enter the number of rows: ");
        int rows = int.Parse(Console.ReadLine());

        Console.Write("Enter the number of columns: ");
        int columns = int.Parse(Console.ReadLine());

        Console.Write("Enter the minimum value: ");
        int minValue = int.Parse(Console.ReadLine());

        Console.Write("Enter the maximum value: ");
        int maxValue = int.Parse(Console.ReadLine());

        MyMatrix matrix = new MyMatrix(rows, columns, minValue, maxValue);

        Console.WriteLine("Our matrix:");
        matrix.Print();

        Console.WriteLine("\nChanging the matrix size, hah:");
        Console.Write("Enter the new number of rows: ");
        int newRows = int.Parse(Console.ReadLine());

        Console.Write("Enter the new number of columns: ");
        int newColumns = int.Parse(Console.ReadLine());

        matrix.ChangeSize(newRows, newColumns, minValue, maxValue);

        Console.WriteLine("New matrix:");
        matrix.Print();

        Console.WriteLine("\nDisplaying a portion of the matrix 3x3:");
        matrix.PrintPartialy(0, 2, 0, 2);

        Console.WriteLine("\nChanging an element of the matrix. Good luck:");
        Console.Write("Enter the row index: ");
        int rowIndex = int.Parse(Console.ReadLine());

        Console.Write("Enter the column index: ");
        int columnIndex = int.Parse(Console.ReadLine());

        Console.Write("Enter the new value: ");
        int newValue = int.Parse(Console.ReadLine());

        matrix[rowIndex, columnIndex] = newValue;

        Console.WriteLine("Modified matrix:");
        matrix.Print();
    }

}



using System;
using System.Collections;
using System.Collections.Generic;

public class MyList<T> : IEnumerable<T>
{
    private T[] items;
    private int count;

    public MyList()
    {
        items = new T[0];
        count = 0;
    }

    public MyList(params T[] initialValues)
    {
        items = new T[initialValues.Length];
        Array.Copy(initialValues, items, initialValues.Length);
        count = initialValues.Length;
    }

    public MyList(IEnumerable<T> collection)
    {
        items = new T[0];
        foreach (var item in collection)
        {
            Add(item);
        }
    }

    public void Add(T item)
    {
        if (count == items.Length)
        {
            int newCapacity = count == 0 ? 4 : count * 2;
            T[] newItems = new T[newCapacity];
            Array.Copy(items, newItems, count);
            items = newItems;
        }

        items[count] = item;
        count++;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            return items[index];
        }
    }

    public int Count
    {
        get { return count; }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Program
{
    static void Main()
    {
        MyList<int> myList = new MyList<int> { 1, 2, 3, 4, 5 };

        myList.Add(678);

        Console.WriteLine($"Our elements i - 1, cheeeeck:\n");
        foreach (var item in myList)
        {
            Console.WriteLine($"Element: {item}");
        }

        Console.WriteLine($"\nTotal number of elements: {myList.Count}");
    }
}*/




using System;
using System.Collections;

public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private List<KeyValuePair<TKey, TValue>> items = new List<KeyValuePair<TKey, TValue>>();

    public void Add(TKey key, TValue value)
    {
        items.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    public TValue this[TKey key]
    {
        get
        {
            var item = items.Find(i => EqualityComparer<TKey>.Default.Equals(i.Key, key));
            if (item.Equals(default(KeyValuePair<TKey, TValue>)))
            {
                throw new KeyNotFoundException($"The key '{key}' was not found in the dictionary.");
            }
            return item.Value;
        }
    }

    public int Count
    {
        get { return items.Count; }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class lab053
{
    static void Main()
    {
        MyDictionary<string, int> myDict = new MyDictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 }
        };

        //MyDictionary<string, int> myDict = new MyDictionary<string, int>();
        //myDict.Add("one", 1);
        //myDict.Add("two", 2);
        //myDict.Add("three", 3);

        Console.WriteLine("Count: " + myDict.Count);
        Console.WriteLine("\nValue for key 'two': " + myDict["two"]);

        Console.WriteLine("\nIterating through the dictionary:");
        foreach (var kvp in myDict)
        {
            Console.WriteLine($"   {kvp.Key}: {kvp.Value}");
        }
    }
}*/
