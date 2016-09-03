using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CS422
{
    public class IndexedNumsStream : Stream
    {
        private long currentPosition;
        private long streamLength;

        public IndexedNumsStream(long length)
        {
            try
            {
                if (length < 0)
                    streamLength = 0;
                //else if(length > Int32.MaxValue)
                //{
                //    throw new ArgumentOutOfRangeException("stream length is too long");
                //}
                else
                    streamLength = length;
                currentPosition = 0;
            }
            catch(ArgumentException ex)
            {
                
            }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return streamLength; }
        }

        public override long Position
        {
            get { return currentPosition; }

            set
            {
                if (value >= 0)
                {
                    if (value > streamLength)
                    {
                        currentPosition = streamLength;
                    }
                    else
                    {
                        currentPosition = value;
                    }
                }
                else
                    currentPosition = 0;
            }
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (count < 0)
                count = 0;
            if (offset < 0)
                offset = 0;
            if ((count + offset) > streamLength)
                throw new ArgumentException("Exceeded streamLength");
            if(count > buffer.Length)
            {
                throw new ArgumentOutOfRangeException("count is bigger than buffer");
            }
            int i = 0;
            for (i = 0; i < count && (i + offset < buffer.Length); i++)
            {
                buffer[i + offset] = (byte)(i % 256);
                currentPosition++;
            }
            return i;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (SeekOrigin.Begin == origin) // Seek origin passed is the begining of the stream
            {
                if (offset > streamLength)
                {
                    throw new ArgumentException("offset moves beyond streamLength");
                }
                else
                {
                    if (offset < 0)
                    {
                        throw new ArgumentException("offset is below zero");
                    }
                    else
                    {
                        currentPosition = offset;
                    }
                }
            }
            else if (SeekOrigin.End == origin) // seek origin passed is the end of the stream
            {
                if (offset > 0)
                {
                    throw new ArgumentException("offset is beyond streamLength");
                }
                else if ((offset + streamLength) < 0)
                {
                    throw new ArgumentException("offset is targeting before begining of stream");
                }
                else
                {
                    currentPosition = streamLength + offset;
                    return currentPosition;
                }
            }
            else // seek origin passed is current posisiton of the stream
            {
                if ((offset + currentPosition) < 0)
                {
                    throw new ArgumentException("offset is targeting before begining of stream");
                }
                else if ((offset + currentPosition) >= streamLength)
                {
                    throw new ArgumentException("offset is beyond streamLength");
                }
                else
                {
                    currentPosition += offset;
                }
            }
            return currentPosition;

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
        }
    }
}