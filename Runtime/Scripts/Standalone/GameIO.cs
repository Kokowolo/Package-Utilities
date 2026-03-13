/* 
 * Copyright (c) 2026 Kokowolo. All Rights Reserved.
 * Author(s): Kokowolo, Will Lacey
 * Date Created: March 6, 2026
 * 
 * Additional Comments:
 *      Read - recreate the instance from data
 *      Load - load data onto another instance
 *
 *      Write - copy the instance down
 *      Save - cache data to recreate instance
 */

using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kokowolo.Utilities
{
    [Serializable]
    public abstract class GameIO : IDisposable
    {
        /*██████████████████████████████████████████████████████████*/
        #region Enums

        // enum FileMode
        // {
        //     Read = 0,
        //     Write = 1,
        // }

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Fields

        protected BinaryReader reader = null;
        protected BinaryWriter writer = null;

        protected float currentFileVersion;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Properties

        public bool IsAnyFileOpen => IsReaderOpen || IsWriterOpen;
        bool IsReaderOpen => reader != null;
        bool IsWriterOpen => writer != null;

        public virtual string FilePath => Application.persistentDataPath;
        public virtual string FileExtension => ".bytes";
        public virtual int NewFileVersion => 0;

        #endregion
        /*██████████████████████████████████████████████████████████*/
        #region Functions

        bool disposed;
        ~GameIO() => Dispose();
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;
            GC.SuppressFinalize(this);
            OnDispose();
            Close();
        }

        protected virtual void OnDispose() {}

        public void OpenRead(Stream stream)
        {
            if (IsAnyFileOpen)
            {
                LogManager.LogWarning($"{GetType()} has a file open");
                return;
            }
            reader = new BinaryReader(stream);
        }
        
        public void OpenWrite(Stream stream)
        {
            if (IsAnyFileOpen)
            {
                LogManager.LogWarning($"{GetType()} has a file open");
                return;
            }
            writer = new BinaryWriter(stream);
        }

        public bool TryClose()
        {
            if (!IsAnyFileOpen)
            {
                LogManager.LogWarning($"{GetType()} has no file open");
                return false;
            }
            Close();
            return true;
        }

        void Close()
        {
            if (IsWriterOpen)
            {
                writer.Close();
                writer = null;
            }
            if (IsReaderOpen)
            {
                reader.Close();
                reader = null;
            }
        }

        static Stream GetByteStream(TextAsset asset)
        {
            return asset ? new MemoryStream(asset.bytes) : null;
        }

        // protected virtual Stream GetByteStream(string fileName)
        // {
        //     if (fileName.Length == 0) return null;

        //     if (IsCustomMapName(fileName))
        //     {
        //         string path = Path.Combine(Application.persistentDataPath, fileName + FileExtension);
        //         if (IsPathValid(path)) return File.OpenRead(path);
        //     }
        //     else
        //     {
        //         // HACK: making input be only alphanumeric will guarantee that asset will not be null
        //         fileName = fileName.Substring(1);
        //         TextAsset asset = null;
        //         foreach (TextAsset file in mapFiles)
        //         {
        //             string fileName = Path.GetFileNameWithoutExtension(file.name); // removes .map
        //             if (fileName.Equals(fileName)) asset = file;
        //         }
        //         return GetByteStream(asset);
        //     }
        //     return null;
        // }

        // public void Delete()
        // {
        //     string mapName = nameInput.text;
        //     if (!IsCustomMapName(mapName)) return;

        //     string path = GetSelectedPath(mapName);
        //     if (path == null) return;

        //     // check if the file exists first
        //     if (File.Exists(path)) File.Delete(path);

        //     nameInput.text = "";
        //     FillList();
        // }

        public string GetSelectedPath(string fileName)
        {
            if (!IsValidFileName(fileName)) return null;
            var path = Path.Combine(FilePath, fileName + FileExtension);
            return path;
        }
        
        public virtual bool IsValidFileName(string fileName)
        {
            if (fileName.Length == 0) return false;
            return true;
        }

        // public virtual void LoadFile(string fileName)
        // {
        //     string path = GetSelectedPath(fileName);
        //     Open(File.Open(path, FileMode.Create));
        //     Write(fileVersion);
        //     Save(GridManager.Target);
        //     Close();
        // }

        // bool Load(string gameFileName) => Load(GetByteStream(gameFileName));
        public virtual bool Load(TextAsset gameFile) => Load(GetByteStream(gameFile));
        bool Load(Stream stream)
        {
            if (stream == null) 
            {
                Debug.LogWarning($"[{GetType()}] stream is null");
                return false;
            }
            OpenRead(stream);
            currentFileVersion = reader.ReadInt32();
            Load();
            Close();
            return true;
        }
        
        protected abstract void Load();

        public virtual void Save(string fileName)
        {
            if (!IsValidFileName(fileName)) return;
            string path = GetSelectedPath(fileName);
            OpenWrite(File.Open(path, FileMode.Create));
            writer.Write(NewFileVersion);
            Save();
            Close();
        }

        protected abstract void Save();

        /*——————————————————————————————————————————————————————————*/
        #region Writer Functions

        public void Write(Vector2Int value) 
        {
            writer.Write(value.x);
            writer.Write(value.y);
        }

        public void Write(Vector3 value) 
        {
            writer.Write(value.x);
            writer.Write(value.y);
            writer.Write(value.z);
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/
        #region Reader Functions

        public Vector2Int ReadVector2Int() 
        {
            return new Vector2Int(reader.ReadInt32(), reader.ReadInt32());
        }

        public Vector3 ReadVector3() 
        {
            return new Vector3(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
        }

        #endregion
        /*——————————————————————————————————————————————————————————*/

        #endregion
        /*██████████████████████████████████████████████████████████*/
    }
}