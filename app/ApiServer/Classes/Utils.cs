using Newtonsoft.Json;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Xml;

namespace Server.Classes;

public static class Utils
{
    public static string[] vocales = new string[] { "a", "e", "i", "o", "u" };
    public static JsonSerializerOptions jsonOptions { get; } = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNameCaseInsensitive = true, ReferenceHandler = ReferenceHandler.IgnoreCycles };
    public static FormUrlEncodedContent GetUrlEncodedContent(this object content) => new FormUrlEncodedContent(System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(System.Text.Json.JsonSerializer.Serialize(content, Utils.jsonOptions), Utils.jsonOptions));
    public static StringContent GetBodyContent(this object content) => new StringContent(System.Text.Json.JsonSerializer.Serialize(content, Utils.jsonOptions), Encoding.UTF8, "application/json");
    public static string GetPath(params string[] dirs) => Path.Combine(dirs);
    /// <summary>
    /// Get Culture Info 
    /// </summary>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static CultureInfo appLanguage(string cultureInfo) => CultureInfo.CreateSpecificCulture(cultureInfo);
    /// <summary>
    /// Get Time Zone Info
    /// </summary>
    /// <param name="timeZone"></param>
    /// <returns></returns>
    public static TimeZoneInfo appTimeZone(string timeZone) => TimeZoneInfo.FindSystemTimeZoneById(timeZone);
    /// <summary>
    /// Wait n secods
    /// </summary>
    /// <param name="Segundos"></param>
    public static void Sleep(double Segundos) => Thread.Sleep((int)(1000 * Segundos));
    public static async Task SleepAsync(double Segundos) => await Task.Delay((int)(1000 * Segundos));
    /// <summary>
    /// Put 0 at left in string
    /// </summary>
    /// <param name="nNumero"></param>
    /// <param name="nPos"></param>
    /// <returns></returns>
    public static string Ceros(int nNumero, int nPos)
    {
        StringBuilder sCeros = new StringBuilder();
        for (int i = 1; i <= nPos; i++)
        {
            sCeros.Append("0");
        }
        sCeros.Append(nNumero.ToString());
        return Right(sCeros.ToString(), nPos);
    }
    /// <summary>
    /// Function left
    /// </summary>
    /// <param name="param"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Left(string param, int length) => param.Substring(0, length);
    /// <summary>
    /// Function right
    /// </summary>
    /// <param name="param"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Right(string param, int length) => param.Substring(param.Length - length, length);
    /// <summary>
    /// Repeat "n" times an char
    /// </summary>
    /// <param name="Caracter"></param>
    /// <param name="Veces"></param>
    /// <returns></returns>
    public static string Replicate(string Caracter, int Veces)
    {
        StringBuilder Resultado = new StringBuilder();
        for (int i = 0; i <= Veces; i++)
        {
            Resultado.Append(Caracter);
        }
        return Resultado.ToString();
    }
    /// <summary>
    /// Replaces characters in string that have diacritics (a glyph added to a letter or basic glyph)
    /// </summary>
    /// <param name="stIn"></param>
    /// <returns></returns>
    public static string RemoveDiacritics(this string stIn)
    {
        string stFormD = stIn.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new StringBuilder();

        for (int ich = 0; ich < stFormD.Length; ich++)
        {
            UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(stFormD[ich]);
            }
        }

        return (sb.ToString().Normalize(NormalizationForm.FormC));
    }
    /// <summary>
    /// Try parse nullable string into int?
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    public static int? TryParseNullable(string val) => int.TryParse(val, out int outValue) ? (int?)outValue : null;
    /// <summary>
    /// Format int value into 9,990
    /// </summary>
    /// <param name="Valor"></param>
    /// <returns></returns>
    public static string FormatNumber(this int? Valor) => Valor.HasValue ? string.Format("{0:#,#0}", Valor) : "";
    /// <summary>
    /// Format decimal value into 9,990.00
    /// </summary>
    /// <param name="Valor"></param>
    /// <param name="Decimales"></param>
    /// <returns></returns>
    public static string FormatDecimal(this decimal? Valor, int Decimales = 2) => Valor.HasValue ? string.Format("{0:#,#0." + Replicate("0", Decimales) + "}", Valor) : "";
    /// <summary>
    /// Format decimal value into $ 9,990.00
    /// </summary>
    /// <param name="Valor"></param>
    /// <param name="Decimales"></param>
    /// <returns></returns>
    public static string FormatCurrency(this decimal? Valor, int Decimales = 2) => Valor.HasValue ? $"$ {string.Format("{0:#,#0." + Replicate("0", Decimales) + "}", Valor)}" : "";
    /// <summary>
    /// Format DateTime into specific format date
    /// </summary>
    /// <param name="Valor"></param>
    /// <param name="Formato"></param>
    /// <returns></returns>
    public static string FormatDate(this DateTime? Valor, string Formato) => Valor.HasValue ? string.Format("{0:" + Formato + "}", Valor).Replace(".", "").ToUpper() : "";
    /// <summary>
    /// Format string phone number 999.999.9999
    /// </summary>
    /// <param name="Valor"></param>
    /// <param name="Formato"></param>
    /// <returns></returns>
    public static string FormatPhone(this string Valor) => string.IsNullOrEmpty(Valor) ? Valor : $"{Valor.Substring(0, 3)}.{Valor.Substring(3, 3)}.{Valor.Substring(6)}";
    /// <summary>
    /// Get today's Date & Time from timeZone
    /// </summary>
    /// <param name="timeZone"></param>
    /// <returns></returns>
    public static DateTime getNow(TimeZoneInfo timeZone) => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
    /// <summary>
    /// Get plural 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetPluralName(string name)
    {
        StringBuilder pluralName = new StringBuilder();
        pluralName.Append(name).Append(Array.FindIndex(vocales, f => f.Equals(Right(name, 1))) > 0 ? "es" : "s");
        return pluralName.ToString();
    }
    public static T GetEnumValue<T>(this string enumValue) => (T)Enum.Parse(typeof(T), enumValue, true);
    /// <summary>
    /// Get string from HashSet
    /// </summary>
    /// <param name="strHashSet"></param>
    /// <returns></returns>
    public static string StringHashSet(this string strHashSet) => Regex.Replace(strHashSet, @"(\s-|""|\[|\])", "");
    /// <summary>
    /// Convert a AsyncEnumerable to List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items, CancellationToken cancellationToken = default)
    {
        var results = new List<T>();
        await foreach (var item in items.WithCancellation(cancellationToken)
                                        .ConfigureAwait(false))
            results.Add(item);
        return results;
    }
    /// <summary>
    /// Get mime type from array
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetContentType(this string path)
    {
        var types = GetMimeTypes();
        var ext = Path.GetExtension(path).ToLowerInvariant();
        return types[ext];
    }
    /// <summary>
    /// Get Array for Mime Types
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, string> GetMimeTypes()
    {
        return new Dictionary<string, string>
                {
                    {".txt", "text/plain"},
                    {".pdf", "application/pdf"},
                    {".doc", "application/vnd.ms-word"},
                    {".docx", "application/vnd.ms-word"},
                    {".xls", "application/vnd.ms-excel"},
                    {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                    {".png", "image/png"},
                    {".jpg", "image/jpeg"},
                    {".jpeg", "image/jpeg"},
                    {".gif", "image/gif"},
                    {".csv", "text/csv"}
                };
    }
    /// <summary>
    /// Get complete exception message
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static String GetMessageException(Exception ex)
    {
        if (ex != null)
        {
            var message = new StringBuilder();
            if (ex.InnerException != null)
            {
                message.Append(GetMessageException(ex.InnerException));
                message.Append(Environment.NewLine);
            }
            message.Append(ex.Message);
            message.Append(Environment.NewLine);
            message.Append(ex.StackTrace);
            message.Append(Environment.NewLine);
            return message.ToString();
        }
        return string.Empty;
    }

    /// <summary>
    /// Get complete exception message as items
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static List<string> GetListMessageException(this List<string> errList, Exception ex)
    {
        List<string> err = errList;
        if (ex != null)
        {
            var message = new StringBuilder();
            if (ex.InnerException != null)
                err = err.GetListMessageException(ex.InnerException);
            err.Add(ex.Message);
            return err;
        }
        return new List<string>();
    }
    /// <summary>
    /// Get string from resource name  {Namespace}.{Folder}.{resourceName}.{Extension}
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="resourceName"></param>
    /// <returns></returns>
    public static string GetEmbeddedResourceString(this Assembly assembly, string Namespace, string Folder, string resourceName)
    {
        using (Stream stream = assembly.GetManifestResourceStream($"{Namespace}.{Folder}.{resourceName}"))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
    /// <summary>
    /// Get bytes from resource name  {Namespace}.{Folder}.{resourceName}.{Extension}
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="Namespace"></param>
    /// <param name="Folder"></param>
    /// <param name="resourceName"></param>
    /// <returns></returns>
    public static byte[] GetEmbeddedResourceBinary(this Assembly assembly, string Namespace, string Folder, string resourceName)
    {
        using (Stream stream = assembly.GetManifestResourceStream($"{Namespace}.{Folder}.{resourceName}"))
        {
            using (BinaryReader br = new BinaryReader(stream))
            {
                return br.ReadBytes((int)stream.Length);
            }
        }
    }
    /// <summary>
    /// Convert XML string to JSON string
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static string XmlToJSON(this string xml, bool omitRoot = false)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        doc.RemoveChild(doc.FirstChild);
        return JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, omitRoot).Replace("@", "");
    }
    /// <summary>
    /// Convert from JSON string to XML string
    /// </summary>
    /// <param name="json"></param>
    /// <param name="rootName"></param>
    /// <returns></returns>
    public static string JSONToXml(this string json, string rootName = "Result")
    {
        XDocument xmlDoc = new XDocument(new XDeclaration("1.0", "utf-8", ""));
        XElement root = new XElement("Root");
        root.Name = rootName;
        var dataTable = JsonConvert.DeserializeObject<DataTable>(json);
        root.Add(
                 from row in dataTable.AsEnumerable()
                 select new XElement("Record",
                                     from column in dataTable.Columns.Cast<DataColumn>()
                                     select new XElement(column.ColumnName, row[column])
                                    )
               );
        xmlDoc.Add(root);
        return xmlDoc.ToString();
    }
    /// <summary>
    /// Convert from XML string to HTML tags from XSL file
    /// </summary>
    /// <param name="xmlResponse"></param>
    /// <param name="path"></param>
    /// <param name="xslFile"></param>
    /// <returns></returns>
    public static string XMLtoHTML(this string xmlResponse, string xslContent)
    {
        using (XmlReader oXmlReader = XmlReader.Create(new StringReader(xmlResponse)))
        {
            XPathDocument xmlDoc = new XPathDocument(oXmlReader);
            XslCompiledTransform xslt = new XslCompiledTransform();
            using (XmlReader reader = XmlReader.Create(new StringReader(xslContent)))
            {
                xslt.Load(reader);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                xslt.Transform(xmlDoc, null, ms);
                ms.Seek(0, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(ms))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }

    public static T JsonFileToModel<T>(string Folder, string FileName)
    {
        using StreamReader streamReader = new(Path.Combine(Folder, FileName));
        return System.Text.Json.JsonSerializer.Deserialize<T>(streamReader.ReadToEnd(), jsonOptions);
    }
    public static string JsonFileToString(string Folder, string FileName)
    {
        try
        {
            using StreamReader streamReader = new(Path.Combine(Folder, FileName));
            return streamReader.ReadToEnd();
        }
        catch (Exception)
        {
            return null;
        }
    }
    public static void JsonToFile(string Folder, string FileName, object Data)
    {
        try
        {
            var json = System.Text.Json.JsonSerializer.Serialize(Data, jsonOptions);
            File.WriteAllText(Path.Combine(Folder, FileName), json, Encoding.Unicode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static bool GetBoolValue(this bool? value) => value.HasValue ? value.Value : false;

    public static string GetString(this string? value) => value is null ? "" : value.ToString();
    public static string GetJson(this object data) => System.Text.Json.JsonSerializer.Serialize(data, jsonOptions);
}
