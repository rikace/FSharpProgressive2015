namespace Actors.HeartBeatActors

open Orleans.Runtime.Configuration
open Orleankka
open Orleankka.FSharp
open Orleankka.Playground
open System
open System.Reflection

type RateInfo = { HeartRate:int; BeatTime:DateTime}

type HeartBeatMessage = 
    | SetRate of RateInfo
    | GetHigestRate
    | GetLowestRate
    | GetAvarageRate
    | GetAllValues

// 1.
// Create an Heartbeat Actor using Orleankka
// The Actor should handle the message 'HeartBeatMessage'
// Use pattern matching to deconstruct the messages received
// and apply the logic to determine the low/high/avg rate according to the message 
// Use an internal state to keep track of all messages to calculate the result
// type HeartBeatActor() 
