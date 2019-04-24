using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3VisualLogger.Model.Bluetooth {
    class Packet {

        protected byte[] packet;
        protected ushort CRC;
        protected ZMode zmode = new ZMode();

        public Packet() {
            Init();
        }

        public void Init() {
            Array.Resize(ref packet, 0);
        }

        public void Add(byte[] signals) {
            int packetLen = packet.Length;
            Array.Resize(ref packet, packet.Length + signals.Length);
            Array.Copy(signals, 0, packet, packetLen, signals.Length);
        }

        public byte[] Get() {
            return packet;
        }

        public int GetSize() {
            return packet.Length;
        }

        public ushort GetCRC() {
            return CRC;
        }

        public void CopyCRCTo(Packet to) {
            to.CRC = this.CRC;
        }

        public void InitCRC() {
            CRC = 0;
        }

        public void UpdateCRCForAll(int start = 0) {
            for (int head = start; head < packet.Length; head++) {
                CRC = UpdateCRC(packet[head], CRC);
            }
        }

        public void UpdateCRCLastData() {
            CRC = UpdateCRC(packet[packet.Length - 1], CRC);
        }

        public static ushort UpdateCRC(byte b, ushort CRC) {
            int i;
            ushort buf;
            ushort buf0;
            byte buf2;
            byte buf3;
            buf0 = (ushort)b;
            buf2 = (byte)(b << 8);
            buf3 = (byte)((ushort)b << 8);
            buf = (ushort)((ushort)b << 8);

            CRC = (ushort)(CRC ^ (ushort)((ushort)b << 8));
            for (i = 1; i <= 8; i++)
                if ((CRC & 0x8000) != 0)
                    CRC = (ushort)((CRC << 1) ^ 0x1021);
                else
                    CRC = (ushort)(CRC << 1);
            return CRC;
        }

        public void DecideCRC() {
            zmode.ZPutBin(ref packet, packet.Length, (byte)((((CRC)) >> 8) & 0xff));
            zmode.ZPutBin(ref packet, packet.Length, (byte)(((CRC)) & 0xff));
        }
    }
}
