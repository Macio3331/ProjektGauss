using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ProjektGauss
{
    public partial class ProjectGauss : Form
    {
        [DllImport(@"C:\Users\macio\source\repos\ProjektGauss\x64\Debug\GaussAsm.dll")]
        static extern void gauss_filter_asm(short[] originalR, short[] modifiableR, int offset, int amount);

        [DllImport(@"C:\Users\macio\source\repos\ProjektGauss\x64\Debug\GaussCpp.dll")]
        static extern void gauss_filter_cpp(short[] originalR, short[] modifiableR, int offset, int amount);

        private delegate void MyProc(short[] originalR, short[] modifiableR, int offset, int amount);

        int numberOfThreads = 1;
        string path = "";
        private struct WavHeader
        {
            // chunk 0
            private int chunkID;
            public int ChunkID
            {
                get { return chunkID; }
                set { chunkID = value; }
            }

            private int fileSize;
            public int FileSize
            {
                get { return fileSize; }
                set { fileSize = value; }
            }

            private int riffType;
            public int RiffType
            {
                get { return riffType; }
                set { riffType = value; }
            }

            // chunk 1
            private int fmtID;
            public int FmtID
            {
                get { return fmtID; }
                set { fmtID = value; }
            }

            private int fmtSize; // bytes for this chunk (expect 16 or 18)
            public int FmtSize
            {
                get { return fmtSize; }
                set { fmtSize = value; }
            }

            // 16 bytes coming...
            private int fmtCode;
            public int FmtCode
            {
                get { return fmtCode; }
                set { fmtCode = value; }
            }

            private int channels;
            public int Channels
            {
                get { return channels; }
                set { channels = value; }
            }

            private int sampleRate;
            public int SampleRate
            {
                get { return sampleRate; }
                set { sampleRate = value; }
            }

            private int byteRate;
            public int ByteRate
            {
                set { byteRate = value; }
                get { return byteRate; }
            }

            private int fmtBlockAlign;
            public int FmtBlockAlign
            {
                get { return fmtBlockAlign; }
                set { fmtBlockAlign = value; }
            }

            private int bitDepth;
            public int BitDepth
            {
                get { return bitDepth; }
                set { bitDepth = value; }
            }

            //extra bytes
            private int fmtExtraSize;
            public int FmtExtraSize
            {
                get { return fmtExtraSize; }
                set { fmtExtraSize = value; }
            }

            private byte[] extradata;
            public byte[] ExtraData
            {
                get { return extradata; }
                set { extradata = value; }
            }

            // chunk 2
            private int dataID;
            public int DataID
            {
                get { return dataID; }
                set { dataID = value; }
            }

            private int bytes;
            public int Bytes
            {
                get { return bytes; }
                set { bytes = value; }
            }
        }

        public ProjectGauss()
        {
            InitializeComponent();
        }

        private void ProjectGauss_Load(object sender, EventArgs e)
        {
            radioButtonAsembler.Checked = true;
        }

        private void trackBarThreads_Scroll(object sender, EventArgs e)
        {
            numberOfThreads = trackBarThreads.Value;
            labelThreads.Text = "Iloœæ w¹tków: " + numberOfThreads;
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Wybierz plik";
            openFileDialog.Filter = "Wav files (*.wav)|*.wav";
            openFileDialog.Multiselect = false;
            openFileDialog.FilterIndex = 1;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;
                labelPath.Text = "Œcie¿ka do pliku: " + path;
                labelMessage.Text = "";
            }
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            if(path == "")
            {
                labelMessage.Text = "Wybierz plik!";
                return;
            }

            MyProc function;
            if (radioButtonAsembler.Checked) function = gauss_filter_asm;
            else function = gauss_filter_cpp;

            short[] originalL = null;
            short[] originalR = null;
            short[] modifiableL = null;
            short[] modifiableR = null;
            WavHeader header;

            bool read = readWav(path, out originalL, out originalR, out header);
            if(!read)
            {
                labelMessage.Text = "Nie uda³o siê wczytaæ pliku!";
                return;
            }

            modifiableL = new short[originalL.Length];
            Buffer.BlockCopy(originalL, 0, modifiableL, 0, originalL.Length * 2);

            Thread[] threadsTab = new Thread[numberOfThreads];
            int amount = 0;
            int rest = 0;
            if (originalR == null)
            {
                amount = (originalL.Length - 4) / numberOfThreads;
                rest = (originalL.Length - 4) % numberOfThreads;
            }
            else
            {
                modifiableR = new short[originalR.Length];
                Buffer.BlockCopy(originalR, 0, modifiableR, 0, originalR.Length * 2);
                amount = (originalR.Length + originalL.Length - 8) / numberOfThreads;
                rest = (originalR.Length + originalL.Length - 8) % numberOfThreads;
            }

            if(originalR == null)
            {
                for (int i = 0; i < numberOfThreads; i++)
                {
                    int offset = i * amount;
                    if (i < rest)
                    {
                        offset += i;
                        threadsTab[i] = new Thread(() => function(originalL, modifiableL, offset, amount + 1));
                    }
                    else
                    {
                        offset += rest;
                        threadsTab[i] = new Thread(() => function(originalL, modifiableL, offset, amount));
                    }
                }
            }
            else
            {
                if(numberOfThreads == 1)
                {
                    threadsTab[0] = new Thread(() =>
                    { 
                        function(originalL, modifiableL, 0, amount / 2);
                        function(originalR, modifiableR, 0, amount / 2); 
                    });
                }
                else if (numberOfThreads % 2 == 0)
                {
                    int restLeft = 0;
                    for (int i = 0; i < numberOfThreads / 2; i++)
                    {
                        int offset = i * amount;
                        if (i < rest)
                        {
                            offset += i;
                            threadsTab[i] = new Thread(() => function(originalL, modifiableL, offset, amount + 1));
                            restLeft++;
                        }
                        else
                        {
                            offset += rest;
                            threadsTab[i] = new Thread(() => function(originalL, modifiableL, offset, amount));
                        }
                    }
                    int newRest = rest - restLeft;
                    for (int i = numberOfThreads / 2, a = 0; i < numberOfThreads; i++)
                    {
                        int offset = a * amount;
                        if (i < newRest)
                        {
                            offset += a;
                            threadsTab[i] = new Thread(() => function(originalR, modifiableR, offset, amount + 1));
                        }
                        else
                        {
                            offset += newRest;
                            threadsTab[i] = new Thread(() => function(originalR, modifiableR, offset, amount));
                        }
                        a++;
                    }
                }
                else
                {
                    int firstTableOffset = 0;
                    int secondTableOffset = 0;
                    int restLeft = 0;
                    for (int i = 0; i < numberOfThreads / 2; i++)
                    {
                        int offset = i * amount;
                        if (i < rest)
                        {
                            firstTableOffset += i;
                            threadsTab[i] = new Thread(() => function(originalL, modifiableL, offset, amount + 1));
                            restLeft++;
                        }
                        else
                        {
                            firstTableOffset += rest;
                            threadsTab[i] = new Thread(() => function(originalL, modifiableL, offset, amount));
                        }
                        firstTableOffset = offset;
                    }
                    int newRest = rest - restLeft;
                    for (int i = numberOfThreads / 2, a = 0; i < numberOfThreads - 1; i++)
                    {
                        int offset = a * amount;
                        if (i < restLeft)
                        {
                            secondTableOffset += a;
                            threadsTab[i] = new Thread(() => function(originalR, modifiableR, offset, amount + 1));
                        }
                        else
                        {
                            secondTableOffset += restLeft;
                            threadsTab[i] = new Thread(() => function(originalR, modifiableR, offset, amount));
                        }
                        a++;
                        secondTableOffset = offset;
                    }
                    firstTableOffset += amount;
                    secondTableOffset += amount;
                    threadsTab[numberOfThreads - 1] = new Thread(() =>
                    {
                        function(originalL, modifiableL, firstTableOffset, originalL.Length - 4 - firstTableOffset);
                        function(originalR, modifiableR, secondTableOffset, originalR.Length - 4 - secondTableOffset);
                    });
                }
            }

            for(int i = 0; i < numberOfThreads; i++)
                threadsTab[i].Start();
            for(int i = 0; i < numberOfThreads; i++)
                threadsTab[i].Join();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Wybierz plik";
            saveFileDialog.Filter = "Wav files (*.wav)|*.wav";
            saveFileDialog.FilterIndex = 1;
            String destinationPath = "";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                destinationPath = saveFileDialog.FileName;
            }

            bool write = writeWav(destinationPath, modifiableL, modifiableR, header);
            if(!write)
            {
                labelMessage.Text = "Nie uda³o siê zapisaæ pliku!";
                return;
            }
        }

        static bool readWav(string filename, out short[] L, out short[] R, out WavHeader header)
        {
            L = R = null;
            header = new WavHeader();

            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);

                    // chunk 0
                    header.ChunkID = reader.ReadInt32();
                    header.FileSize = reader.ReadInt32();
                    header.RiffType = reader.ReadInt32();

                    // chunk 1
                    header.FmtID = reader.ReadInt32();
                    header.FmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

                    // 16 bytes coming...
                    header.FmtCode = reader.ReadInt16();
                    header.Channels = reader.ReadInt16();
                    header.SampleRate = reader.ReadInt32();
                    header.ByteRate = reader.ReadInt32();
                    header.FmtBlockAlign = reader.ReadInt16();
                    header.BitDepth = reader.ReadInt16();

                    if (header.FmtSize == 18)
                    {
                        // Read any extra values
                        int fmtExtraSize = reader.ReadInt16();
                        byte[] extraData = new byte[fmtExtraSize];
                        extraData = reader.ReadBytes(fmtExtraSize);
                        header.ExtraData = extraData;
                    }

                    // chunk 2
                    header.DataID = reader.ReadInt32(); //data in ASCII format
                    header.Bytes = reader.ReadInt32();

                    // DATA!
                    byte[] byteArray = reader.ReadBytes(header.Bytes);

                    int bytesForSamp = header.BitDepth / 8;
                    int nValues = header.Bytes / bytesForSamp;

                    short[] asShort = null;
                    switch (header.BitDepth)
                    {
                        case 16:
                            asShort = new short[nValues];
                            Buffer.BlockCopy(byteArray, 0, asShort, 0, header.Bytes);
                            break;
                        default:
                            return false;
                    }

                    switch (header.Channels)
                    {
                        case 1:
                            L = asShort;
                            R = null;
                            return true;
                        case 2:
                            // de-interleave
                            int nSamps = nValues / 2;
                            L = new short[nSamps];
                            R = new short[nSamps];
                            for (int s = 0, v = 0; s < nSamps; s++)
                            {
                                L[s] = asShort[v++];
                                R[s] = asShort[v++];
                            }
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch
            {
                Console.WriteLine("...Failed to load: " + filename);
                return false;
            }
        }

        static bool writeWav(string filename, short[] L, short[] R, WavHeader header)
        {
            try
            {
                using (FileStream fs = File.Create(filename))
                {
                    BinaryWriter writer = new BinaryWriter(fs);

                    // chunk 0
                    writer.Write(header.ChunkID);
                    writer.Write(header.FileSize);
                    writer.Write(header.RiffType);

                    // chunk 1
                    writer.Write(header.FmtID);
                    writer.Write(header.FmtSize);

                    // 16 bytes coming...
                    writer.Write((Int16)header.FmtCode);
                    writer.Write((Int16)header.Channels);
                    writer.Write(header.SampleRate);
                    writer.Write(header.ByteRate);
                    writer.Write((Int16)header.FmtBlockAlign);
                    writer.Write((Int16)header.BitDepth);

                    if (header.FmtSize == 18)
                    {
                        // Read any extra values
                        writer.Write((Int16)header.FmtExtraSize);
                        writer.Write(header.ExtraData);
                    }

                    // chunk 2
                    writer.Write(header.DataID);
                    writer.Write(header.Bytes);

                    // DATA!
                    if (R == null)
                    {
                        byte[] bytesL = new byte[L.Length * 2];
                        Buffer.BlockCopy(L, 0, bytesL, 0, L.Length * 2);
                        writer.Write(bytesL);
                    }
                    else
                    {
                        short[] data = new short[R.Length + L.Length];
                        for (int s = 0, v = 0; s < data.Length; s += 2, v++)
                        {
                            data[s] = L[v];
                            data[s + 1] = R[v];
                        }
                        byte[] bytes = new byte[data.Length * 2];
                        Buffer.BlockCopy(data, 0, bytes, 0, data.Length * 2);
                        writer.Write(bytes);
                    }
                }
                return true;
            }
            catch
            {
                Console.WriteLine("...Failed to write: " + filename);
                return false;
            }
        }
            
    }
}