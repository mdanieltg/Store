using System.Text;

namespace StoreWebAPI.Util;

public static class Arrays
{
    public static byte[] Concat(this byte[] leftArray, byte[] rightArray)
    {
        int totalLength = leftArray.Length + rightArray.Length;
        var buffer = new byte[totalLength];

        Array.Copy(
            sourceArray: leftArray,
            destinationArray: buffer,
            length: leftArray.Length);

        Array.Copy(
            sourceArray: rightArray,
            sourceIndex: 0,
            destinationArray: buffer,
            destinationIndex: leftArray.Length,
            length: rightArray.Length);

        return buffer;
    }

    public static string Stringify(this byte[] array)
    {
        StringBuilder stringBuilder = new(array.Length * 2);
        for (var index = 0; index < array.Length; ++index)
        {
            byte b = array[index];
            stringBuilder.Append(b.ToString("X2"));
        }

        return stringBuilder.ToString();
    }
}
