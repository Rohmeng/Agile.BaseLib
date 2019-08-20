using MessagePack;

namespace Agile.BaseLib.Helpers
{
    public class MessagePackSerialize
    {
        static MessagePackSerialize()
        {
            MessagePackSerializer.SetDefaultResolver(MessagePack.Resolvers.ContractlessStandardResolver.Instance);
        }

        public static byte[] Serialize<T>(T obj)
        {
            return MessagePackSerializer.Serialize(obj);
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<T>(bytes);
        }
    }
}
