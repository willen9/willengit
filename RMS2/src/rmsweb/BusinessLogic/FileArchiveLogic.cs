using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLogic
{
    public class FileArchiveLogic
    {
        IFileArchiveDAL objDAL = new FileArchiveDAL();

        public bool AddFileArchive(FileArchive fileArchive, string fileName,string filePath)
        {
            return objDAL.AddFileArchive(fileArchive, fileName, filePath);
        }

        public List<FileArchive> SearchFile(string FileKey)
        {
            return objDAL.SearchFile(FileKey);
        }

        public bool DelFile(string FileKey)
        {
            return objDAL.DelFile(FileKey);
        }
    }
}