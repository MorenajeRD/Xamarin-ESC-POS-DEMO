using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static BluetoothTest.Views.BT;

namespace BluetoothTest.Models
{

    public static class ExMeth
    {
        public static void Enlarged(this BinaryWriter bw, string text)
        {
            bw.Write(AsciiControlChars.Escape);
            bw.Write((byte)33);
            bw.Write((byte)32);
            bw.Write(text);
            bw.Write(AsciiControlChars.Newline);
        }
        public static void High(this BinaryWriter bw, string text)
        {
            bw.Write(AsciiControlChars.Escape);
            bw.Write((byte)33);
            bw.Write((byte)16);
            bw.Write(text); //Width,enlarged
            bw.Write(AsciiControlChars.Newline);
        }
        public static void LargeText(this BinaryWriter bw, string text)
        {
            bw.Write(AsciiControlChars.Escape);
            bw.Write((byte)33);
            bw.Write((byte)48);
            bw.Write(text);
            bw.Write(AsciiControlChars.Newline);
        }
        public static void FeedLines(this BinaryWriter bw, int lines)
        {
            bw.Write(AsciiControlChars.Newline);
            if (lines > 0)
            {
                bw.Write(AsciiControlChars.Escape);
                bw.Write('d');
                bw.Write((byte)lines - 1);
            }
        }

        public static void LineaPunteada(this BinaryWriter bw)
        {
            bw.Write(AsciiControlChars.Newline);
            bw.NormalFont("------------------------------");
        }

        public static void Linea(this BinaryWriter bw)
        {
            bw.Write(AsciiControlChars.Newline);
            bw.NormalFont("______________________________");
        }




        public static void Finish(this BinaryWriter bw)
        {
            bw.FeedLines(1);
            //bw.NormalFont("---  Thank You, Come Again ---");
            bw.NormalFont("-- Gracias Por Preferirnos! --");
            bw.FeedLines(1);
            bw.Write(AsciiControlChars.Newline);
            bw.Write(AsciiControlChars.Newline);
            bw.Write(AsciiControlChars.Newline);
            bw.Write(AsciiControlChars.Newline);
            bw.Write(AsciiControlChars.Newline);
            bw.Write(AsciiControlChars.Newline);
        }

        public static void NormalFont(this BinaryWriter bw, string text, bool line = true)
        {
            bw.Write(AsciiControlChars.Escape);
            bw.Write((byte)33);
            bw.Write((byte)8);
            bw.Write(" " + text);
            if (line)
                bw.Write(AsciiControlChars.Newline);
        }
    }
}
