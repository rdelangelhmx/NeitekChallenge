using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.OpenApi.Extensions;

namespace Server.Classes;

public static class EncryptUtils
{
    const string key = "r8B7iC8KK9fD68s0un";

    const string PatronSearch = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz1234567890=$!@#";
    const string PatronEncrypt = "#@!$=0987654321zyxwvutsrqpoñnmlkjihgfedcbaZYXWVUTSRQPOÑNMLKJIHGFEDCBA";

    #region Basic Encription
    public static string EncryptStr(string cadena)
    {
        StringBuilder result = new StringBuilder();
        for (int idx = 0; idx < cadena.Length; idx++)
        {
            result.Append(EncryptarChar(cadena.Substring(idx, 1), cadena.Length, idx));
        }
        return result.ToString();
    }
    private static string EncryptarChar(string caracter, int variable, int a_indice)
    {
        int indice = 0;
        if (PatronSearch.IndexOf(caracter) != -1)
        {
            indice = (PatronSearch.IndexOf(caracter) + variable + a_indice) % PatronSearch.Length;
            return PatronEncrypt.Substring(indice, 1);
        }
        return caracter;
    }
    public static string DecryptStr(string cadena)
    {
        StringBuilder result = new StringBuilder();
        for (int idx = 0; idx < cadena.Length; idx++)
        {
            result.Append(DecryptChar(cadena.Substring(idx, 1), cadena.Length, idx));
        }
        return result.ToString();
    }
    public static string DecryptChar(string caracter, int variable, int a_indice)
    {
        int indice = 0;
        if (PatronEncrypt.IndexOf(caracter) != -1)
        {
            if ((PatronEncrypt.IndexOf(caracter) - variable - a_indice) > 0)
                indice = (PatronEncrypt.IndexOf(caracter) - variable - a_indice) % PatronEncrypt.Length;
            else
                indice = (PatronSearch.Length) + ((PatronEncrypt.IndexOf(caracter) - variable - a_indice) % PatronEncrypt.Length);
            indice = indice % PatronEncrypt.Length;
            return PatronSearch.Substring(indice, 1);
        }
        else
            return caracter;
    }
    #endregion

    #region Medium Encription
    const string _PassPhrase = "Pa$$phrAse";

    const string _SaltValue = "S@lt";

    const string _HashAlgorithm = "SHA1";

    const int _PasswordIterations = 1;

    const string _InitVector = "@1B2c3D4e5F6g7H8";

    const int _KeySize = 256;

    const int DEFAULT_MIN_PASSWORD_LENGTH = 8;

    const int DEFAULT_MAX_PASSWORD_LENGTH = 24;

    const string PASSWORD_CHARS_LCASE = "abcdefghijklmnopqrstuvwxyz";

    const string PASSWORD_CHARS_UCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    const string PASSWORD_CHARS_NUMERIC = "0123456789";

    const string PASSWORD_CHARS_SPECIAL = "#$%&@";

