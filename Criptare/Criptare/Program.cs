using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Introduceti cheia: ");
        string key = Console.ReadLine().ToUpper();
        Console.Write("Introduceti textul pentru criptare: ");
        string text = Console.ReadLine();

        string encryptedText = Encrypt(key, text);
        Console.WriteLine($"Textul criptat: {encryptedText}");

        string decryptedText = Decrypt(key, encryptedText);
        Console.WriteLine($"Textul decriptat: {decryptedText}");
    }

    static string Encrypt(string key, string text)
    {
        var sortedKey = key.OrderBy(c => c).ToArray();
        var keyOrder = key.Select(c => Array.IndexOf(sortedKey, c) + 1).ToArray();  // +1 because array index starts from 0
        var textLength = ((text.Length - 1) / key.Length + 1) * key.Length;

        StringBuilder sb = new StringBuilder(textLength);
        sb.Append(text);

        int fillerChar = 0;  // counter for the filler characters
        for (int i = text.Length; i < textLength; i++)
        {
            sb.Append((char)('A' + fillerChar % 26));
            fillerChar++;
        }

        // Print key
        Console.WriteLine("Matricea este:");
        foreach (char c in key)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();

        foreach (int order in keyOrder)
        {
            Console.Write(order + " ");
        }
        Console.WriteLine();

        for (int i = 0; i < textLength / key.Length; i++)
        {
            for (int j = 0; j < key.Length; j++)
            {
                Console.Write(sb[j + i * key.Length] + " ");
            }
            Console.WriteLine();
        }

        StringBuilder encryptedText = new StringBuilder(textLength);

        for (int i = 0; i < key.Length; i++)
        {
            int col = Array.IndexOf(keyOrder, i + 1);  // +1 because array index starts from 0

            for (int row = 0; row < textLength / key.Length; row++)
            {
                encryptedText.Append(sb[col + row * key.Length]);
            }
        }

        return encryptedText.ToString();
    }


    static string Decrypt(string key, string encryptedText)
    {
        var sortedKey = key.OrderBy(c => c).ToArray();
        var keyOrder = key.Select(c => Array.IndexOf(sortedKey, c) + 1).ToArray();
        var textLength = encryptedText.Length;

        StringBuilder decryptedText = new StringBuilder(textLength);

        int rows = (textLength + key.Length - 1) / key.Length;
        int cols = key.Length;

        char[,] matrix = new char[rows, cols];

        int index = 0;
        for (int i = 0; i < cols; i++)
        {
            int col = Array.IndexOf(keyOrder, i + 1);

            for (int row = 0; row < rows; row++)
            {
                if (index < textLength)
                {
                    matrix[row, col] = encryptedText[index++];
                }
                else
                {
                    break;
                }
            }
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                decryptedText.Append(matrix[row, col]);
            }
        }

        // Remove filler characters
        int fillerChar = 0;
        while (fillerChar < textLength && decryptedText[decryptedText.Length - 1] >= 'A' && decryptedText[decryptedText.Length - 1] <= 'Z')
        {
            decryptedText.Length -= 1;
            fillerChar++;
        } 

        return decryptedText.ToString();
    }




}
