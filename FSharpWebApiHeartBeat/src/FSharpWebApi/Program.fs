// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
open Microsoft.Owin.Hosting
open Owin
open Microsoft.Owin
open System
open System.IO
open System.Threading.Tasks
open System.Web.Http
open WebServerBuilder
open Orleankka
open Orleankka.FSharp
open System.Web.Http
open System.Threading.Tasks
open Orleankka.Playground
open System.Reflection
open Orleans.Runtime.Configuration

[<EntryPoint>]
let main argv = 

    let hostAddress = "http://localhost:8000"
    let server = WebApp.Start(hostAddress, getAppBuilder())
    
    let assembly = typeof<Actors.HeartBeatActors.HeartBeatActor>.Assembly
    let system = ActorSystem.Configure()
                            .Playground()
                            .Register(assembly)
                            .Done()
    
    Controllers.ActorInstance.Instance.HeartBeatActor <- system

    Console.ForegroundColor <- ConsoleColor.Yellow

    printfn "Web server up and running on %s" hostAddress
    printf  "Press any key to stop"

    Console.ReadKey() |> ignore

    system.Dispose()
    server.Dispose()

    0 // return an integer exit code
