using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problems
{
    public class Problem59 : ProblemBase
    {
        public override string Title => "XOR decryption";

        public override string Description => @"
Each character on a computer is assigned a unique code and the preferred standard is ASCII (American Standard Code for Information Interchange). For example, uppercase A = 65, asterisk (*) = 42, and lowercase k = 107.

A modern encryption method is to take a text file, convert the bytes to ASCII, then XOR each byte with a given value, taken from a secret key. The advantage with the XOR function is that using the same encryption key on the cipher text, restores the plain text; for example, 65 XOR 42 = 107, then 107 XOR 42 = 65.

For unbreakable encryption, the key is the same length as the plain text message, and the key is made up of random bytes. The user would keep the encrypted message and the encryption key in different locations, and without both 'halves', it is impossible to decrypt the message.

Unfortunately, this method is impractical for most users, so the modified method is to use a password as a key.If the password is shorter than the message, which is likely, the key is repeated cyclically throughout the message.The balance for this method is using a sufficiently long password key for security, but short enough to be memorable.

Your task has been made easy, as the encryption key consists of three lower case characters. Using p059_cipher.txt(right click and 'Save Link/Target As...'), a file containing the encrypted ASCII codes, and the knowledge that the plain text must contain common English words, decrypt the message and find the sum of the ASCII values in the original text.
            ";

        public override string GetAnswer()
        {
            const string filePath = "Data/p059_cipher.txt";
            const byte keyMinVal = 97;
            const byte keyMaxVal = 122;

            var cipher = ReadFileToBytes(filePath);

            for (byte a = keyMinVal; a <= keyMaxVal; a++)
            {
                for (byte b = keyMinVal; b <= keyMaxVal; b++)
                {
                    for (byte c = keyMinVal; c <= keyMaxVal; c++)
                    {
                        var decryptedCipher = Decrypt(cipher, new byte[] { a, b, c });
                        var message = ConvertToString(decryptedCipher);

                        if (IsEnglishText(message))
                        {
                            Console.WriteLine(message);
                            return decryptedCipher.Sum(x => x).ToString();
                        }
                    }
                }
            }

            return "not found";
        }

        private byte[] ReadFileToBytes(string filePath)
        {
            const int bufferSize = 128;

            var readCipher = new List<byte>();
            using (var fileStream = File.OpenRead(filePath))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.ASCII, true, bufferSize))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        readCipher.AddRange(line.Split(',').Select(x => byte.Parse(x)));
                    }
                }
            }

            return readCipher.ToArray();
        }

        private byte[] Decrypt(byte[] cipher, byte[] key)
        {
            var decryptedBytes = new byte[cipher.Length];

            for (int i = 0; i < cipher.Length; i++)
            {
                decryptedBytes[i] = (byte)(cipher[i] ^ key[i % 3]);
            }

            return decryptedBytes;
        }

        private string ConvertToString(byte[] ascii)
        {
            return Encoding.ASCII.GetString(ascii);
        }

        // No need to overcomplicate things
        private bool IsEnglishText(string text)
        {
            return text.Contains(" the ");
        }
    }
}