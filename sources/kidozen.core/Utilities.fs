module Utilities

open System.IO
open System.Text

let replace oldStr newStr (s:string) = 
  s.Replace(oldValue=oldStr, newValue=newStr)

let startsWith lookFor (s:string) = 
  s.StartsWith(lookFor)

let split c (s:string) = 
    s.Split [|c|]

let getstring (data:Stream) =
    use sr = new StreamReader(data)
    sr.ReadToEnd()

let getstream (data:string) =
    use ms = new MemoryStream(UTF8Encoding.Default.GetBytes(data)) 
    ms
    
let (|Prefix|_|) (p:string) (s:string) =
    if s.StartsWith(p) then
        Some(s)
    else
        None
