using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Muni
{
    /// <summary>
    /// The principal function of this class is to serialize a Mesh object. 
    /// This class it also used when a Mesh object is saved into a binary file
    /// or it is used to get a Mesh object from a binary file. 
    /// The implemention of this class deprecates the old way to save the 
    /// 3D model data on txt files. 
    /// </summary>
    public class MeshSerializer
    {
        /// <summary>
        /// The defualt constructor
        /// </summary>
       public MeshSerializer() { }

       /// <summary>
       /// This function recives a path string and a Mesh object. This serializes the Mesh
       /// object into binary and saves the stream into a binary file. 
       /// </summary>
       /// <param name="filename">The path where the serialized mesh object will be saved</param>
       /// <param name="objectToSerialize">The Mesh object that will be saved on a file</param>
       public static void SaveMesh(string filename, Mesh objectToSerialize)
       {
          Stream stream = File.Open(filename, FileMode.Create);
          BinaryFormatter bFormatter = new BinaryFormatter();
          bFormatter.Serialize(stream, objectToSerialize);
          stream.Close();
       }

       /// <summary>
       /// This function reads an serialized Mesh object from a binary file. This 
       /// function recives the file path where the binary file is, then it read the stream
       /// and deserialize it on a Mesh object
       /// </summary>
       /// <param name="filename">The file path where the Mesh object is serialized</param>
       /// <returns>On an error this returns false, true otherwise</returns>
       public static Mesh LoadMesh(string filename)
       {
          Mesh tmp_mesh;
          if (File.Exists(filename) == false)
            return null;
          Stream stream = File.Open(filename, FileMode.Open);
          BinaryFormatter bFormatter = new BinaryFormatter();

          try
          {
              tmp_mesh = (Mesh)bFormatter.Deserialize(stream);
          }
          catch (System.Runtime.Serialization.SerializationException e)
          {

              return null;
          }

          stream.Close();
          return tmp_mesh;
       }
    }
}
