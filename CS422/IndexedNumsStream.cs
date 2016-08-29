using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CS422
{
    public class IndexedNumsStream : Stream
    {
        private int currentPosition;
        private long streamLength;
        IndexedNumsStream(long length)
        {
            streamLength = length;
        }

        public override bool CanRead
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanSeek
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanWrite
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                if (value >= 0)
                {
                    if(value > streamLength)
                    {
                        Position = streamLength;
                    }
                    else
                    {
                        Position = value;
                    }
                }
                else
                    Position = 0;
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count < 0)
                count = 0;
            for(int i = offset; i < count; i++)
            {
                buffer[i] =(byte)(i % 256);
            }
            return count;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            if (value < 0)
                streamLength = 0;
            else
                streamLength = value;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}