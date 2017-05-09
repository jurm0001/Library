using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Diagnostics;

using WindowsApis.Data;


namespace WindowsApis
{
    public class Kernel32
    {

        public const UInt32 INFINITE = 0xFFFFFFFF;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);


        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        public static extern bool GetSystemPowerStatus(out WindowsApiStructs.SystemPowerStatus
           lpSystemPowerStatus);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, Int32 dwProcessId);

        [DllImport("kernel32.dll", EntryPoint = "CreateFile", SetLastError = true)]
        public static extern IntPtr CreateFile(String lpFileName,
            UInt32 dwDesiredAccess, UInt32 dwShareMode,
            IntPtr lpSecurityAttributes, UInt32 dwCreationDisposition,
            UInt32 dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateNamedPipe(
            String lpName,                                    // pipe name
            uint dwOpenMode,                                // pipe open mode
            uint dwPipeMode,                                // pipe-specific modes
            uint nMaxInstances,                            // maximum number of instances
            uint nOutBufferSize,                        // output buffer size
            uint nInBufferSize,                            // input buffer size
            uint nDefaultTimeOut,                        // time-out interval
            IntPtr pipeSecurityDescriptor        // SD
            );

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DisconnectNamedPipe(
            IntPtr hHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ConnectNamedPipe(
            IntPtr hHandle,                                    // handle to named pipe
            IntPtr lpOverlapped                    // overlapped structure
            );

        [DllImport("kernel32.dll", EntryPoint = "PeekNamedPipe", SetLastError = true)]
        public static extern bool PeekNamedPipe(IntPtr handle,
            byte[] buffer, uint nBufferSize, ref uint bytesRead,
            ref uint bytesAvail, ref uint BytesLeftThisMessage);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadFile(IntPtr handle,
            byte[] buffer, uint toRead, ref uint read, IntPtr lpOverLapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteFile(IntPtr handle,
            byte[] buffer, uint count, ref uint written, IntPtr lpOverlapped);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FlushFileBuffers(IntPtr handle);


        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool CreatePipe(
                  ref     IntPtr hReadPipe,
                  ref     IntPtr hWritePipe,
                  WindowsApiStructs.SECURITY_ATTRIBUTES lpPipeAttributes,
                  UInt32 nSize);


        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern IntPtr CallNamedPipe(
                    String lpNamedPipeName,
                    byte[] lpInBuffer,
                    UInt32 nInBufferSize,
                    out  byte[] lpOutBuffer,
                    UInt32 nOutBufferSize,
                    out  byte[] lpBytesRead,
                    WindowsApiEnums.PIPETIMEOUT nTimeOut);


        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool GetNamedPipeClientComputerName(
                    long Pipe,
                    ref string ClientComputerName,
                    ulong ClientComputerNameLength);


        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool GetNamedPipeClientProcessId(
                 long Pipe,
                 ref ulong ClientProcessId);



        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool GetNamedPipeClientSessionId(
                   long Pipe,
                   ref ulong ClientSessionId);



        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool GetNamedPipeHandleState(
                  long hNamedPipe,
                  ref UInt32 lpState,
                  ref UInt32 lpCurInstances,
                  ref UInt32 lpMaxCollectionCount,
                  ref UInt32 lpCollectDataTimeout,
                  ref string lpUserName,
                  UInt32 nMaxUserNameSize);


        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool GetNamedPipeInfo(
                  long hNamedPipe,
                  ref UInt32 lpFlags,
                  ref UInt32 lpOutBufferSize,
                  ref UInt32 lpInBufferSize,
                  ref UInt32 lpMaxInstances);



        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool GetNamedPipeServerProcessId(
                   long Pipe,
                   ref ulong ServerProcessId);

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool GetNamedPipeServerSessionId(
                  long Pipe,
                  ref ulong ServerSessionId);



        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool SetNamedPipeHandleState(
                  long hNamedPipe,
                  ref UInt32 lpMode,
                  ref UInt32 lpMaxCollectionCount,
                  ref UInt32 lpCollectDataTimeout);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool TransactNamedPipe(IntPtr hNamedPipe,
        byte[] lpInBuffer,
        UInt32 nInBufferSize,
        byte[] lpOutBuffer,
        UInt32 nOutBufferSize,
        out UInt32 lpBytesRead,
        IntPtr lpOverlapped);


        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern bool WaitNamedPipe(
                 string lpNamedPipeName,
                 UInt32 nTimeOut);


        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode, EntryPoint = "ReadFile", SetLastError = true)]
        public static extern bool ReadFileClient(IntPtr hFile, byte[] lpBuffer,
        uint nNumberOfBytesToRead, out uint lpNumberOfBytesRead, IntPtr lpOverlapped);

        [DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool InitializeSecurityDescriptor(out WindowsApiStructs.SECURITY_DESCRIPTOR pSecurityDescriptor,
            UInt32 dwRevision);

        [DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool SetSecurityDescriptorDacl(
            ref WindowsApiStructs.SECURITY_DESCRIPTOR pSecurityDescriptor,
            Int32 bDaclPresent,
            //ref ACL pDacl,
            IntPtr pDacl,
            Int32 bDaclDefaulted);

        [DllImport("kernel32.dll")]
        public static extern uint GetSystemDirectory([Out] StringBuilder lpBuffer,
           uint uSize);



    }
}
