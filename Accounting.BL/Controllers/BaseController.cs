using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Accounting.BL.Controllers
{
    public abstract class BaseController
    {
        protected T Get<T>(string fileName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if (fileStream.Length > 0 && binaryFormatter.Deserialize(fileStream) is T value)
                {
                    return value;
                }
                else
                {
                    return default(T);
                }
            }
        }

        // todo: can make returning value bool to check if saving method succeded
        // todo: maybe will have to change value type from T to object
        protected void Post<T>(string fileName, T value)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(fileStream, value);
            }
        }
    }
}
