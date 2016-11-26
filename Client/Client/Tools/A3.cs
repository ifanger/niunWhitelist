using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Client.Tools
{
    class A3
    {
        public static string UIDtoGUID(string sUID)
        {
            long UID;

            if (sUID.Length != 17)
                throw new Exception("A UID fornecida é inválida.");

            if (!long.TryParse(sUID, out UID))
                throw new Exception("A UID fornecida não é numérica.");

            byte[] parts = new byte[] { 0x42, 0x45, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 2; i < 10; i++)
            {
                NiunDiv res = new NiunDiv(UID);
                UID = res.Quotient();
                parts[i] = (byte)res.Remainder();
            }

            string md5hashed;

            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] hash = md5.ComputeHash(parts);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }

            md5hashed = sb.ToString();

            return md5hashed;
        }

        public static string ValidateUsername(string sName)
        {
            if (string.IsNullOrEmpty(sName))
                throw new Exception("Você não digitou um nome de usuário.");

            if (sName.Contains("\n"))
                throw new Exception("Nome de usuário inválido.");

            return sName;
        }
    }
}
