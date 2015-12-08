namespace FSharpInterfaceLibrary
open System
open System.Threading.Tasks
open Orleans


type IHello =
   inherit IGrainWithIntegerKey

   abstract SayHello : greeting:string -> Task<string>
