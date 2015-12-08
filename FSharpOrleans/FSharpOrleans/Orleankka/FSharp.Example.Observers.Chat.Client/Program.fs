
open Orleans.Runtime.Configuration
open Orleankka
open Orleankka.Client
open Orleankka.FSharp
open System
open System.Reflection
open ChatServer

[<EntryPoint>]
let main argv =    
   printfn "Please wait until Chat Server has completed boot and then press enter. \n"
   Console.ReadLine() |> ignore

   let config = ClientConfiguration().LoadFromEmbeddedResource(Assembly.GetExecutingAssembly(), "Client.xml")
   
   use system = ActorSystem.Configure()
                           .Client()
                           .From(config)
                           .Register(typeof<ChatServer>.Assembly)
                           .Done()

   // 1.
   // Create a generic Client observer Actor

   // 2. 
   // create a Chat Server Actor 

   printfn "Enter your user name... \n"
   let userName = Console.ReadLine()     
   
   // 2.
   // Subscribe the Client observer Actor
   // that receives and reacts to the messages sent
   // from the Server Actor
   // use pattern matching to deconstruct the message
   // and print into the console your message 
   
   let job() = task {
      printfn "Connecting.... \n"      

      // 3.
      // Send a message to the server actor to join the Chat
      // and print the "Welcome Response" (May be different color)
      
      printfn "Connected! \n"
      //printfn "%s\n" response // response from step 3
      
      // 4.
      // Create a recursive (or lame for/while loop)
      // to get your message from the console and send it 
      // to the actor (or 'Say' to the Actor).
      // The message can be sent or notified
      // Build extra functionality to "Disconnect" from the 
      // Server if the message that you insert is "quit" 
  
   }
   

   Task.run(job) |> ignore
   
   0
