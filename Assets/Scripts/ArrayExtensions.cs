using System;

namespace DefaultNamespace
{
    public static class ArrayExtensions
    {
        public static void RemoveSameObjects(this Array array)
        {
            for (var i = 0; i < array.Length; i++)
            for (var j = i + 1; j < array.Length; j++)
                if (ReferenceEquals(array.GetValue(i), array.GetValue(j)))
                    array.SetValue(null, j);
        }
    }
}