using Android.Bluetooth;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.IO;
using BluetoothTest.Models;
using System.Collections;
using System.Drawing;
using System.Windows.Input;
using Android.Graphics;

namespace BluetoothTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BT : ContentPage
    {
        BluetoothSocket socket;
        BluetoothAdapter mBluetoothAdapter;
        BluetoothDevice device;
        public BT()
        {
            InitializeComponent();

            mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

            if (mBluetoothAdapter == null)
                throw new Exception("No Bluetooth adapter found.");

            if (!mBluetoothAdapter.IsEnabled)
                throw new Exception("Bluetooth adapter is not enabled.");

            //string MY_UUID = "loco";
            device = (from bd in mBluetoothAdapter.BondedDevices
                      where bd.Name.ToLower().Contains("print")
                      select bd).FirstOrDefault();

            socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
            socket.Connect();
          
            byte[] Text =  "Te amo Queruvi".ToCharArray().Select(character => (byte)character).ToArray();

            byte[] Restart = new byte[] { 0x1b, 0x40, 0x0a}; // ​​reiniciar la impresora
            byte[] Start = new byte[] { 0x0a };
            //buffer = JsonConvert.DeserializeObject<byte[]>("['0x1b', '0x42', '0x03', '0x03']");
            //buffer = JsonConvert.DeserializeObject<byte[]>("[0x1f, 0x11, 0x04']");
            //buffer = [0x1f, 0x11, 0x04];
            var aa = socket.IsConnected;
            // byte[] buffer = new byte[0];
            //0x1f, 0x11, 0x04
            //socket.OutputStream.Write(buffer2, 0, buffer2.Length);
            //socket.OutputStream.Write(buffer3, 0, buffer3.Length);
            //socket.OutputStream.Write(buffer, 0, buffer.Length);


            var aaaa = new string[] { "1C", "70", "01", "03" };
           
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                foreach (var item in aaaa)
                {
                    bw.Write(StringToByteArray(item));
                }
                // Reset the printer bws (NV images are not cleared)
                bw.Write(AsciiControlChars.Escape);
                bw.Write('@');
                bw.Write(AsciiControlChars.Newline);
                bw.Write(AsciiControlChars.Escape);
              
               

                bw.Write(new byte[] { 28, 112, 1, 0 });
                bw.LineaPunteada();
                bw.LargeText("KLK");
                bw.LineaPunteada();
                //bw.NormalFont("",false);
                //bw.Linea();
                bw.Write(AsciiControlChars.Newline);

                bw.Write("wawawa klk manin");
                bw.LineaPunteada();
                bw.Write(AsciiControlChars.Newline);
                bw.Write(AsciiControlChars.Newline);
                bw.Write(AsciiControlChars.Newline);
                bw.Write(AsciiControlChars.GroupSeparator);
                bw.Write('V');
                bw.Write((byte)66);
                bw.Write((byte)3);
                bw.Finish();

                //bw.Flush();
                string test = string.Empty;
           
                byte[] buffer =  ms.GetBuffer();

                // Send the converted ANSI string to the printer.

                socket.OutputStream.Write(buffer, 0, buffer.Length);
            }
        


            socket.Close();
            //if (mBluetoothAdapter.State == State.Connected)
            //{ 
            // socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            //}
            //try
            //{
            //    socket = device.CreateRfcommSocketToServiceRecord(MY_UUID);
            //}
            //catch (System.Exception e1)
            //{
            //    var x = e1.Message;
            //}



            var a = mBluetoothAdapter.IsEnabled;

            var b = mBluetoothAdapter.BondedDevices.Count;

            var c = (mBluetoothAdapter.State == State.Connected);

            var d = mBluetoothAdapter;



        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static class AsciiControlChars
        {
            /// <summary>
            /// Usually indicates the end of a string.
            /// </summary>
            public const char Nul = (char)0x00;

            /// <summary>
            /// Meant to be used for printers. When receiving this code the 
            /// printer moves to the next sheet of paper.
            /// </summary>
            public const char FormFeed = (char)0x0C;

            /// <summary>
            /// Starts an extended sequence of control codes.
            /// </summary>
            public const char Escape = (char)0x1B;

            /// <summary>
            /// Advances to the next line.
            /// </summary>
            public const char Newline = (char)0x0A;

            /// <summary>
            /// Defined to separate tables or different sets of data in a serial
            /// data storage system.
            /// </summary>
            public const char GroupSeparator = (char)0x1D;

            /// <summary>
            /// A horizontal tab.
            /// </summary>
            public const char HorizontalTab = (char)0x09;


            /// <summary>
            /// Vertical Tab
            /// </summary>
            public const char VerticalTab = (char)0x11;


            /// <summary>
            /// Returns the carriage to the start of the line.
            /// </summary>
            public const char CarriageReturn = (char)0x0D;

            /// <summary>
            /// Cancels the operation.
            /// </summary>
            public const char Cancel = (char)0x18;

            /// <summary>
            /// Indicates that control characters present in the stream should
            /// be passed through as transmitted and not interpreted as control
            /// characters.
            /// </summary>
            public const char DataLinkEscape = (char)0x10;

            /// <summary>
            /// Signals the end of a transmission.
            /// </summary>
            public const char EndOfTransmission = (char)0x04;

            /// <summary>
            /// In serial storage, signals the separation of two files.
            /// </summary>
            public const char FileSeparator = (char)0x1C;

        }

        //public string GetLogo()
        //{
        //    string logo = "";
        //    if (!File.Exists(@"C:\bitmap.bmp"))
        //        return null;
        //    BitmapData data = GetBitmapData(@"C:\bitmap.bmp");
        //    BitArray dots = data.Dots;
        //    byte[] width = BitConverter.GetBytes(data.Width);

        //    int offset = 0;
        //    MemoryStream stream = new MemoryStream();
        //    BinaryWriter bw = new BinaryWriter(stream);

        //    bw.Write((char)0x1B);
        //    bw.Write('@');

        //    bw.Write((char)0x1B);
        //    bw.Write('3');
        //    bw.Write((byte)24);

        //    while (offset < data.Height)
        //    {
        //        bw.Write((char)0x1B);
        //        bw.Write('*');         // bit-image mode
        //        bw.Write((byte)33);    // 24-dot double-density
        //        bw.Write(width[0]);  // width low byte
        //        bw.Write(width[1]);  // width high byte

        //        for (int x = 0; x < data.Width; ++x)
        //        {
        //            for (int k = 0; k < 3; ++k)
        //            {
        //                byte slice = 0;
        //                for (int b = 0; b < 8; ++b)
        //                {
        //                    int y = (((offset / 8) + k) * 8) + b;
        //                    // Calculate the location of the pixel we want in the bit array.
        //                    // It'll be at (y * width) + x.
        //                    int i = (y * data.Width) + x;

        //                    // If the image is shorter than 24 dots, pad with zero.
        //                    bool v = false;
        //                    if (i < dots.Length)
        //                    {
        //                        v = dots[i];
        //                    }
        //                    slice |= (byte)((v ? 1 : 0) << (7 - b));
        //                }

        //                bw.Write(slice);
        //            }
        //        }
        //        offset += 24;
        //        bw.Write((char)0x0A);
        //    }
        //    // Restore the line spacing to the default of 30 dots.
        //    bw.Write((char)0x1B);
        //    bw.Write('3');
        //    bw.Write((byte)30);

        //    bw.Flush();
        //    byte[] bytes = stream.ToArray();
        //    return logo + Encoding.Default.GetString(bytes);
        //}

        //public BitmapData GetBitmapData(string bmpFileName)
        //{
        //    using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
        //    {
        //        var threshold = 127;
        //        var index = 0;
        //        double multiplier = 570; // this depends on your printer model. for Beiyang you should use 1000
        //        double scale = (double)(multiplier / (double)bitmap.Width);
        //        int xheight = (int)(bitmap.Height * scale);
        //        int xwidth = (int)(bitmap.Width * scale);
        //        var dimensions = xwidth * xheight;
        //        var dots = new BitArray(dimensions);

        //        for (var y = 0; y < xheight; y++)
        //        {
        //            for (var x = 0; x < xwidth; x++)
        //            {
        //                var _x = (int)(x / scale);
        //                var _y = (int)(y / scale);
        //                var color = bitmap.GetPixel(_x, _y);
        //                var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
        //                dots[index] = (luminance < threshold);
        //                index++;
        //            }
        //        }

        //        return new BitmapData()
        //        {
        //            Dots = dots,
        //            Height = (int)(bitmap.Height * scale),
        //            Width = (int)(bitmap.Width * scale)
        //        };
        //    }
        //}

        public class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }


    }
}