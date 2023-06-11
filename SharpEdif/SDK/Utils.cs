using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpEdif
{
    public static class Utils
    {
        public static void Log(object str)
        {
            // I don't need logs rn
            Console.WriteLine(str);
        }
        public static unsafe void MemoryCopy(void* data,void* dest, int len)
        {
            for (int i = 0; i < len; i++)
            {
                ((byte*)dest)[i] = ((byte*)data)[i];
            }
        }

        public static unsafe void CopyStringToMemoryA(string str, byte* ptr, int cleanupLen)
        {
            var len = Encoding.ASCII.GetBytes(str).Length;
            for (int i = 0; i < cleanupLen; i++)
            {
                ptr[i] = 0;
            }
            var newString = Marshal.StringToHGlobalAnsi(str);
            MemoryCopy(newString.ToPointer(),(void*)ptr,len);
            
        }
        public static unsafe void CopyStringToMemoryW(string str, byte* ptr, int cleanupLen)
        {
            var len = Encoding.Unicode.GetBytes(str).Length;
            for (int i = 0; i < cleanupLen; i++)
            {
                ptr[i] = 0;
            }
            var newString = Marshal.StringToHGlobalUni(str);
            MemoryCopy(newString.ToPointer(),(void*)ptr,len);

        }
    }
}