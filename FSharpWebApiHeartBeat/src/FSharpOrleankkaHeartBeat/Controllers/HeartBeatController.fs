namespace FSharpOrleankkaHeartBeat.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Reflection
open Orleankka
open Orleankka.FSharp
open System.Web.Http
open System.Threading.Tasks
open System.Reactive.Subjects
open HeartBeatActors
open Orleankka.Playground
open Orleans.Runtime.Configuration
open Actors.HeartBeatActors

/// Retrieves values.
[<RoutePrefix("api2/heartbeat")>]
type HeartBeatController() =
    inherit ApiController()
        
    [<Route("highrate/{id:int}")>]
    [<System.Web.Http.HttpGet>]
    member x.GetHighRate(id : int) : Task<int> = 
        let grain = ActorInstance.Instance.HeartBeatActor.ActorOf<HeartBeatActor>(string id)
        let highRate() :Task<int> = grain <? GetHigestRate
        highRate()
    
    [<Route("lowrate/{id:int}")>]
    [<System.Web.Http.HttpGet>]
    member x.GetLowRate(id : int) : Task<int> = 
        let grain = ActorInstance.Instance.HeartBeatActor.ActorOf<HeartBeatActor>(string id)
        let lowRate() :Task<int> = grain <? GetLowestRate
        lowRate()

    [<Route("avarage/{id:int}")>]
    [<System.Web.Http.HttpGet>]
    member x.GetAvarage(id : int) : Task<int> = 
        let grain = ActorInstance.Instance.HeartBeatActor.ActorOf<HeartBeatActor>(string id)
        let lowRate() :Task<int> = grain <? GetAvarageRate
        lowRate()

    member this.Get() :Task<System.Collections.Generic.IEnumerable<int>> = 
        let grain = ActorInstance.Instance.HeartBeatActor.ActorOf<HeartBeatActor>(string id)

        let heartBeats() : Task<seq<int>> = grain <? GetAllValues
        heartBeats()

    [<Route("setrate/{id:int}"); System.Web.Http.HttpPost>]  
    member x.Post(id:int, [<FromBody>]value:int) : Task<int> =  
        let grain = ActorInstance.Instance.HeartBeatActor.ActorOf<HeartBeatActor>(string id)
        let setValue() :Task<int> = grain <? SetRate(int value)
        setValue()

