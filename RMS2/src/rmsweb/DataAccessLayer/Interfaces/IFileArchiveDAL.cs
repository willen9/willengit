using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccessLayer.Interfaces
{
    public interface IFileArchiveDAL
    {
        bool AddFileArchive(FileArchive fileArchive, string fileName,string filePath);

        List<FileArchive> SearchFile(string FileKey);

        bool DelFile(string FileKey);
    }
}