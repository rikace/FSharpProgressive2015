namespace HeartBeatActors

open Orleans.Runtime.Configuration
open Orleankka
open Orleankka.FSharp
open Orleankka.Playground
open System
open System.Reflection

//type HeartBeatMessage =
//   | SetRate of int 
//   | GetHigestRate 
//   | GetLowestRate
//   | GetAvarageRate
//
//
//type HeartBeatActor() = 
//   inherit Actor<HeartBeatMessage>()
//
//   let mutable rates = []
//
//   override this.Receive message reply = task {
//      match message with
//      | SetRate(rate) -> rates <- (rate::rates)
//      | GetHigestRate -> rates |> List.max |> reply
//      | GetLowestRate -> rates |> List.min |> reply
//      | GetAvarageRate -> rates |> List.map (float) |> List.average |> int |> reply
//   }



type ActorInstance private () =
    let mutable system = Unchecked.defaultof<IActorSystem>

    static let instance = ActorInstance()
    static member Instance = instance
    member this.HeartBeatActor 
        with get() = system
        and set value = system <- value
    
  



