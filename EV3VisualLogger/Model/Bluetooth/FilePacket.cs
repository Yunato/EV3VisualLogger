using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3VisualLogger.Model.Bluetooth {
    class FilePacket : Packet{

        public void AddFileContent(byte data) {
            zmode.ZPutBin(ref packet, packet.Length, data);
            CRC = UpdateCRC(data, CRC);
        }
        
        public void DecideEOF(int signalSize) {
            byte[] TxHdr = new byte[4];
            zmode.ZStoHdr(ref TxHdr, signalSize);
            byte[] ZEOF = new byte[0];
            zmode.ZShHdr(ref ZEOF, TxHdr, 11, CRC);
            Add(ZEOF);
        }
    }
}
