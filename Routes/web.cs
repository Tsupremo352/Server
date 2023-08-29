using Server.App.Router;

namespace Server.Routes;
class Web{
    public static void RegisterRoutes(){
        Router.Get("/", "HomeController@index");
    }
}