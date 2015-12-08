#r "../Actors/bin/Debug/Actors.dll"
#r "../packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"
#r "../packages/FSharp.Charting.0.90.13/lib/net40/FSharp.Charting.dll"
#r "../packages/Newtonsoft.Json.6.0.4/lib/net40/Newtonsoft.Json.dll"

open FSharp.Data
open System
open FSharp.Charting
open Newtonsoft.Json
open Actors.HeartBeatActors

async {
    let! result = Http.AsyncRequestString("http://localhost:8000/setrate/5", 
                            httpMethod="POST",
                            body = FormValues ["", "80"])
    printfn "%A" result } |> Async.RunSynchronously


let rnd = Random()
let randomNumber = List.init 100 (fun _ -> rnd.Next(10, 220))
randomNumber
    |> Seq.map(fun n -> async {
                            let! result = Http.AsyncRequestString("http://localhost:8000/setrate/5", 
                                                                httpMethod="POST",
                                                                body = FormValues ["", string n]) 
                            return result })
    |> Async.Parallel 
    |> Async.RunSynchronously




async {
    let! result = Http.AsyncRequestString("http://localhost:8000/highrate/5", 
                            httpMethod="GET")
    printfn "%A" result } |> Async.RunSynchronously


async {
    let! result = Http.AsyncRequestString("http://localhost:8000/lowrate/5", 
                            httpMethod="GET")
    printfn "%A" result } |> Async.RunSynchronously
  
   
async {
    let! result = Http.AsyncRequestString("http://localhost:8000/avarage/5", 
                            httpMethod="GET")
    printfn "%A" result } |> Async.RunSynchronously

let rateInfoListString =
    async {
        let! result = Http.AsyncRequestString("http://localhost:8000/getall/5", 
                                httpMethod="GET")
        printfn "%A" result 
        return result} 
    |> Async.RunSynchronously


let rateInfoList = JsonConvert.DeserializeObject<RateInfo[]>(rateInfoListString)


rateInfoList
|> Seq.mapi(fun i r -> (r.BeatTime, r.HeartRate))
|> Chart.Line
|> Chart.Show


