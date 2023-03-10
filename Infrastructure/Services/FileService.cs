using Infrastructure.Operation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    //public class FileService
    //{
    //     async Task<string> FileRenameAsync(string path,string fileName,bool first=true)
    //    {
    //      string newFileName = await Task.Run<string>( async () =>
    //        {
    //            string extension = Path.GetExtension(fileName);
    //            string newFileName = string.Empty;
    //            if (first)
    //            {
    //                string oldName = Path.GetFileNameWithoutExtension(fileName);
    //                newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
    //            }
    //            else
    //            {
    //                newFileName = fileName;
    //                int index = newFileName.IndexOf("-");
    //                if (index==-1)
    //                {
    //                    newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
    //                }
    //                else
    //                {
    //                    int last = 0;
    //                    while (true)
    //                    {
    //                        last = index;
    //                        index = newFileName.IndexOf('-', index + 1);
    //                        if(index == -1)
    //                        {
    //                            index= last;
    //                            break;
    //                        }
    //                    }

    //                    int index2 = newFileName.IndexOf(".");
    //                    string fileNo = newFileName.Substring(index+1,index2-index-1);

    //                    if(int.TryParse(fileNo, out int _fileNo))
    //                    {
    //                        _fileNo++;
    //                        newFileName = newFileName.Remove(index+1, index2 - index - 1)
    //                                              .Insert(index+1, _fileNo.ToString());
    //                    }
    //                    else
    //                    {

    //                    }


                        
    //                }
    //            }


    //            if (File.Exists($"{path}\\{newFileName}"))
    //                return await FileRenameAsync(path, newFileName, false);
    //            else
    //                return newFileName;
    //        });
    //        return newFileName;
    //    }

    //}
}
