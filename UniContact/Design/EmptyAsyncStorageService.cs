﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Cimbalino.Phone.Toolkit.Services;

namespace UniContact.Design
{
    public class EmptyAsyncStorageService : IAsyncStorageService
    {
        public async Task CopyFileAsync(string sourceFileName, string destinationFileName)
        {
            
        }

        public async Task CopyFileAsync(string sourceFileName, string destinationFileName, bool overwrite)
        {
            
        }

        public async Task CreateDirectoryAsync(string dir)
        {
            
        }

        public async Task<Stream> CreateFileAsync(string path)
        {
            return null;
        }

        public async Task DeleteDirectoryAsync(string dir)
        {
            
        }

        public async Task DeleteFileAsync(string path)
        {
            
        }

        public async Task<bool> DirectoryExistsAsync(string dir)
        {
            return true;
        }

        public async Task<bool> FileExistsAsync(string path)
        {
            return true;
        }

        public async Task<string[]> GetDirectoryNamesAsync()
        {
            return null;
        }

        public async Task<string[]> GetDirectoryNamesAsync(string searchPattern)
        {
            return null;
        }

        public async Task<string[]> GetFileNamesAsync()
        {
            return null;
        }

        public async Task<string[]> GetFileNamesAsync(string searchPattern)
        {
            return null;
        }

        public async Task<Stream> OpenFileForReadAsync(string path)
        {
            return null;
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            return null;
        }

        public async Task<string> ReadAllTextAsync(string path, Encoding encoding)
        {
            return null;
        }

        public async Task<string[]> ReadAllLinesAsync(string path)
        {
            return null;
        }

        public async Task<string[]> ReadAllLinesAsync(string path, Encoding encoding)
        {
            return null;
        }

        public async Task<byte[]> ReadAllBytesAsync(string path)
        {
            return null;
        }

        public async Task WriteAllTextAsync(string path, string contents)
        {
            
        }

        public async Task WriteAllTextAsync(string path, string contents, Encoding encoding)
        {
            
        }

        public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents)
        {
            
        }

        public async Task WriteAllLinesAsync(string path, IEnumerable<string> contents, Encoding encoding)
        {
            
        }

        public async Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            
        }

        public async Task AppendAllText(string path, string contents)
        {
            
        }

        public async Task AppendAllText(string path, string contents, Encoding encoding)
        {
            
        }

        public async Task AppendAllLines(string path, IEnumerable<string> contents)
        {
            
        }

        public async Task AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        {
            
        }
    }
}
