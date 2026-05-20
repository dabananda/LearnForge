using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LearnForge.Server.Api.Utils
{
    public class Security
    {

        public static string GetRedisKey(object payload)
        {
            // If the caller gave us raw JSON, keep it.
            string json = payload is string s
                ? s
                : JsonSerializer.Serialize(
                      payload,
                      new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });

            return ComputeHash(json);
        }

        private static string ComputeHash(string json)
        {
            using var sha = SHA256.Create();  // or SHA256.HashData in .NET 8+
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
            return Convert.ToBase64String(bytes);
        }
        public static string SHA256Converter(string message)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
        public static async Task<string> CustomBase64Encode(string inData)
        {
            const string Base64 = "CHAEGDFXLJKMZPINSOQRUVTWYBeadmvfghbicjlnokpsqyrtwzux0971364285+/";

            StringBuilder sOut = new StringBuilder();
            int i;

            // For each group of 3 bytes
            for (i = 0; i < inData.Length; i += 3)
            {
                int nGroup, pOut;

                // Create one long from this 3 bytes.
                nGroup = (i < inData.Length ? inData[i] : 0) << 16;
                nGroup += (i + 1 < inData.Length ? inData[i + 1] : 0) << 8;
                nGroup += i + 2 < inData.Length ? inData[i + 2] : 0;

                // Convert to base64
                pOut = Base64[(nGroup >> 18) & 63];
                sOut.Append((char)pOut);
                pOut = Base64[(nGroup >> 12) & 63];
                sOut.Append((char)pOut);
                pOut = Base64[(nGroup >> 6) & 63];
                sOut.Append((char)pOut);
                pOut = Base64[nGroup & 63];
                sOut.Append((char)pOut);
            }

            switch (inData.Length % 3)
            {
                case 1: // 8 bit final
                    sOut.Remove(sOut.Length - 2, 2).Append("==");
                    break;
                case 2: // 16 bit final
                    sOut.Remove(sOut.Length - 1, 1).Append("=");
                    break;
            }

            return sOut.ToString();
        }

        public static async Task<string> CustomBase64Decode(string base64String)
        {
            const string Base64 = "CHAEGDFXLJKMZPINSOQRUVTWYBeadmvfghbicjlnokpsqyrtwzux0971364285+/";
            base64String = base64String.Trim();
            int dataLength = base64String.Length;
            string sOut = "";
            int groupBegin = 0;

            // Remove white spaces, if any
            base64String = base64String.Replace("\r\n", "").Replace("\t", "").Replace(" ", "");

            // The source must consist of groups with a length of 4 chars
            if (dataLength % 4 != 0)
            {
                throw new Exception("Invalid Request for Security!");
            }

            // Now decode each group:
            for (groupBegin = 1; groupBegin <= dataLength; groupBegin += 4)
            {
                int numDataBytes = 3;
                int thisData;
                int nGroup = 0;
                string pOut = "";

                // Each data group encodes up to 3 actual bytes.
                for (int charCounter = 0; charCounter <= 3; charCounter++)
                {
                    // Convert each character into 6 bits of data and add it to an integer for temporary storage.
                    // If a character is '=', there is one fewer data byte. (There can only be a maximum of 2 '=' in the whole string.)
                    char thisChar = base64String[groupBegin + charCounter - 1];
                    if (thisChar == '=')
                    {
                        numDataBytes--;
                        thisData = 0;
                    }
                    else
                    {
                        thisData = Base64.IndexOf(thisChar);
                    }

                    if (thisData == -1)
                    {
                        throw new Exception("Bad character in Base64 string.");
                    }

                    nGroup = 64 * nGroup + thisData;
                }

                // Hex splits the long to 6 groups with 4 bits
                string hexValue = nGroup.ToString("X");

                // Add leading zeros
                hexValue = new string('0', 6 - hexValue.Length) + hexValue;

                // Convert the 3 byte hex integer (6 chars) to 3 characters
                pOut = ((char)Convert.ToByte(hexValue.Substring(0, 2), 16)).ToString() +
                       ((char)Convert.ToByte(hexValue.Substring(2, 2), 16)).ToString() +
                       ((char)Convert.ToByte(hexValue.Substring(4, 2), 16)).ToString();

                // Add numDataBytes characters to out string
                sOut += pOut.Substring(0, numDataBytes);
            }

            return sOut;
        }

        public static string FormatDate(string inputDate)
        {
            DateTime date = DateTime.Parse(inputDate);
            return date.ToString("dd MMMM yyyy");
        }


        public static string FormatTime(string inputTime)
        {
            DateTime time = DateTime.Parse(inputTime);
            return time.ToString("h:mm tt");
        }
        public static string FormatDateTime(string inputTime)
        {
            DateTime time = DateTime.Parse(inputTime);
            return time.ToString("d/M/yyyy  h:mm tt");
        }

        //public static async Task<bool> CheckValidationErrors<T>(T request, AbstractValidator<T> validationRules, List<Error> events)
        //{
        //    var hasValidationError = false;

        //    var validationResults = validationRules.Validate(request);
        //    if (!validationResults.IsValid)
        //    {
        //        hasValidationError = true;
        //        HandleValidationErrors(validationResults, events); // Add Validation Error to event
        //    }

        //    return hasValidationError;
        //}


        //public static void HandleValidationErrors(FluentValidation.Results.ValidationResult validationResult, List<Error> events)
        //{
        //    try
        //    {
        //        ErrorDictionary errorDictionary = new ErrorDictionary();

        //        events.AddRange(validationResult.Errors.Select(error =>
        //            new Error
        //            {
        //                ErrorCode = int.Parse(error.ErrorCode),
        //                Name = error.PropertyName,
        //                InvalidValue = error.AttemptedValue,
        //                Message = errorDictionary.GetEnumDescriptionPublic((ErrorCode)int.Parse(error.ErrorCode))
        //            }
        //        ));
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}


        private static int GetDateCode(DateTime? dttmpDate)
        {
            if (DateTime.TryParse(dttmpDate.ToString(), out DateTime parsedDate))
            {
                return parsedDate.Day + parsedDate.Month + parsedDate.Year;
            }
            else
            {
                return 7 + 12 + 1973;
            }
        }

        public static string EncodeID(int enComID, string enAppId, DateTime? endtDate)
        {
            int enTodaysCode = GetDateCode(DateTime.Now); // Sum all date parts of today's date
            int enGivenCode = GetDateCode(endtDate); // Sum all date parts of the given date (birth date)
            int enTotal = enComID + enTodaysCode + enGivenCode; // Sum all parts with Company id
            int enLnFinalId = int.Parse(enAppId) ^ enTotal; // XOR with applicant id
            string enstrFinalId = enLnFinalId.ToString() + enGivenCode.ToString("D4"); // Attach 4-digit 'enGivenCode' at the end of the ID value
            int enMatchingCode = enLnFinalId / 2 ^ enTodaysCode; // This code is for matching

            enstrFinalId = enstrFinalId + "ID" + enMatchingCode;
            return enstrFinalId;
        }

        public static int DecodeID(int dcComID, string dcAppId)
        {
            int dcTodaysCode = GetDateCode(DateTime.Now); // Sum all date parts of today's date
            string[] dcVPart = dcAppId.Split("ID");
            if (dcVPart.Length != 2)
            {
                throw new ArgumentException("Invalid input format");
            }

            string dcEncodedID = dcVPart[0].Substring(0, dcVPart[0].Length - 4);
            int dcMatchingCode = int.Parse(dcEncodedID) / 2 ^ dcTodaysCode;
            if (int.TryParse(dcVPart[1], out int parsedMatchingCode) && dcMatchingCode != parsedMatchingCode)
            {
                throw new ArgumentException("Unmatched codes");
            }

            string dcAppDateSum = dcVPart[0].Substring(dcVPart[0].Length - 4);
            int dcTotal = dcComID + dcTodaysCode + int.Parse(dcAppDateSum); // Sum all parts with Company id
            int dcLnFinalId = int.Parse(dcEncodedID) ^ dcTotal; // XOR with applicant id

            return dcLnFinalId;
        }
        public static string EncryptEncrpAppl(string input, int isUrlEncoded, string logicNumber)
        {
            string abLocal = input;

            if (isUrlEncoded == 1)
            {
                abLocal = UrlDecodeForEncryptDecrypt(abLocal);
            }

            StringBuilder abOdd = new StringBuilder();
            StringBuilder abEven = new StringBuilder();

            for (int i = 1; i <= abLocal.Length; i++)
            {
                if (i % 2 != 0)
                {
                    abOdd.Append(abLocal[i - 1]);
                }
                else
                {
                    abEven.Append(abLocal[i - 1]);
                }
            }

            string newQh = abOdd.ToString() + abEven.ToString();
            Random rnd = new Random();
            int rndForOddEven = rnd.Next(0, 10);

            StringBuilder myZ = new StringBuilder();

            for (int j = 0; j < newQh.Length; j++)
            {
                if (rndForOddEven % 2 != 0) // odd
                {
                    //myZ.Append(newQh[j] + ((char)(ushort.MaxValue - rnd.Next())).ToString());
                    myZ.Append(newQh[j] + ((char)(90 + rnd.Next(0, 10))).ToString());
                }
                else
                {
                    //myZ.Append(((char)(ushort.MaxValue - rnd.Next())).ToString() + newQh[j]);
                    myZ.Append(((char)(90 + rnd.Next(0, 10))).ToString() + newQh[j]);
                }
            }

            //return new string(myZ.ToString().Reverse().ToArray()) + rndForOddEven.ToString();
            return new string(myZ.ToString().Reverse().ToArray()) + rndForOddEven.ToString();
        }

        public static string UrlDecodeForEncryptDecrypt(string QStr1_udfed)
        {
            string[,] nArr_udfed = new string[,]
            {
                {"!", "%21"},
                {"\"", "%22"},
                {"#", "%23"},
                {"$", "%24"},
                {"%", "%25"},
                {"&", "%26"},
                {"'", "%27"},
                {"(", "%28"},
                {")", "%29"},
                {"*", "%2A"},
                {"+", "%2B"},
                {",", "%2C"},
                {"-", "%2D"},
                {".", "%2E"},
                {"/", "%2F"},
                {"0", "%30"},
                {"1", "%31"},
                {"2", "%32"},
                {"3", "%33"},
                {"4", "%34"},
                {"5", "%35"},
                {"6", "%36"},
                {"7", "%37"},
                {"8", "%38"},
                {"9", "%39"},
                {":", "%3A"},
                {";", "%3B"},
                {"<", "%3C"},
                {"=", "%3D"},
                {">", "%3E"},
                {"?", "%3F"},
                {"@", "%40"},
                {"A", "%41"},
                {"B", "%42"},
                {"C", "%43"},
                {"D", "%44"},
                {"E", "%45"},
                {"F", "%46"},
                {"G", "%47"},
                {"H", "%48"},
                {"I", "%49"},
                {"J", "%4A"},
                {"K", "%4B"},
                {"L", "%4C"},
                {"M", "%4D"},
                {"N", "%4E"},
                {"O", "%4F"},
                {"P", "%50"},
                {"Q", "%51"},
                {"R", "%52"},
                {"S", "%53"},
                {"T", "%54"},
                {"U", "%55"},
                {"V", "%56"},
                {"W", "%57"},
                {"X", "%58"},
                {"Y", "%59"},
                {"Z", "%5A"},
                {"[", "%5B"},
                {"\\", "%5C"},
                {"]", "%5D"},
                {"^", "%5E"},
                {"_", "%5F"},
                {"`", "%60"},
                {"a", "%61"},
                {"b", "%62"},
                {"c", "%63"},
                {"d", "%64"},
                {"e", "%65"},
                {"f", "%66"},
                {"g", "%67"},
                {"h", "%68"},
                {"i", "%69"},
                {"j", "%6A"},
                {"k", "%6B"},
                {"l", "%6C"},
                {"m", "%6D"},
                {"n", "%6E"},
                {"o", "%6F"},
                {"p", "%70"},
                {"q", "%71"},
                {"r", "%72"},
                {"s", "%73"},
                {"t", "%74"},
                {"u", "%75"},
                {"v", "%76"},
                {"w", "%77"},
                {"x", "%78"},
                {"y", "%79"},
                {"z", "%7A"},
                {"{", "%7B"},
                {"|", "%7C"},
                {"}", "%7D"},
                {"~", "%7E"},
                {"", "%7F"},
                {"", "%80"},
                {"", "%81"},
                {"", "%82"},
                {"", "%83"},
                {"", "%84"},
                {"", "%85"},
                {"", "%86"},
                {"", "%87"},
                {"", "%88"},
                {"", "%89"},
                {"", "%8A"},
                {"", "%8B"},
                {"", "%8C"},
                {"", "%8D"},
                {"", "%8E"},
                {"", "%8F"},
                {"", "%90"},
                {"", "%91"},
                {"", "%92"},
                {"", "%93"},
                {"", "%94"},
                {"", "%95"},
                {"", "%96"},
                {"", "%97"},
                {"", "%98"},
                {"", "%99"},
                {"", "%9A"},
                {"", "%9B"},
                {"", "%9C"},
                {"", "%9D"},
                {"", "%9E"},
                {"", "%9F"},
                {"", "%A0"},
                {"", "%A1"},
                {"", "%A2"},
                {"", "%A3"},
                {"", "%A4"},
                {"", "%A5"},
                {"|", "%A6"},
                {"", "%A7"},
                {"", "%A8"},
                {"", "%A9"},
                {"", "%AA"},
                {"", "%AB"},
                {"", "%AC"},
                {"", "%AD"},
                {"", "%AE"},
                {"", "%AF"},
                {"", "%B0"},
                {"", "%B1"},
                {"", "%B2"},
                {"", "%B3"},
                {"", "%B4"},
                {"", "%B5"},
                {"", "%B6"},
                {"", "%B7"},
                {"", "%B8"},
                {"", "%B9"},
                {"", "%BA"},
                {"", "%BB"},
                {"", "%BC"},
                {"", "%BD"},
                {"", "%BE"},
                {"", "%BF"},
                {"", "%C0"},
                {"", "%C1"},
                {"", "%C2"},
                {"", "%C3"},
                {"", "%C4"},
                {"", "%C5"},
                {"", "%C6"},
                {"", "%C7"},
                {"", "%C8"},
                {"", "%C9"},
                {"", "%CA"},
                {"", "%CB"},
                {"", "%CC"},
                {"", "%CD"},
                {"", "%CE"},
                {"", "%CF"},
                {"", "%D0"},
                {"", "%D1"},
                {"", "%D2"},
                {"", "%D3"},
                {"", "%D4"},
                {"", "%D5"},
                {"", "%D6"},
                {"", "%D7"},
                {"", "%D8"},
                {"", "%D9"},
                {"", "%DA"},
                {"", "%DB"},
                {"", "%DC"},
                {"", "%DD"},
                {"", "%DE"},
                {"", "%DF"},
                {"", "%E0"},
                {"", "%E1"},
                {"", "%E2"},
                {"", "%E3"},
                {"", "%E4"},
                {"", "%E5"},
                {"", "%E6"},
                {"", "%E7"},
                {"", "%E8"},
                {"", "%E9"},
                {"", "%EA"},
                {"", "%EB"},
                {"", "%EC"},
                {"", "%ED"},
                {"", "%EE"},
                {"", "%EF"},
                {"", "%F0"},
                {"", "%F1"},
                {"", "%F2"},
                {"", "%F3"},
                {"", "%F4"},
                {"", "%F5"},
                {"", "%F6"},
                {"", "%F7"},
                {"", "%F8"},
                {"", "%F9"},
                {"", "%FA"},
                {"", "%FB"},
                {"", "%FC"},
                {"", "%FD"},
                {"", "%FE"},
                {"", "%FF"}
            };

            string result = QStr1_udfed;

            for (int i = 0; i < nArr_udfed.GetLength(0); i++)
            {
                result = result.Replace(nArr_udfed[i, 1], nArr_udfed[i, 0]);
            }

            return result;
        }

        public static bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }

        public static string GetConvertedDateTime(string strExamDate)
        {
            // Parse the input date string into a DateTime object
            DateTime examDate = DateTime.Parse(strExamDate);

            // Extract day, month, and year
            int intExamDay = examDate.Day;
            int intExamMonth = examDate.Month;
            int intExamYear = examDate.Year;

            // Format day and month to always have two digits
            string formattedDay = intExamDay.ToString("D2");
            string formattedMonth = intExamMonth.ToString("D2");

            // Extract and format the time component
            string strExamTime = examDate.ToString("HH:mm:ss");

            // Format the final date string as MM/DD/YYYY HH:mm:ss
            string formattedDate = $"{formattedMonth}/{formattedDay}/{intExamYear} {strExamTime}";

            return formattedDate;
        }

        public static int GetPercentage(int mainVal, int totalVal)
        {
            if (mainVal.ToString() == null || mainVal.ToString() == "")
            {
                mainVal = 0;
            }

            int mainValue;
            if (!int.TryParse(mainVal.ToString(), out mainValue))
            {
                mainValue = 0;
            }

            if (totalVal > 0)
            {
                return (int)((long)mainValue * 100 / totalVal);
            }
            else
            {
                return 0;
            }
        }


        public static string Base64Encode(string inData)
        {
            const string Base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            StringBuilder sOut = new StringBuilder();

            for (int i = 0; i < inData.Length; i += 3)
            {
                int nGroup;
                if (i + 2 < inData.Length)
                {
                    nGroup = (inData[i] << 16) + (inData[i + 1] << 8) + inData[i + 2];
                }
                else if (i + 1 < inData.Length)
                {
                    nGroup = (inData[i] << 16) + (inData[i + 1] << 8);
                }
                else
                {
                    nGroup = (inData[i] << 16);
                }

                sOut.Append(Base64[(nGroup >> 18) & 63]);
                sOut.Append(Base64[(nGroup >> 12) & 63]);
                sOut.Append((i + 1) < inData.Length ? Base64[(nGroup >> 6) & 63] : '=');
                sOut.Append((i + 2) < inData.Length ? Base64[nGroup & 63] : '=');
            }

            return sOut.ToString();
        }


    }
}
