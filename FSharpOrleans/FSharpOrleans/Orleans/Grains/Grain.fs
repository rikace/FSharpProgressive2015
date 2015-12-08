namespace Grains

open System
open System.Threading.Tasks
open Orleans

open FSharpWorldInterfaces

type GrainHelloWorld() = 
    inherit Orleans.Grain()

    interface FSharpWorldInterfaces.IHello with

        override this.SayHello(greeting:string) =
            Task.FromResult<string>(sprintf "This comes from F#! - Hello %s!!" greeting)

// 1.
// Develop an Orleans Grain that implements the interface "FSharpWorldInterfaces.ICalculator"
// You can keep the a mutable current state inside the Orleans Grain 
// because the share nothing approach is keeping the state safe
// Add some logging to print into the console the operations

          
            
    