    public static string Encrypt(string plainText)
    {
        try
        {
            byte[] bytes = Encoding.ASCII.GetBytes(_InitVector);
            byte[] bytes2 = Encoding.ASCII.GetBytes(_SaltValue);
            byte[] bytes3 = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(_PassPhrase, bytes2, _HashAlgorithm, _PasswordIterations);
            byte[] bytes4 = passwordDeriveBytes.GetBytes(checked((int)Math.Round((double)_KeySize / 8.0)));
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Mode = CipherMode.CBC;
            ICryptoTransform transform = rijndaelManaged.CreateEncryptor(bytes4, bytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(bytes3, 0, bytes3.Length);
            cryptoStream.FlushFinalBlock();
            byte[] inArray = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(inArray);
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            ex2.Source += " (Encrypt)";
            throw ex2;
        }
    }

    public static string Decrypt(string cipherText)
    {
        checked
        {
            try
            {
                byte[] bytes = Encoding.ASCII.GetBytes(_InitVector);
                byte[] bytes2 = Encoding.ASCII.GetBytes(_SaltValue);
                byte[] array = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(_PassPhrase, bytes2, _HashAlgorithm, _PasswordIterations);
                byte[] bytes3 = passwordDeriveBytes.GetBytes((int)Math.Round((double)_KeySize / 8.0));
                RijndaelManaged rijndaelManaged = new RijndaelManaged();
                rijndaelManaged.Mode = CipherMode.CBC;
                ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes3, bytes);
                MemoryStream memoryStream = new MemoryStream(array);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
                byte[] array2 = new byte[array.Length + 1];
                int count = cryptoStream.Read(array2, 0, array2.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(array2, 0, count);
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                ex2.Source += " (Decrypt)";
                throw ex2;
            }
        }
    }

    public static string GenerateHash(string SourceText)
    {
        try
        {
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] bytes = unicodeEncoding.GetBytes(SourceText);
            SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            byte[] inArray = sHA1CryptoServiceProvider.ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    public static string GeneratePassword()
    {
        return GeneratePassword(DEFAULT_MIN_PASSWORD_LENGTH, DEFAULT_MAX_PASSWORD_LENGTH);
    }

    public static string GeneratePassword(int length)
    {
        return GeneratePassword(length, length);
    }

    public static string GeneratePassword(int minLength, int maxLength)
    {
        if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
        {
            string text = null;
        }

        char[][] array = new char[4][]
        {
                PASSWORD_CHARS_LCASE.ToCharArray(),
                PASSWORD_CHARS_UCASE.ToCharArray(),
                PASSWORD_CHARS_NUMERIC.ToCharArray(),
                PASSWORD_CHARS_SPECIAL.ToCharArray()
        };
        checked
        {
            int[] array2 = new int[array.Length - 1 + 1];
            int num = array2.Length - 1;
            for (int i = 0; i <= num; i++)
            {
                array2[i] = array[i].Length;
            }

            int[] array3 = new int[array.Length - 1 + 1];
            int num2 = array3.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                array3[i] = i;
            }

            byte[] array4 = new byte[4];
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            rNGCryptoServiceProvider.GetBytes(array4);
            int seed = unchecked(((array4[0] & 0x7F) << 24) | (byte)(array4[1] << (0x10 & 7)) | (byte)(array4[2] << (8 & 7))) | array4[3];
            Random random = new Random(seed);
            char[] array5 = null;
            array5 = ((minLength >= maxLength) ? new char[minLength - 1 + 1] : new char[random.Next(minLength - 1, maxLength) + 1]);
            int num3 = array3.Length - 1;
            int num4 = array5.Length - 1;
            for (int i = 0; i <= num4; i++)
            {
                int num5 = ((num3 != 0) ? random.Next(0, num3) : 0);
                int num6 = array3[num5];
                int num7 = array2[num6] - 1;
                int num8 = ((num7 != 0) ? random.Next(0, num7 + 1) : 0);
                array5[i] = array[num6][num8];
                if (num7 == 0)
                {
                    array2[num6] = array[num6].Length;
                }
                else
                {
                    if (num7 != num8)
                    {
                        char c = array[num6][num7];
                        array[num6][num7] = array[num6][num8];
                        array[num6][num8] = c;
                    }

                    array2[num6]--;
                }

                if (num3 == 0)
                {
                    num3 = array3.Length - 1;
                    continue;
                }

                if (num3 != num5)
                {
                    int num9 = array3[num3];
                    array3[num3] = array3[num5];
                    array3[num5] = num9;
                }

                num3--;
            }

            return new string(array5);
        }
    }

    #endregion

    #region Utilidades
    public static int Asc(string s)
    {
        return Encoding.ASCII.GetBytes(s)[0];
    }
    public static char Chr(int c)
    {
        return Convert.ToChar(c);
    }
    public static string Mid(string s, int i, int f)
    {
        return s.Substring(i, f);
    }
    public static string Mid(string s, int i)
    {
        return s.Substring(i);
    }
    public static int Len(string s)
    {
        return s.Length;
    }
    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }

