
namespace Joy.Structure
{
    public class Hash
    {
        public HashItem[] array;
        public Hash(int size)
        {
            array = new HashItem[size];
        }

        protected virtual int Locate(object key)
        {
            
        }
    }

    public class HashItem
    {
        public object Key;
        public object Value;
    }
}
