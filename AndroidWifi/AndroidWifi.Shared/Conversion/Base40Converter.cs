/*
Copyright 2018 Ellisnet - Jeremy Ellis

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable LocalizableElement

namespace AndroidWifi.Conversion
{
    public class Base40Converter : IDisposable
    {
        #region Private conversion methods

        private string ConvertHexToBase40(string hexString)
        {
            string result = "";
            string tempResult = "";

            string dotOrNot = "";

            int chunkSize = 16;

            hexString = hexString.Trim().ToUpper();

            if (hexString.Length > 0)
            {
                while (hexString.Length > chunkSize)
                {
                    var currentChunk = hexString.Substring(0, chunkSize);
                    hexString = hexString.Substring(chunkSize, hexString.Length - chunkSize);
                    tempResult += dotOrNot + ConvertHexChunkToBase40(currentChunk);
                    dotOrNot = ".";
                }

                if (hexString.Length > 0)
                {
                    tempResult += dotOrNot + ConvertHexChunkToBase40(hexString);
                }
                result = tempResult;
            }

            return result;
        }

        private string ConvertBase40ToHex(string base40String)
        {
            string result = "";
            string tempResult = "";

            char[] separators = { '.' };

            base40String = base40String.Trim().ToUpper();

            if (base40String.Length > 0)
            {
                string[] chunks = base40String.Split(separators);

                foreach (string currentChunk in chunks)
                {
                    tempResult += ConvertBase40ChunkToHex(currentChunk);
                }
                result = tempResult;
            }

            return result;
        }

        private string ConvertBase40ChunkToHex(string base40Chunk)
        {
            string result = "";

            ulong tempResult = 0;
            char[] separators = { '.' };
            ulong multiplier = 1;
            int hexLength = 16;

            bool isInvalidChar = false;

            base40Chunk = base40Chunk.Trim().ToUpper().Replace(":", "");

            if (base40Chunk.Length > 0)
            {
                if (base40Chunk.Contains("$"))
                {
                    // ReSharper disable once StringIndexOfIsCultureSpecific.1
                    int intColonPos = base40Chunk.IndexOf("$");
                    var lengthChar = base40Chunk.Substring(0, intColonPos);
                    base40Chunk = base40Chunk.Substring(intColonPos + 1, base40Chunk.Length - (intColonPos + 1));
                    char[] charLengthChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
                    for (int i = 0; i < charLengthChars.Length; i++)
                    {
                        if (lengthChar == charLengthChars[i].ToString())
                        {
                            hexLength = i;
                            break;
                        }
                    }
                }

                base40Chunk = Base40Chars(base40Chunk, false);

                var base40Chunks = base40Chunk.Split(separators);
                if (base40Chunks.Length > 1)
                {
                    for (int i = 1; i <= (base40Chunks.Length - 1); i++)
                    {
                        multiplier = multiplier * 40;
                    }

                    for (int intIndex = 0; intIndex <= (base40Chunks.Length - 1); intIndex++)
                    {
                        try
                        {
                            short currentChunk = Convert.ToInt16(base40Chunks[intIndex], 10);
                            tempResult += (ulong)currentChunk * multiplier;
                            multiplier = multiplier / 40;
                        }
                        catch
                        {
                            // ReSharper disable once RedundantAssignment
                            isInvalidChar = true;
                            throw new ArgumentException("An invalid Base 40 string was provided to be converted to Hexadecimal.", nameof(base40Chunk));
                            //return result;
                        }
                        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                        if (isInvalidChar) {break;}
                    }

                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (!isInvalidChar)
                    {
                        result = tempResult.ToString("X");
                        result = result.Trim();

                        while (result.Length < hexLength)
                        {
                            result = "0" + result;
                        }
                    }
                }
            }

            return result;
        }

        private string ConvertHexChunkToBase40(string hexChunk)
        {
            string result = "";
            string tempResult = "";
            string dotOrNot = "";
            string lengthInfo = "";
            int baseValue = 40;

            ulong divisor = 1;

            bool isInvalidHex = false;

            hexChunk = hexChunk.Trim().ToUpper();

            if ((hexChunk.Length > 0) && (hexChunk.Length < 17))
            {
                ulong remainder;
                try
                {
                    remainder = Convert.ToUInt64(hexChunk, 16);
                }
                catch
                {
                    // ReSharper disable once RedundantAssignment
                    isInvalidHex = true;
                    throw new ArgumentException("An invalid Hexadecimal string was provided to be converted to Base 40.", nameof(hexChunk));
                }

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (!isInvalidHex)
                {
                    if (hexChunk.Length < 16)
                    {
                        char[] charLengthChars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
                        lengthInfo = charLengthChars[hexChunk.Length] + "$";
                    }

                    for (int i = 1; i <= 12; i++)
                    {
                        divisor = divisor * (ulong)baseValue;
                    }

                    for (int intPower = 12; intPower > 0; intPower--)
                    {
                        var quotient = (int)(remainder / divisor);
                        remainder = remainder % divisor;
                        if ((quotient > 0) || (dotOrNot != ""))
                        {
                            tempResult += dotOrNot + quotient;
                            dotOrNot = ".";
                        }
                        divisor = divisor / (ulong)baseValue;
                    }
                    tempResult += dotOrNot + remainder;

                    tempResult = Base40Chars(tempResult, true);

                    result = lengthInfo + tempResult;
                }
            }
            return result;
        }

        private string Base40Chars(string convertText, bool toBase40Char)
        {
            string result = "";
            string tempResult = "";

            string dotOrNot = "";

            bool isInvalidChar = false;

            char[] separators = { '.' };

            char[] base40Chars = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E',
                                         'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                                         'U', 'V', 'W', 'X', 'Y', 'Z', '%', '*', '+', '-' };

            convertText = convertText.Trim();

            if (convertText.Length > 0)
            {
                short currentCharValue;
                if (toBase40Char)
                {
                    foreach (string strCurrentChar in convertText.Split(separators))
                    {
                        try
                        {
                            currentCharValue = Convert.ToInt16(strCurrentChar, 10);
                        }
                        catch
                        {
                            // ReSharper disable once RedundantAssignment
                            isInvalidChar = true;
                            throw new ArgumentException("An invalid character was found in the string to be converted to/from Base 40.", nameof(convertText));
                        }

                        // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                        if (isInvalidChar) {break;}

                        tempResult += base40Chars[currentCharValue].ToString();
                    }
                }
                else
                {
                    convertText = convertText.ToUpper();

                    for (int intIndex = 0; intIndex < convertText.Length; intIndex++)
                    {
                        var currentChar = Convert.ToChar(convertText.Substring(intIndex, 1));
                        currentCharValue = -1;
                        for (short i = 0; i < base40Chars.Length; i++)
                        {
                            if (base40Chars[i] == currentChar)
                            {
                                currentCharValue = i;
                                break;
                            }
                        }
                        if (currentCharValue > -1)
                        {
                            tempResult += dotOrNot + currentCharValue;
                            dotOrNot = ".";
                        }
                    }
                }
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (!isInvalidChar) {result = tempResult;}
            }

            return result;
        }

        #endregion

        public string ToBase40String(IList<byte> bytesToConvert)
        {
            if (bytesToConvert == null) { throw new ArgumentNullException(nameof(bytesToConvert));}
            if (bytesToConvert.Count == 0) { throw new ArgumentOutOfRangeException(nameof(bytesToConvert));}

            string hexString = String.Join("", bytesToConvert.Select(s => s.ToString("X2").ToUpper()));
            return ConvertHexToBase40(hexString);
        }

        public string ToBase40String(string textToConvert)
        {
            if (textToConvert == null) { throw new ArgumentNullException(nameof(textToConvert)); }
            if (textToConvert.Length == 0) { throw new ArgumentOutOfRangeException(nameof(textToConvert)); }

            return ToBase40String(Encoding.UTF8.GetBytes(textToConvert));
        }

        public IList<byte> BytesFromBase40(string base40Text)
        {
            if (base40Text == null) { throw new ArgumentNullException(nameof(base40Text)); }
            if (base40Text.Length == 0) { throw new ArgumentOutOfRangeException(nameof(base40Text)); }

            string hexString = ConvertBase40ToHex(base40Text);
            if ((hexString.Length % 2) != 0) { throw new InvalidOperationException("An invalid number of characters was encountered.");}

            var result = new List<byte>();
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string currentByte = hexString.Substring(i, 2);
                result.Add(Convert.ToByte(currentByte, 16));
            }

            return result;
        }

        public string StringFromBase40(string base40Text)
        {
            if (base40Text == null) { throw new ArgumentNullException(nameof(base40Text)); }
            if (base40Text.Length == 0) { throw new ArgumentOutOfRangeException(nameof(base40Text)); }

            byte[] bytes = BytesFromBase40(base40Text).ToArray();

            return Encoding.UTF8.GetString(bytes);
        }

        public void Dispose()
        {
            //Nothing to do here
        }
    }
}
