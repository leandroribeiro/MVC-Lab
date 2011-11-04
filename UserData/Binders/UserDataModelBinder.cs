using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using UserData.Models;

namespace UserData.Binders {

    public class UserDataModelBinder<T> : IModelBinder { //DefaultModelBinder {

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");

            if (controllerContext.RequestContext.HttpContext.Request.IsAuthenticated) {
                var cookie = controllerContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (null == cookie)
                    return null;

                var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                if (!string.IsNullOrEmpty(decrypted.UserData))
                    //return BsonSerializer.Deserialize<T>(decrypted.UserData);
                    return new JavaScriptSerializer().Deserialize<T>(decrypted.UserData);

            }//if

            return null;
        }//method
    }

    public class UserDataModelBinder1 : DefaultModelBinder {

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Cannot update instances");

            if (controllerContext.RequestContext.HttpContext.Request.IsAuthenticated) {
                var cookie = controllerContext.RequestContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];

                if (null == cookie)
                    return null;

                var decrypted = FormsAuthentication.Decrypt(cookie.Value);

                if (!string.IsNullOrEmpty(decrypted.UserData))
                    //return BsonSerializer.Deserialize<T>(decrypted.UserData);
                    return new JavaScriptSerializer().Deserialize<UserDataModel>(decrypted.UserData);

            }//if

            return null;
        }//method


        private UserDataModel getValue(ModelBindingContext bindingContext, string key) {
            if (string.IsNullOrEmpty(key)) {
                return null;
            }
            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);
            if (result == null && bindingContext.FallbackToEmptyPrefix) {
                result = bindingContext.ValueProvider.GetValue(key);
            }
            if (result == null) {
                return null;
            }
            return (UserDataModel)result.ConvertTo(typeof(UserDataModel));
        }


    }//class

}//namespace