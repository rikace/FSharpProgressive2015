module Controllers

open System.Web.Http
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Reflection
open Orleankka
open Orleankka.FSharp
open System.Web.Http
open System.Threading.Tasks
open Orleankka.Playground
open Orleans.Runtime.Configuration

open Newtonsoft.Json
open Actors.HeartBeatActors

type ActorInstance private () =
    let mutable system = Unchecked.defaultof<IActorSystem>

    static let instance = ActorInstance()
    static member Instance = instance
    member this.HeartBeatActor 
        with get() = system
        and set value = system <- value



type TemperatureValue() =
    member val value = 0 with get,set

type HeartBeatController() = 
    inherit ApiController()
    
    [<Route("highrate/{id:int}")>]
    [<System.Web.Http.HttpGet>]
    member x.GetHighRate(id : int) : Task<RateInfo> = Task.FromResult(Unchecked.defaultof<RateInfo>)
        // 1.
        // get a reference for the Actor/Grain HeartBeatActor
        // send the message 'GetHigestRate' to the actor and return the result
       
    
    // 2.
    // Create an API for lowrate message

    // 3. 
    // Create an API for avarage message

    // 3. 
    // Create an API for Set Rate message (POST)

    // 4. 
    // Create an API to retrive all Rates