    public static string RandomString(int size, bool lowerCase)
    {
        StringBuilder builder = new StringBuilder();
        Random random = new Random();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }
        if (lowerCase)
            return builder.ToString().ToLower();
        return builder.ToString();
    }

    public static string Getkey()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomString(3, true));
        builder.Append(Utils.Ceros(RandomNumber(1, 9999), 4));
        builder.Append(RandomString(3, false));
        return builder.ToString();
    }
    public static string Getkey(int sInitial, int nMiddle, int sFinal)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(RandomString(sInitial, true));
        builder.Append(Utils.Ceros(RandomNumber(1, int.Parse($"1{Utils.Replicate("0", nMiddle)}") - 1), nMiddle));
        builder.Append(RandomString(sFinal, false));
        return builder.ToString();
    }

    const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
    const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string NUMBERS = "123456789";
    const string SPECIALS = @"!@£$%^&*()#€";

    public static string GeneratePassword(bool useLowercase, bool useUppercase, bool useNumbers, bool useSpecial, int passwordSize)
    {
        char[] _password = new char[passwordSize];
        string charSet = ""; // Initialise to blank
        System.Random _random = new Random();
        int counter;

        // Build up the character set to choose from
        if (useLowercase) charSet += LOWER_CASE;

        if (useUppercase) charSet += UPPER_CAES;

        if (useNumbers) charSet += NUMBERS;

        if (useSpecial) charSet += SPECIALS;

        for (counter = 0; counter < passwordSize; counter++)
        {
            _password[counter] = charSet[_random.Next(charSet.Length - 1)];
        }

        return String.Join(null, _password);
    }
    #endregion

    #region Base 64
    public static string To64(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }
    public static string To64(byte[] plainTextBytes)
    {
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string From64(string base64EncodedData)
    {
        if (!string.IsNullOrEmpty(base64EncodedData))
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        return string.Empty;
    }
    public static string From64(byte[] base64EncodedBytes)
    {
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string Encode64(string toEncode)
    {
        if (!string.IsNullOrEmpty(toEncode))
        {
            var _bytes = System.Text.Encoding.UTF8.GetBytes(toEncode);
            return System.Convert.ToBase64String(_bytes);
        }
        return string.Empty;
    }

    public static string Decode64(string toDecode)
    {
        var _bytes = System.Convert.FromBase64String(toDecode);
        return System.Text.Encoding.UTF8.GetString(_bytes);
    }
    #endregion

    #region SHA1 SHA256 SHA512
    public static byte[] getSha1(string str)
    {
        UTF8Encoding UE = new UTF8Encoding();
        byte[] message = UE.GetBytes(str);
        var hashString = SHA1.Create();
        return hashString.ComputeHash(message);
    }

    public static string getSHA1(string str)
    {
        UTF8Encoding UE = new UTF8Encoding();
        byte[] hashValue;
        byte[] message = UE.GetBytes(str);
        var hashString = SHA1.Create();
        hashValue = hashString.ComputeHash(message);
        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < hashValue.Length; i++)
        {
            strBuilder.Append(hashValue[i].ToString("X2"));
        }
        return strBuilder.ToString();
    }

    public static byte[] getSha256(string str)
    {
        UTF8Encoding UE = new UTF8Encoding();
        byte[] message = UE.GetBytes(str);
        var hashString = SHA256.Create();
        return hashString.ComputeHash(message);
    }

    public static string getSHA256(string str)
    {
        UTF8Encoding UE = new UTF8Encoding();
        byte[] hashValue;
        byte[] message = UE.GetBytes(str);
        var hashString = SHA256.Create();
        hashValue = hashString.ComputeHash(message);
        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < hashValue.Length; i++)
        {
            strBuilder.Append(hashValue[i].ToString("X2"));
        }
        return strBuilder.ToString();
    }

    public static byte[] getSha512(string str)
    {
        UTF8Encoding UE = new UTF8Encoding();
        byte[] message = UE.GetBytes(str);
        var hashString = SHA512.Create();
        return hashString.ComputeHash(message);
    }

    public static string getSHA512(string str)
    {
        UTF8Encoding UE = new UTF8Encoding();
        byte[] hashValue;
        byte[] message = UE.GetBytes(str);
        var hashString = SHA512.Create();
        hashValue = hashString.ComputeHash(message);
        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < hashValue.Length; i++)
        {
            strBuilder.Append(hashValue[i].ToString("X2"));
        }
        return strBuilder.ToString();
    }
    #endregion

    #region AES256
    private static byte[] passwordBytes = new byte[] { 70, 113, 8, 154, 29, 41, 41, 210, 236, 153, 216, 185, 172, 143, 25, 228, 193, 209, 183, 122, 88, 66, 221, 2, 122, 28, 59, 84, 135, 251, 199, 115 };
    public static byte[] AES_EncryptBytes(byte[] bytesToBeEncrypted, byte[] passwordBytesH)
    {
        byte[] encryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;

                var keyEncript = new Rfc2898DeriveBytes(passwordBytesH, saltBytes, 1000);
                AES.Key = keyEncript.GetBytes(AES.KeySize / 8);
                AES.IV = keyEncript.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                    cs.Close();
                }
                encryptedBytes = ms.ToArray();
            }
        }
        return encryptedBytes;
    }

    private static string AES_Key = "61746f732d656e6372797074";

    public static string EncryptText(string input)
    {
        // Get the bytes of the string
        byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);

        // Hash the password with SHA256
        byte[] passwordBytes_2 = SHA256.Create().ComputeHash(passwordBytes);

        byte[] bytesEncrypted = AES_EncryptBytes(bytesToBeEncrypted, passwordBytes_2);

        string result = Convert.ToBase64String(bytesEncrypted);

        return result;
    }

    public static byte[] EncryptAES(string input)
    {
        // Get the bytes of the string
        byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);

        // Hash the password with SHA256
        byte[] passwordBytes_2 = SHA256.Create().ComputeHash(passwordBytes);

        return AES_EncryptBytes(bytesToBeEncrypted, passwordBytes_2);
    }

    public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted)
    {
        byte[] decryptedBytes = null;

        // Set your salt here, change it to meet your flavor:
        // The salt bytes must be at least 8 bytes.
        byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

        using (MemoryStream ms = new MemoryStream())
        {
            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.KeySize = 256;
                AES.BlockSize = 128;

                var keyEncript = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                AES.Key = keyEncript.GetBytes(AES.KeySize / 8);
                AES.IV = keyEncript.GetBytes(AES.BlockSize / 8);

                AES.Mode = CipherMode.CBC;

                using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                    cs.Close();
                }
                decryptedBytes = ms.ToArray();
            }
        }
        return decryptedBytes;
    }

    public static string AESDecryptBytes(byte[] bytesToBeEncrypted)
    {
        byte[] encrip = AES_Decrypt(bytesToBeEncrypted);

        return Encoding.UTF8.GetString(encrip);
    }
    public static string DecryptText(string input)
    {
        // Get the bytes of the string
        byte[] bytesToBeDecrypted = Convert.FromBase64String(input);
        byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted);
        string result = Encoding.UTF8.GetString(bytesDecrypted);

        return result;
    }

    public static byte[] AES_EncryptString(string plainText)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(AES_Key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return array;
    }

    public static string AES_DecryptString(string cipherText)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(AES_Key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
    #endregion

    public static string MD5Hash(string text)
    {
        using (var md5 = MD5.Create())
        {
            var result = md5.ComputeHash(Encoding.ASCII.GetBytes(text));
            return Encoding.ASCII.GetString(result);
        }
    }

    public static X509Certificate2 FindCertificateFromStore(StoreName storeName, StoreLocation storeLocation, X509FindType findType, string findValue)
    {
        X509Store store = new X509Store(storeName, storeLocation);
        store.Open(OpenFlags.ReadOnly);
        try
        {
            var results = store.Certificates.Find(findType, findValue.CleanThumbprint(), true);
            if (results.Count > 0)
                return results[0];
            else
                throw new Exception($"No certificate found in store {storeName.GetDisplayName()} and location {storeLocation.GetDisplayName()}");
        }
        finally
        {
            store.Close();
        }
    }

    /// <summary>
    /// Replace spaces, non word chars and convert to uppercase
    /// </summary>
    /// <param name="mmcThumbprint"></param>
    /// <returns></returns>
    public static string CleanThumbprint(this string mmcThumbprint) => Regex.Replace(mmcThumbprint, @"\s|\W", "").ToUpper();

    #region Hash Passwords
    /// <summary>
    /// https://github.com/mammadkoma/WebApi/blob/master/WebApi/Utilities/PasswordHasher.cs
    /// </summary>
    public const int Pbkdf2Iterations = 1000;

    public static string HashPasswordV3(string password)
    {
        return Convert.ToBase64String(HashPasswordV3(password, RandomNumberGenerator.Create()
            , prf: KeyDerivationPrf.HMACSHA512, iterCount: Pbkdf2Iterations, saltSize: 128 / 8
            , numBytesRequested: 256 / 8));
    }

    public static bool VerifyHashedPasswordV3(string hashedPasswordStr, string password)
    {
        byte[] hashedPassword = Convert.FromBase64String(hashedPasswordStr);
        var iterCount = default(int);
        var prf = default(KeyDerivationPrf);

        try
        {
            // Read header information
            prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedPassword, 1);
            iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
            int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8)
            {
                return false;
            }
            byte[] salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            int subkeyLength = hashedPassword.Length - 13 - salt.Length;
            if (subkeyLength < 128 / 8)
            {
                return false;
            }
            byte[] expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);
#if NETSTANDARD2_0 || NETFRAMEWORK
            return ByteArraysEqual(actualSubkey, expectedSubkey);
#elif NETCOREAPP
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
#else
#error Update target frameworks
#endif
        }
        catch
        {
            // This should never occur except in the case of a malformed payload, where
            // we might go off the end of the array. Regardless, a malformed payload
            // implies verification failed.
            return false;
        }
    }

    private static byte[] HashPasswordV3(string password, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
    {
        byte[] salt = new byte[saltSize];
        rng.GetBytes(salt);
        byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);
        var outputBytes = new byte[13 + salt.Length + subkey.Length];
        outputBytes[0] = 0x01; // format marker
        WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
        return outputBytes;
    }

    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }

    private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
    {
        return ((uint)(buffer[offset + 0]) << 24)
            | ((uint)(buffer[offset + 1]) << 16)
            | ((uint)(buffer[offset + 2]) << 8)
            | ((uint)(buffer[offset + 3]));
    }
    #endregion
}
