using System.Web.Script.Serialization;

namespace UserData.Infrastructure {
    public static class JSONHelper {
        public static string ToJson(this object obj) {
            var serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJson(this object obj, int recursionDepth) {
            var serializer = new JavaScriptSerializer() { 
                RecursionLimit = recursionDepth
            };
            return serializer.Serialize(obj);
        }
    }
}