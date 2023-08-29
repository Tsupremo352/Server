using System.Reflection;

namespace Server.App.Router;
static class Router{
    public static void Get(string path, string controller){
        ResolveCall(controller, "GET");
    }
    public static void Post(string path, string controller){
    }
    public static void Put(string path, string controller){
    }
    public static void Delete(string path, string controller){
    }
    public static void Patch(string path, string controller){
    }

    public static void ResolveCall(string controller, string method){
        string[] pathParts = controller.Split("@");
        if(pathParts.Length != 2){
            throw new Exception("Invalid controller or method");
        }
        string controllerName = pathParts[0];
        string methodName = pathParts[1];
        if(controllerName != null && controllerName != "" && methodName != null && methodName != ""){
            Type? type = Type.GetType(typeName: $"Server.Controllers.{controllerName}");
            if(type != null){
                MethodInfo? methodInfo = type.GetMethod(methodName);
                if(methodInfo != null){
                    object? instance = Activator.CreateInstance(type);
                    if(instance != null){
                        methodInfo.Invoke(instance, null);
                    }else{
                        throw new Exception($"Invalid controller or method {controllerName}::{methodName}");
                    }
                }else{
                    throw new Exception($"Invalid controller or method {controllerName}::{methodName}");
                }
            }
        }else{
            throw new Exception("Invalid controller or method");
        }
    }
}