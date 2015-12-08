module WebServerBuilder

open Owin
open System.Web.Http


let getAppBuilder() =
    let config = new HttpConfiguration()
    config.MapHttpAttributeRoutes()

    // Configure serialization
    config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling <-Newtonsoft.Json.DefaultValueHandling.Include
    config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling <-Newtonsoft.Json.NullValueHandling.Include

    fun (appBuilder:IAppBuilder) -> appBuilder.UseWebApi(config) |> ignore
    