using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeKtviWpfToolkit
{
    public static class ClipboardHelper
    {
        public delegate string[] ParseFormat(string value);

        public static bool IsInClipboardCSV => Clipboard.GetDataObject().GetData(DataFormats.CommaSeparatedValue) != null;

        public static List<string[]> ParseClipboardData()
        {
            List<string[]> clipboardData = null;
            object clipboardRawData = null;
            ParseFormat parseFormat = null;

            IDataObject dataObjtst = Clipboard.GetDataObject();
            var rert = dataObjtst.GetData(DataFormats.Rtf);
            // get the data and set the parsing method based on the format
            // currently works with CSV and Text DataFormats            
            IDataObject dataObj = Clipboard.GetDataObject();
            if (dataObj.GetData(DataFormats.CommaSeparatedValue) != null)
            {
                clipboardRawData = Clipboard.GetText(TextDataFormat.Text);
                parseFormat = ParseCsvFormat;
            }
            else if ((clipboardRawData = dataObj.GetData(DataFormats.Text)) != null)
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

                string[] rows = rawDataStr.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
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
            string textToCB = string.Empty;
            StringBuilder sb = ToRTF(clipboardData);

            foreach (var row in clipboardData)
            {
                foreach (var cell in row)
                {
                    textToCB += cell + '\t';
                }
                textToCB += "\r\n";
            }

            ///////////////////////////////////////////

            //sb.Append(@"{\rtf1 ");

            ////Prepare the header Row
            //sb.Append(@"\trowd");

            ////A cell with width 1000.
            //sb.Append(@"\cellx1000");

            ////sb.Append(@"\intbl   ID");

            ////Another cell with width 1000.Endpoint at 2000(which is 1000+1000).
            //sb.Append(@"\cellx2000");

            ////sb.Append(@"\cell    Name");

            ////Another cell with width 1000.Ending at 3000 (which is 2000+1000)
            //sb.Append(@"\cellx3000");

            ////sb.AppendFormat(@"\cell    City");

            ////Another cell with width 1000.End at 4000 (which is 3000+1000)
            //sb.Append(@"\cellx4000");

            //sb.Append(@"\cell    Country");

            //Add the created row
            //sb.Append(@"\intbl \cell \row");

            //Add 3 data Rows.Give proper padding space between data.Notice the gap after cell.
            //sb.Append(@"\intbl 1" + @"\cell Raj" +     @"\cell Bangalore" +     @"\cell India" + @"\cell"+ @"\row");
            //sb.Append(@"\intbl 2" + @"\cell Peter" +   @"\cell Mumbai" +        @"\cell India" + @"\cell"+ @"\row");
            //sb.Append(@"\intbl 3" + @"\cell Chris" +   @"\cell Delhi" +         @"\cell India" + @"\cell"+ @"\row");

            //sb.Append(@"\pard");

            //            sb.Append(@"}");
            //            sb.Clear();
            //            //new DataObject(DataFormats.Rtf, sb)

            //            //string rtfString = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{\f0\fnil Microsoft Sans Serif;}}" + "\n" + @"{\colortbl";

            //            sb.Append(
            //            "{\\rtf1\\ansi \\ansicpg1252\r\n{\\fonttbl{\\f0\\fnil Calibri;}{\\f1\\fnil Calibri;}{\\f2\\fnil Calibri;}{\\f3\\fnil Calibri;}{\\f4\\fnil Calibri;}{\\f5\\fnil Calibri;}{\\f6\\fnil Calibri Light;}{\\f7\\fnil Calibri;}{\\f8\\fnil Calibri;}{\\f9\\fnil Calibri;}{\\f10\\fnil Calibri;}{\\f11\\fnil Calibri;}{\\f12\\fnil Calibri;}{\\f13\\fnil Calibri;}{\\f14\\fnil Calibri;}{\\f15\\fnil Calibri;}{\\f16\\fnil Calibri;}{\\f17\\fnil Calibri;}{\\f18\\fnil Calibri;}{\\f19\\fnil Calibri;}{\\f20\\fnil Calibri;}{\\f21\\fnil Calibri;}{\\f22\\fnil Segoe UI;}{\\f23\\fnil Calibri;}{\\f24\\fnil Calibri;}{\\f25\\fnil Arial;}{\\f26\\fnil Arial;}{\\f27\\fnil Arial;}}\r\n{\\info{\\id220}}\\plain {\\colortbl\\red0\\green0\\blue0;\\red255\\green255\\blue255;\\red255\\green0\\blue0;\\red0\\green255\\blue0;\\red0\\green0\\blue255;\\red255\\green255\\blue0;\\red255\\green0\\blue255;\\red0\\green255\\blue255;\\red0\\green0\\blue0;\\red255\\green255\\blue255;\\red255\\green0\\blue0;\\red0\\green255\\blue0;\\red0\\green0\\blue255;\\red255\\green255\\blue0;\\red255\\green0\\blue255;\\red0\\green255\\blue255;\\red128\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue128;\\red128\\green128\\blue0;\\red128\\green0\\blue128;\\red0\\green128\\blue128;\\red192\\green192\\blue192;\\red128\\green128\\blue128;\\red153\\green153\\blue255;\\red153\\green51\\blue102;\\red255\\green255\\blue204;\\red204\\green255\\blue255;\\red102\\green0\\blue102;\\red255\\green128\\blue128;\\red0\\green102\\blue204;\\red204\\green204\\blue255;\\red0\\green0\\blue128;\\red255\\green0\\blue255;\\red255\\green255\\blue0;\\red0\\green255\\blue255;\\red128\\green0\\blue128;\\red128\\green0\\blue0;\\red0\\green128\\blue128;\\red0\\green0\\blue255;\\red0\\green204\\blue255;\\red204\\green255\\blue255;\\red204\\green255\\blue204;\\red255\\green255\\blue153;\\red153\\green204\\blue255;\\red255\\green153\\blue204;\\red204\\green153\\blue255;\\red255\\green204\\blue153;\\red51\\green102\\blue255;\\red51\\green204\\blue204;\\red153\\green204\\blue0;\\red255\\green204\\blue0;\\red255\\green153\\blue0;\\red255\\green102\\blue0;\\red102\\green102\\blue153;\\red150\\green150\\blue150;\\red0\\green51\\blue102;\\red51\\green153\\blue102;\\red0\\green51\\blue0;\\red51\\green51\\blue0;\\red153\\green51\\blue0;\\red153\\green51\\blue102;\\red51\\green51\\blue153;\\red51\\green51\\blue51;;\\red255\\green255\\blue255;\\red100\\green100\\blue100;\\red240\\green240\\blue240;\\red0\\green0\\blue0;\\red255\\green255\\blue255;\\red160\\green160\\blue160;\\red0\\green120\\blue215;\\red0\\green0\\blue0;\\red200\\green200\\blue200;\\red55\\green55\\blue55;\\red255\\green255\\blue255;\\red100\\green100\\blue100;\\red0\\green0\\blue0;\\red255\\green255\\blue255;\\red0\\green0\\blue0;\\red255\\green255\\blue225;\\red0\\green0\\blue0;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;\\red192\\green192\\blue192;\\red150\\green150\\blue150;\\red128\\green128\\blue128;\\red102\\green102\\blue102;\\red51\\green51\\blue51;\\red91\\green151\\blue255;\\red255\\green97\\blue107;\\red183\\green124\\blue255;\\red0\\green176\\blue44;\\red252\\green88\\blue190;\\red255\\green144\\blue0;\\red46\\green176\\blue179;\\red51\\green102\\blue153;\\red128\\green0\\blue0;\\red0\\green128\\blue0;\\red0\\green0\\blue128;\\red128\\green128\\blue0;\\red128\\green0\\blue128;\\red0\\green128\\blue128;\\red0\\green0\\blue208;\\red212\\green212\\blue212;\\red50\\green106\\blue199;\\red0\\green120\\blue212;\\red136\\green23\\blue152;\\red227\\green0\\blue140;\\red0\\green78\\blue140;\\red209\\green52\\blue56;\\red202\\green80\\blue16;\\red3\\green131\\blue135;\\red152\\green111\\blue11;\\red164\\green38\\blue44;\\red194\\green57\\blue179;\\red57\\green57\\blue57;\\red79\\green107\\blue237;\\red117\\green11\\blue28;\\red135\\green100\\blue184;\\red122\\green117\\blue116;\\red0\\green91\\blue112;\\red92\\green46\\blue145;\\red105\\green121\\blue126;\\red142\\green86\\blue46;\\red170\\green170\\blue170;}\r\n\\trowd \\trgaph30\\trleft-30\\trrh290\\cellx7189\\cellx12911\\pard \\intbl \\ql \\f0\\fs22 \\cf8 A12-1(1)\\cell \\ql \\u1055\\'cf\\u1059\\'d3\\u1043\\'c3\\u1042\\'c2\\u1085\\'ed\\u1075\\'e3(\\u1040\\'c0)-LS 1\\u1093\\'f50,5\\cell \r\n\\pard \\intbl \\row\\trowd \\trgaph30\\trleft-30\\trrh290\\cellx7189\\cellx12911\\pard \\intbl \\ql N(49)\\cell \\ql \\u1055\\'cf\\u1059\\'d3\\u1043\\'c3\\u1042\\'c2\\u1085\\'ed\\u1075\\'e3(\\u1040\\'c0)-LS 1\\u1093\\'f50,5\\cell \\pard \\intbl \\row}\r\n"
            //);
            //string s = sb.ToString();
            var dataObj = new DataObject();
            dataObj.SetData(DataFormats.Rtf, sb);
            dataObj.SetData(DataFormats.Text, textToCB);


            Clipboard.SetDataObject(dataObj);
        }

        private static StringBuilder ToRTF(List<List<string>> clipboardData)
        {
            StringBuilder sb = new StringBuilder();

            int maxRowLen = clipboardData.Max(x => x.Count);

            int[] maxLenInColumn = new int[maxRowLen];

            for (int i = 0; i < maxLenInColumn.Length; i++)
                maxLenInColumn[i] = 300; //default value 


            sb.Append(@"{\rtf1 ");

            sb.Append(@"\trowd"); //Prepare the header Row

            for (int i = 0; i < maxRowLen; i++) //find max cell len
            {
                foreach (var row in clipboardData)
                {
                    if (i > row.Count)
                        break;
                    if (row[i].Length * 125 > maxLenInColumn[i])
                        maxLenInColumn[i] = row[i].Length * 125;
                }
            }

            int curLenth = 0;
            foreach (var len in maxLenInColumn) //set len width
            {
                curLenth += len;
                sb.Append(@"\cellx" + curLenth);
            }

            foreach (var row in clipboardData) //fill columns
            {
                sb.Append(@"\intbl"); //Start the row

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
                    outputList.Add(value.Substring(startIndex, endIndex - startIndex));

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
                    outputList.Add(value.Substring(startIndex));
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
