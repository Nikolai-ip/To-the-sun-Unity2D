using System;

namespace DefaultNamespace
{
    public static class ArrayExtensions
    {
        public static void RemoveSameObjects(this Array array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i+1; j < array.Length; j++)
                {
                    if (ReferenceEquals(array.GetValue(i),array.GetValue(j)))
                    {
                        array.SetValue(null,j);
                    }
                }
            }
        }
    }
}