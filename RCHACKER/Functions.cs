using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Ionic;
using Ionic.Zlib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZipFile  = Ionic.Zip.ZipFile; 

namespace RCHACKER
{
    public static class Functions
    {
        private static Encoding encoding = Encoding.UTF8;
        private static FlurlClient client = new FlurlClient("https://app.streets.cafe");
        //our json must be change into two structure 
        public static async Task<string> GetData(string path)
        {
            initClient();
            
            var resultBytes = await client.Request(path).GetBytesAsync();
            
            var str = Functions.DecompressZlib(resultBytes);
            Console.WriteLine("---------response-------- \n " + str + "\n --------------------------------------");
            return str;
        }
        public static async Task<string> PostData(object input)
        {
            initClient();
            var json = JsonConvert.SerializeObject(input).Replace("\n", "").Replace(@"\", "");
         
         
          
            if (json[0] == '"')
            {
                json=json.Remove(0,1);
            }

            if (json.Last() == '"')
            {
                json= json.Remove(json.Length-1);
            }
            var hash = Functions.CreateMD5(json);
            var combineStr = hash + json;
            Console.WriteLine(combineStr);
            var byte1 = Functions.GetBytesFromString(combineStr);
            var final = Functions.CompressZlib(byte1);
            Console.WriteLine("payload byte length:" + final.Length);
         //   Console.WriteLine(Functions.DecompressZlib(final));
            var content = new ByteArrayContent(final);
            var p = await client.Request("/rpc/?t=0").PostAsync(content);
            var resultBytes = await p.GetBytesAsync();
            var str = Functions.DecompressZlib(resultBytes);
            Console.WriteLine("---------response-------- \n " + str + "\n --------------------------------------");
            return str;
        }

        public static int getPurchaseItemGourmetPoint(CustomItem item)
        {
            if (item.cost == 0)
            {
                return Convert.ToInt32(Math.Min(50, Math.Floor(0.025 * item.smcost * 300)));
            }
          //  return Math.min(50, Math.floor(0.025 * inItemConfig.cost)) 
          return  Convert.ToInt32( Math.Min(50, Math.Floor(0.025 * item.cost)));
        }
        private  static void initClient()
        {
            client.WithHeader("Content-Type", "application/octet-stream");
            client.WithHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0");
            client.WithHeader("accept-encoding", "gzip, deflate, br");
            client.WithHeader("te", "trailers");

        }
        private static byte[] GetBytesFromString(string input)
        {
            return encoding.GetBytes(input);
        }

        public static byte[] CompressZlib(byte[] bytes)
        {

            using (MemoryStream ms = new MemoryStream())
            {
                using (Ionic.Zlib.ZlibStream zip = new Ionic.Zlib.ZlibStream(ms, Ionic.Zlib.CompressionMode.Compress, true))
                {
                    zip.Write(bytes, 0, bytes.Length);
                }

                return ms.ToArray();
            }
        }
        private static string DecompressZlib(byte[] data)
        {
           
                //skip the first 3 bytes
                if (data[0] != 120)
                {
                    var index = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] == 120)
                        {
                            index = i;
                            break;
                        }
                    }
                    data = data.Skip(index ).ToArray();
                }

                //if (!(data[0] == 120 && data[1] == 156))
                //{
                //    throw new Exception("error for decompress" );
                //}


                using (var input = new MemoryStream(data))
                {
                    using (Ionic.Zlib.ZlibStream decompressor = new Ionic.Zlib.ZlibStream(input, Ionic.Zlib.CompressionMode.Decompress))
                    {
                        int read = 0;
                        var buffer = new byte[8192];

                        using (MemoryStream output = new MemoryStream())
                        {
                            while ((read = decompressor.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                output.Write(buffer, 0, read);
                            }
                            return encoding.GetString(output.ToArray());
                        }
                    }
                }

            
            

            
        }
       
        //public static byte[] SharpZipLibCompress(byte[] data)
        //{
        //    MemoryStream compressed = new MemoryStream();
        //    DeflaterOutputStream outputStream = new DeflaterOutputStream(compressed);
        //    outputStream.Write(data, 0, data.Length);
        //    outputStream.Close();
        //    return compressed.ToArray();
        //}
        //public static string SharpZipLibDecompress(byte[] data)
        //{
        //    MemoryStream compressed = new MemoryStream(data);
        //    MemoryStream decompressed = new MemoryStream();
        //    InflaterInputStream inputStream = new InflaterInputStream(compressed);
        //    inputStream.CopyTo(decompressed);
        //    return encoding.GetString( decompressed.ToArray());
        //}
        private static string CreateMD5(string rinput)
        {
            // Use input string to calculate MD5 hash
            var input = "jisS121J2fpoh" + "_" + rinput;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

       
    }
}
