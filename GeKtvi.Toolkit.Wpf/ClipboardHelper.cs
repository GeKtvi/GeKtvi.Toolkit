using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace GeKtvi.Toolkit.Wpf
{
    public static class ClipboardHelper
    {
        public delegate string[] ParseFormat(string value);

        public static bool IsInClipboardCSV => Clipboard.GetDataObject().GetData(DataFormats.CommaSeparatedValue) != null;

        public static List<string[]> ParseClipboardData()
        {
            try
            {
                return ParseClipboardData(Clipboard.GetDataObject());
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                if (e.HResult != -2147221040)
                    throw;
            }
            return new();
        }

        public static List<string[]> ParseClipboardData(IDataObject dataObject)
        {
            List<string[]> clipboardData = null;
            object clipboardRawData = null;
            ParseFormat parseFormat = null;

            // get the data and set the parsing method based on the format
            // currently works with CSV and Text DataFormats            

            if (dataObject.GetData(DataFormats.CommaSeparatedValue) != null)
            {
                clipboardRawData = Clipboard.GetText(TextDataFormat.UnicodeText);
                parseFormat = ParseCsvFormat;
            }
            else if ((clipboardRawData = dataObject.GetData(DataFormats.UnicodeText)) != null)
            {
                parseFormat = ParseTextFormat;
            }

            if (parseFormat != null)
            {
                string rawDataStr = clipboardRawData as string;

                if (rawDataStr == null && clipboardRawData is MemoryStream)
                {
                    // cannot convert to a string so try a MemoryStream
                    MemoryStream ms = clipboardRawData as MemoryStream;
                    StreamReader sr = new StreamReader(ms);
                    rawDataStr = sr.ReadToEnd();
                }
                Debug.Assert(rawDataStr != null, string.Format("clipboardRawData: {0}, could not be converted to a string or memorystream.", clipboardRawData));

                string[] rows = rawDataStr.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (rows != null && rows.Length > 0)
                {
                    clipboardData = new List<string[]>();
                    foreach (string row in rows)
                    {
                        clipboardData.Add(parseFormat(row));
                    }
                }
                else
                {
                    Debug.WriteLine("unable to parse row data.  possibly null or contains zero rows.");
                    return new List<string[]> { new string[] { string.Empty } };
                }
            }

            return clipboardData;
        }

        public static void SetClipboardData(List<List<string>> clipboardData)
        {
            DataObject dataObj = ToDataObject(clipboardData);

            Clipboard.SetDataObject(dataObj);
        }

        public static DataObject ToDataObject(List<List<string>> clipboardData)
        {
            string textToCB = string.Empty;
            StringBuilder sb = ToRTF(clipboardData);

            for (int i = 0; i < clipboardData.Count; i++)
            {
                for (int y = 0; y < clipboardData[i].Count; y++)
                {
                    if (y != clipboardData[i].Count - 1)
                        textToCB += clipboardData[i][y] + '\t';
                    else
                        textToCB += clipboardData[i][y];
                }
                if (i != clipboardData.Count - 1)
                    textToCB += "\r\n";
            }

            DataObject dataObj = new DataObject();
            dataObj.SetData(DataFormats.Rtf, sb);
            dataObj.SetData(DataFormats.Text, textToCB);
            return dataObj;
        }

        private static StringBuilder ToRTF(List<List<string>> clipboardData)
        {
            StringBuilder sb = new StringBuilder();

            if (clipboardData.Count == 0)
                return sb;

            int maxRowLen = clipboardData.Max(x => x.Count);

            int[] maxLenInColumn = new int[maxRowLen];

            for (int i = 0; i < maxLenInColumn.Length; i++)
                maxLenInColumn[i] = 300; //default value 


            sb.Append(@"{\rtf1 ");

            sb.Append(@"\trowd"); //Prepare the header Row

            for (int i = 0; i < maxRowLen; i++) //find max cell len
            {
                foreach (List<string> row in clipboardData)
                {
                    if (i > row.Count)
                        break;
                    int length = row[i].Length * 95;
                    if (length > maxLenInColumn[i])
                        maxLenInColumn[i] = length;
                }
            }

            int curLength = 0;
            foreach (int len in maxLenInColumn) //set len width
            {
                curLength += len;
                sb.Append(@"\cellx" + curLength); //+ curLength
            }

            foreach (List<string> row in clipboardData) //fill columns
            {
                sb.Append(@"\intbl " + "\\fs18"); //Start the row

                for (int i = 0; i < maxRowLen; i++)
                {
                    if (i < row.Count)
                        sb.Append(" " + row[i] + @"\cell"); //cell
                    else
                        sb.Append(" " + @"\cell"); //empty cell
                }

                sb.Append(@"\row"); // end row
            }

            sb.Append(@"\pard");
            sb.Append(@"}"); // end
            return sb;
        }

        public static string[] ParseCsvFormat(string value)
        {
            return ParseCsvOrTextFormat(value, true);
        }

        public static string[] ParseTextFormat(string value)
        {
            return ParseCsvOrTextFormat(value, false);
        }

        private static string[] ParseCsvOrTextFormat(string value, bool isCSV)
        {
            List<string> outputList = new List<string>();

            char separator = isCSV ? '\t' : '\t';
            int startIndex = 0;
            int endIndex = 0;

            for (int i = 0; i < value.Length; i++)
            {
                char ch = value[i];
                if (ch == separator)
                {
                    outputList.Add(value[startIndex..endIndex]);

                    startIndex = endIndex + 1;
                    endIndex = startIndex;
                }
                else if (ch == '\"' && isCSV)
                {
                    // skip until the ending quotes
                    i++;
                    if (i >= value.Length)
                    {
                        throw new FormatException(string.Format("value: {0} had a format exception", value));
                    }
                    char tempCh = value[i];
                    while (tempCh != '\"' && i < value.Length)
                        i++;

                    endIndex = i;
                }
                else if (i + 1 == value.Length)
                {
                    // add the last value
                    outputList.Add(value[startIndex..]);
                    break;
                }
                else
                {
                    endIndex++;
                }
            }

            return outputList.ToArray();
        }
    }
}
